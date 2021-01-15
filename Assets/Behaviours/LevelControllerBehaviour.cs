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
        public int BallsInGoal { get; set; } = 0;

        public bool IsSwitched { get; private set; } = false;

        public float MinimumLevelGeometryY { get; private set; }

        public bool LevelStarted => LevelStartTime != -1;

        public float LevelStartTime { get; private set; } = -1;

        public AudioClip LevelStartClip;

        public AudioClip LevelRestartClip;

        public GameObject CrossSceneAudioObject;

        private AudioSource audioSource;

        public void RestartLevel()
        {
            var camera = FindObjectOfType<CameraControlBehaviour>();
            camera.StorePosition();
            //GameObject liveAudio = Instantiate(CrossSceneAudioObject);
            //liveAudio.GetComponent<CrossSceneAudio>().PlayAudio(LevelRestartClip);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Start()
        {
            MinimumLevelGeometryY = GameObject.Find("LevelGeometry").GetComponentsInChildren<Transform>()
                .Min(x => x.position.y - 10);
            audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(LevelStartClip);
            }
        }
    }
}
