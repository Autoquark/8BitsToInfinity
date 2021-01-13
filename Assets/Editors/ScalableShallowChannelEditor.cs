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
    class ScalableShallowChannelEditor : Editor
    {
        private GameObject _prefab;
        private Transform _lowest;
        private Transform _highest;
        private MonoBehaviour Target => (MonoBehaviour)target;
        private Vector3 _upOffset;

        private void OnEnable()
        {
            _prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PipeShallowSlope.prefab");
            _lowest = FindLowest();
            _highest = FindHighest();

            _upOffset = new Vector3(0, 0.25f, -_prefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_lowest == null)
            {
                if (GUILayout.Button("Add"))
                {
                    var next = (GameObject)PrefabUtility.InstantiatePrefab(_prefab, ((MonoBehaviour)target).transform);
                    _highest = _lowest = next.transform;
                }
            }
            else if(GUILayout.Button("Extend Up"))
            {
                var next = (GameObject)PrefabUtility.InstantiatePrefab(_prefab, ((MonoBehaviour)target).transform);
                next.transform.localPosition = _highest.localPosition + _upOffset;
                _highest = next.transform;
            }
            else if (GUILayout.Button("Reduce Up"))
            {
                DestroyImmediate(_highest.gameObject);
                _highest = FindHighest();
            }
            else if (GUILayout.Button("Extend Down"))
            {
                var next = (GameObject)PrefabUtility.InstantiatePrefab(_prefab, ((MonoBehaviour)target).transform);
                next.transform.localPosition = _lowest.localPosition - _upOffset;
                _lowest = next.transform;
            }
            else if (GUILayout.Button("Reduce Down"))
            {
                DestroyImmediate(_lowest.gameObject);
                _lowest = FindLowest();
            }
        }

        private Transform FindHighest() => Target.transform.Children().MaxByOrDefault(x => x.transform.localPosition.y);
        private Transform FindLowest() => Target.transform.Children().MinByOrDefault(x => x.transform.localPosition.y);
    }
}
