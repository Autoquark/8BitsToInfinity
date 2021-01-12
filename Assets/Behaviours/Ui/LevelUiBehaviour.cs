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
        private Text _ballsInGoalText;
        [SerializeField]
        private Text _goalsCompleteText;
        [SerializeField]
        private Text _restartText;
        [SerializeField]
        private LevelControllerBehaviour _levelController;

        private Lazy<IList<GoalZoneBehaviour>> _goalZonesWithRequirement;

        public LevelUiBehaviour()
        {
            _goalZonesWithRequirement = new Lazy<IList<GoalZoneBehaviour>>(() => FindObjectsOfType<GoalZoneBehaviour>().Where(x => x.RequiredInThisZone > 0).ToList());
        }

        private void Start()
        {
            _ballsInGoalText.gameObject.SetActive(_levelController.TotalBallsInGoalRequired > 0);
            _goalsCompleteText.gameObject.SetActive(_goalZonesWithRequirement.Value.Any(x => x.RequiredInThisZone > 0));
        }

        private void Update()
        {
            var remaining = FindObjectsOfType<BallBehaviour>().Count() + FindObjectsOfType<BallGeneratorBehaviour>().Sum(x => x.RemainingBalls);
            _ballsRemainingText.text = $"Balls remaining: {remaining}";

            _ballsInGoalText.text = $"Balls in goal: {_levelController.BallsInGoal}/{_levelController.TotalBallsInGoalRequired}";
            _ballsInGoalText.color = _levelController.BallsInGoal >= _levelController.TotalBallsInGoalRequired ? Color.green : Color.white;

            _goalsCompleteText.text = $"Goal zones complete: {_goalZonesWithRequirement.Value.Count(x => x.IsComplete)}/{_goalZonesWithRequirement.Value.Count}";
            _goalsCompleteText.color = _goalZonesWithRequirement.Value.All(x => x.IsComplete) ? Color.green : Color.white;

            _restartText.gameObject.SetActive(remaining + _levelController.BallsInGoal < _levelController.TotalBallsInGoalRequired);
        }
    }
}
