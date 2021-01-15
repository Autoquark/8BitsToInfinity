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
    class LevelSelectUiBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text _selectedLevelText;

        private int _selectedLevelNumber = 0;
        private int _firstLevelIndex = 1;
        private int _lastLevelIndex = -1;
        private int _levelCount => _lastLevelIndex - _firstLevelIndex;

        private void Start()
        {
            if(_lastLevelIndex == -1)
            {
                _lastLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
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

            _selectedLevelText.text = "Level " + (_selectedLevelNumber + 1);
        }
    }
}
