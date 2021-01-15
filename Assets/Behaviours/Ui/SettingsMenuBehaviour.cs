using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Behaviours.Ui
{
    class SettingsMenuBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text _cameraInvertValueText;

        private void Update()
        {
            _cameraInvertValueText.text = Settings.InvertCameraPan ? "On" : "Off";
            _cameraInvertValueText.color = Settings.InvertCameraPan ? Color.green : Color.red;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Settings.InvertCameraPan = !Settings.InvertCameraPan;
            }
        }
    }
}
