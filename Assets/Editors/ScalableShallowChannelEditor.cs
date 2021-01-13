using Assets.Behaviours;
using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(ScalableShallowChannelBehaviour))]
    [CanEditMultipleObjects]
    class ScalableChannelEditor : Editor
    {
        private IList<GameObject> _prefabs = new List<GameObject>();
        private Transform _lowest;
        private Transform _highest;
        private MonoBehaviour Target => (MonoBehaviour)target;
        private int _selectedPrefabIndex = 0;

        private void OnEnable()
        {
            _prefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PipeShallowSlope.prefab"));
            _prefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PipeSloped.prefab"));
            _prefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PipeFlat.prefab"));

            _lowest = FindLowest();
            _highest = FindHighest();
            if(_lowest != null)
            {
                _selectedPrefabIndex = _prefabs.IndexOf(PrefabUtility.GetCorrespondingObjectFromSource(_lowest.gameObject));
            }
            if(_selectedPrefabIndex == -1)
            {
                _selectedPrefabIndex = 0;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _selectedPrefabIndex = EditorGUILayout.Popup(_selectedPrefabIndex, _prefabs.Select(x => x.name).ToArray());

            if (_lowest == null)
            {
                if (GUILayout.Button("Add"))
                {
                    var next = (GameObject)PrefabUtility.InstantiatePrefab(_prefabs[_selectedPrefabIndex], ((MonoBehaviour)target).transform);
                    _highest = _lowest = next.transform;
                }
            }
            else if(GUILayout.Button("Extend Up"))
            {
                var next = (GameObject)PrefabUtility.InstantiatePrefab(_prefabs[_selectedPrefabIndex], ((MonoBehaviour)target).transform);
                next.transform.localPosition = _highest.localPosition + GetNextOffset(_prefabs[_selectedPrefabIndex]);
                _highest = next.transform;
            }
            else if (GUILayout.Button("Reduce Up"))
            {
                DestroyImmediate(_highest.gameObject);
                _highest = FindHighest();
            }
            else if (GUILayout.Button("Extend Down"))
            {
                var next = (GameObject)PrefabUtility.InstantiatePrefab(_prefabs[_selectedPrefabIndex], ((MonoBehaviour)target).transform);
                next.transform.localPosition = _lowest.localPosition - GetNextOffset(_lowest.gameObject);
                _lowest = next.transform;
            }
            else if (GUILayout.Button("Reduce Down"))
            {
                DestroyImmediate(_lowest.gameObject);
                _lowest = FindLowest();
            }
        }

        private Vector3 GetNextOffset(GameObject previous)
        {
            var size = previous.GetComponent<MeshFilter>().sharedMesh.bounds.size;
            return new Vector3(0, size.y - 0.5f, -size.z);
        }

        private Transform FindHighest() => Target.transform.Children().MaxByOrDefault(x => x.transform.localPosition.y);
        private Transform FindLowest() => Target.transform.Children().MinByOrDefault(x => x.transform.localPosition.y);
    }
}
