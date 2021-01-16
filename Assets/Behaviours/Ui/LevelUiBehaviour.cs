using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Behaviours.Ui
{
    class LevelUiBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text _ballsRemainingText;
        [SerializeField]
        private Text _goalsCompleteText;
        [SerializeField]
        private Text _restartText;
        [SerializeField]
        private LevelControllerBehaviour _levelController;
        [SerializeField]
        private GameObject _levelCompleteMenu;
        [SerializeField]
        private GameObject _inLevelUi;
        [SerializeField]
        private GameObject _levelStartMessage;
        [SerializeField]
        private GameObject _pauseMenu;

        public bool InTutorial = false;

        private Lazy<IList<GoalZoneBehaviour>> _goalZonesWithRequirement;

        private float _levelCompletedAt = -1;

        public LevelUiBehaviour()
        {
            _goalZonesWithRequirement = new Lazy<IList<GoalZoneBehaviour>>(() => FindObjectsOfType<GoalZoneBehaviour>().Where(x => x.RequiredInThisZone > 0).ToList());
        }

        private void Start()
        {
            _goalsCompleteText.gameObject.SetActive(_goalZonesWithRequirement.Value.Any(x => x.RequiredInThisZone > 0));
            _levelCompleteMenu.SetActive(false);
        }

        private void Update()
        {
            _levelStartMessage.SetActive(!InTutorial && !_levelController.LevelStarted && !_pauseMenu.activeSelf);

            var remaining = FindObjectsOfType<BallBehaviour>().Count() + FindObjectsOfType<BallGeneratorBehaviour>().Sum(x => x.RemainingBalls);
            _ballsRemainingText.text = $"Balls remaining: {remaining}";

            var goalZonesComplete = _goalZonesWithRequirement.Value.All(x => x.IsComplete);
            _goalsCompleteText.text = $"Goal zones complete: {_goalZonesWithRequirement.Value.Count(x => x.IsComplete)}/{_goalZonesWithRequirement.Value.Count}";
            _goalsCompleteText.color = goalZonesComplete ? Color.green : Color.white;

            float ballsRequired = _goalZonesWithRequirement.Value.Sum(x => x.RequiredInThisZone);

            _restartText.gameObject.SetActive(remaining + _levelController.BallsInGoal < ballsRequired);

            if(goalZonesComplete && _levelCompletedAt == -1)
            {
                _levelCompletedAt = Time.time;
            }

            if (_levelCompletedAt != -1 && Time.time - _levelCompletedAt > 1 && _inLevelUi.activeSelf)
            {
                if (!_levelCompleteMenu.activeSelf)
                {
                    Instantiate(_levelController.LevelCompleteAudio);
                }
                _inLevelUi.SetActive(false);
                _levelCompleteMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if(Input.GetKeyDown(KeyCode.Escape) && _levelCompletedAt == -1)
            {
                _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            }
        }

        public bool pauseMenuActive()
        {
            return _pauseMenu.activeSelf;
        }

    }
}
