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
    class CompletionUiBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Button _mainMenuButton;
        [SerializeField]
        private Button _quitButton;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Level_Machine"));
            _quitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}
