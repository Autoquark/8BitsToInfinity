using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Behaviours.Ui
{
    class LevelSelectUiBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text _selectedLevelText;

        private int _selectedLevelNumber = 0;
        private int _firstLevelIndex = 1;
        private int _lastLevelIndex = -1;
        private int _levelCount => _lastLevelIndex - _firstLevelIndex;

        private IDictionary<string, string> _sceneNameToLevelName = new Dictionary<string, string>
        {
            { "Level_Catapult", "Catapult" },
            { "Level_Gauntlet", "Gauntlet" },
            { "Level_Pachinko", "Pachinko" },
            { "Level_Seesaw", "Seesaw" },
            { "Level_Spinner1", "Spin to Win" },
            { "Level_Spinner2", "Spinstack" },
            { "Level_ThreeWay", "2 out of 3 Ain't Enough" },
            { "Level_Turnstile", "Turnstile" },
            { "Level_TurnTheRightWay", "Turn the Right Way" },
            { "Level_TurnTurnTurn", "Turn Turn Turn!" },
            { "Level_Tutorial", "Tutorial" },
        };

        private void Start()
        {
            if(_lastLevelIndex == -1)
            {
                _lastLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(_firstLevelIndex + _selectedLevelNumber);
                return;
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectedLevelNumber++;
                if(_selectedLevelNumber == _levelCount)
                {
                    _selectedLevelNumber = 0;
                }
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                _selectedLevelNumber--;
                if (_selectedLevelNumber == -1)
                {
                    _selectedLevelNumber = _levelCount - 1;
                }
            }

            // SceneManager.GetSceneByBuildIndex returns an invalid scene if the scene has not been loaded, so we have to do this
            _selectedLevelText.text = _sceneNameToLevelName[Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(_firstLevelIndex + _selectedLevelNumber))];
        }
    }
}
