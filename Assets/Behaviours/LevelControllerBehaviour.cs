using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Behaviours
{
    class LevelControllerBehaviour : MonoBehaviour
    {
        public int BallsInGoalRequired = 1;

        public int BallsInGoal { get; set; } = 0;

        public bool IsSwitched { get; private set; } = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsSwitched = !IsSwitched;
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
