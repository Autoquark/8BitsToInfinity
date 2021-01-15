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
            Text Name;
            [SerializeField]
            float Pivot;
            [SerializeField]
            float Pan;
            [SerializeField]
            float Rotate;
            [SerializeField]
            float Distance;
        }
        [SerializeField]
        private MenuPosition[] MenuPositions;

        private int CurrentMenu = 0;
        Lazy<CameraControlBehaviour> CameraControl;

        MainMenuUiBehaviour()
        {
            CameraControl = new Lazy<CameraControlBehaviour>(FindObjectOfType<CameraControlBehaviour>);
        }

        private void Start()
        {
            var cc = CameraControl.Value;
            if (cc != null)
            {
                cc.SetAutomaticMode();
            }
        }

        private void FixedUpdate()
        {
            var cc = CameraControl.Value;
            if (cc != null)
            {
                cc.AnimateTo(MenuPositions[CurrentMenu]);
            }
        }
    }
}
