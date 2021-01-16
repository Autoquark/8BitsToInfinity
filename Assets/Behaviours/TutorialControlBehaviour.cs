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

        int _step = 0;

        private void Start()
        {
            foreach(var step in Steps)
            {
                step.Root.SetActive(false);
            }

            Background.SetActive(false);

            StartPhase();
        }

        void StartPhase()
        {
            Steps[_step].Root.SetActive(true);
//            _pauseController.Value.Pause();
            Steps[_step].Special.Invoke();
            Background.SetActive(true);
        }

        void EndPhase()
        {
            Steps[_step].Root.SetActive(false);
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
                }
            }
        }
    }
}
