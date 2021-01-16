using Assets.Behaviours.Ui;
using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Behaviours
{
    class TutorialControlBehaviour : MonoBehaviour
    {
        [SerializeField]
        public GameObject Background;

        [Serializable]
        class Step
        {
            [SerializeField]
            public GameObject Root;
            [SerializeField]
            public GameObject Subject;
            [SerializeField]
            public UnityEvent Special;
        }

        [SerializeField]
        Step[] Steps;

        //        Lazy<PauseControlBehaviour> _pauseController = new Lazy<PauseControlBehaviour>(FindObjectOfType<PauseControlBehaviour>);
        static readonly Lazy<Material> Attract = new Lazy<Material>(() => Resources.Load<Material>("Pipe Palette/BasicPipeAttract"));
        Dictionary<MeshRenderer, Material[]> ResetMaterials = new Dictionary<MeshRenderer, Material[]>();
        static readonly Lazy<LevelControllerBehaviour> LevelController = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
        static readonly Lazy<LevelUiBehaviour> LevelUiController = new Lazy<LevelUiBehaviour>(FindObjectOfType<LevelUiBehaviour>);

        int _step = 0;

        private void Start()
        {
            LevelController.Value.Enabled = false;
            LevelUiController.Value.InTutorial = true;

            foreach (var step in Steps)
            {
                step.Root.SetActive(false);
            }

            Background.SetActive(false);

            StartPhase();
        }

        void StartPhase()
        {
            Step step = Steps[_step];
            step.Root.SetActive(true);
            //            _pauseController.Value.Pause();
            step.Special.Invoke();
            Background.SetActive(true);

            if (step.Subject)
            {
                SetAttractColours(step.Subject);
            }
        }

        void EndPhase()
        {
            Step step = Steps[_step];
            if (step.Subject)
            {
                ResetColours(step.Subject);
            }
            step.Root.SetActive(false);
//            _pauseController.Value.Unpause();
            _step++;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return)
                || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                EndPhase();

                if (_step < Steps.Length)
                {
                    StartPhase();
                }
                else
                {
                    Background.SetActive(false);
                    LevelController.Value.Enabled = true;
                    LevelUiController.Value.InTutorial = false;
                }
            }
        }
        private void SetAttractColours(GameObject subject)
        {
            ResetMaterials.Clear();

            foreach(var mr in subject.GetComponentsInChildren<MeshRenderer>())
            {
                ResetMaterials[mr] = mr.materials;
                mr.materials = new Material[] { Attract.Value };
            }
        }

        private void ResetColours(GameObject subject)
        {
            foreach(var pair in ResetMaterials)
            {
                pair.Key.materials = pair.Value;
            }
        }
    }
}
