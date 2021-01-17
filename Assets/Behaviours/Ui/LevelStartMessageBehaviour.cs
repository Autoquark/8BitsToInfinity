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
    class LevelStartMessageBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text _levelName;

        private void OnEnable()
        {
            _levelName.text = $"\"{LevelData.LevelNamesBySceneName[SceneManager.GetActiveScene().name]}\"";
        }
    }
}
