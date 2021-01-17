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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(LevelData._firstLevelIndex + _selectedLevelNumber);
                return;
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                _selectedLevelNumber++;
                if(_selectedLevelNumber == LevelData._levelCount)
                {
                    _selectedLevelNumber = 0;
                }
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectedLevelNumber--;
                if (_selectedLevelNumber == -1)
                {
                    _selectedLevelNumber = LevelData._levelCount - 1;
                }
            }

            // SceneManager.GetSceneByBuildIndex returns an invalid scene if the scene has not been loaded, so we have to do this
            _selectedLevelText.text = LevelData.LevelNamesBySceneName[Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(LevelData._firstLevelIndex + _selectedLevelNumber))];
        }
    }
}
