using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Behaviours.Ui
{
    class PauseMenuBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Button _restartLevelButton;
        [SerializeField]
        private Button _mainMenuButton;

        private CursorLockMode _previousLockMode;

        private void Start()
        {
            var levelController = FindObjectOfType<LevelControllerBehaviour>();
            _restartLevelButton.onClick.AddListener(() => levelController.RestartLevel());
            _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Level_Machine"));
        }

        private void OnEnable()
        {
            _previousLockMode = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
            FindObjectOfType<PauseControlBehaviour>().Pause();
        }

        private void OnDisable()
        {
            Cursor.lockState = _previousLockMode;
            FindObjectOfType<PauseControlBehaviour>().Unpause();
        }
    }
}
