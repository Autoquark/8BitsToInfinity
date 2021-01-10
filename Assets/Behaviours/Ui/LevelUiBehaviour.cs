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
        private Text _restartText;
        [SerializeField]
        private LevelControllerBehaviour _levelController;

        private void Update()
        {
            var remaining = FindObjectsOfType<BallBehaviour>().Count() + FindObjectsOfType<BallGeneratorBehaviour>().Sum(x => x.RemainingBalls);
            _ballsRemainingText.text = $"Balls remaining: {remaining}";
            _ballsInGoalText.text = $"Balls in goal: {_levelController.BallsInGoal}/{_levelController.BallsInGoalRequired}";
            _ballsInGoalText.color = _levelController.BallsInGoal >= _levelController.BallsInGoalRequired ? Color.green : Color.white;
            _restartText.gameObject.SetActive(remaining + _levelController.BallsInGoal < _levelController.BallsInGoalRequired);
        }
    }
}
