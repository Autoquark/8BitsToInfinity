using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    class SwitchBehaviour : MonoBehaviour
    {
        Lazy<LevelControllerBehaviour> _levelController = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
        Lazy<GameObject> _switch_left;
        Lazy<GameObject> _switch_right;

        SwitchBehaviour()
        {
            _switch_left = new Lazy<GameObject>(() => transform.Find("ImageL").gameObject);
            _switch_right = new Lazy<GameObject>(() => transform.Find("ImageR").gameObject);
        }

        private void Update()
        {
            _switch_left.Value.SetActive(_levelController.Value.IsSwitched);
            _switch_right.Value.SetActive(!_levelController.Value.IsSwitched);
        }
    }
}
