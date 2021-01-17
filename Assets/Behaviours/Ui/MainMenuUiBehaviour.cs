using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Behaviours.Ui
{
    class MainMenuUiBehaviour : MonoBehaviour
    {
        [Serializable]
        public class MenuPosition
        {
            [SerializeField]
            public GameObject UiRoot;
            [SerializeField]
            public float Pivot;
            [SerializeField]
            public float Pan;
            [SerializeField]
            public float Rotate;
            [SerializeField]
            public float Distance;
        }
        [SerializeField]
        private MenuPosition[] _menuPositions;

        private int _currentMenu = 0;
        Lazy<CameraControlBehaviour> _cameraControl;

        MainMenuUiBehaviour()
        {
            _cameraControl = new Lazy<CameraControlBehaviour>(FindObjectOfType<CameraControlBehaviour>);
        }

        private void Start()
        {
            if (_cameraControl.Value != null)
            {
                _cameraControl.Value.SetAutomaticMode();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _currentMenu--;
                if (_currentMenu == -1)
                {
                    _currentMenu = _menuPositions.Length - 1;
                }
                if (_cameraControl.Value != null)
                {
                    _cameraControl.Value.AnimateTo(_menuPositions[_currentMenu]);
                }
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                _currentMenu++;
                if(_currentMenu == _menuPositions.Length)
                {
                    _currentMenu = 0;
                }
                if (_cameraControl.Value != null)
                {
                    _cameraControl.Value.AnimateTo(_menuPositions[_currentMenu]);
                }
            }

            _menuPositions[_currentMenu].UiRoot.SetActive(true);
            foreach(var menu in _menuPositions.Select(x => x.UiRoot).Except(_menuPositions[_currentMenu].UiRoot))
            {
                menu.SetActive(false);
            }
        }
    }
}
