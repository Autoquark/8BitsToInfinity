using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Behaviours
{
    class GoalZoneBehaviour : MonoBehaviour
    {
        GoalZoneBehaviour()
        {
            _levelController = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
            _camera = new Lazy<Camera>(FindObjectOfType<Camera>);
        }

        public int RequiredInThisZone = 0;
        public bool IsComplete => BallsInThisZone >= RequiredInThisZone;
        public int BallsInThisZone { get; private set; } = 0;

        private readonly Lazy<LevelControllerBehaviour> _levelController;
        private readonly Lazy<Camera> _camera;

        [SerializeField]
        private GameObject _displayRoot;
        [SerializeField]
        private Text _displayText;

        private void Start()
        {
            _displayRoot.SetActive(RequiredInThisZone > 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.HasComponent<BallBehaviour>())
            {
                BallsInThisZone++;
                _levelController.Value.BallsInGoal++;
                other.DestroyGameObject();
                GetComponent<AudioSource>().Play();
            }
        }

        private void Update()
        {
            _displayRoot.transform.forward = _camera.Value.transform.forward;
            _displayText.text = $"{BallsInThisZone}/{RequiredInThisZone}";
        }
    }
}
