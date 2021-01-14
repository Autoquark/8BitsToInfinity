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
        private static Vector3 _previousCameraPosition;
        private static Quaternion _previousCameraRotation;
        private static string _previousScene;

        public int TotalBallsInGoalRequired = 1;

        public int BallsInGoal { get; set; } = 0;

        public bool IsSwitched { get; private set; } = false;

        public float MinimumLevelGeometryY { get; private set; }

        public bool LevelStarted => LevelStartTime != -1;

        public float LevelStartTime { get; private set; } = -1;

        public void RestartLevel()
        {
            var camera = FindObjectOfType<CameraControlBehaviour>();
            _previousCameraPosition = camera.transform.position;
            _previousCameraRotation = camera.transform.rotation;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Start()
        {
            var scene = SceneManager.GetActiveScene();
            if (scene.name == _previousScene)
            {
                var camera = FindObjectOfType<CameraControlBehaviour>();
                camera.transform.position = _previousCameraPosition;
                camera.transform.rotation = _previousCameraRotation;
            }

            _previousScene = scene.name;

            MinimumLevelGeometryY = GameObject.Find("LevelGeometry").GetComponentsInChildren<Transform>()
                .Min(x => x.position.y - 10);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsSwitched = !IsSwitched;
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                RestartLevel();
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                LevelStartTime = Time.time;
            }
        }
    }
}
