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
        private bool _previousCursorVisible;

        private void Start()
        {
            var levelController = FindObjectOfType<LevelControllerBehaviour>();
            _restartLevelButton.onClick.AddListener(() => levelController.RestartLevel());
            _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Level_Machine"));
        }

        private void OnEnable()
        {
            _previousLockMode = Cursor.lockState;
            _previousCursorVisible = Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            FindObjectOfType<PauseControlBehaviour>().Pause();
        }

        private void OnDisable()
        {
            Cursor.lockState = _previousLockMode;
            Cursor.visible = _previousCursorVisible;
            FindObjectOfType<PauseControlBehaviour>().Unpause();
        }
    }
}
