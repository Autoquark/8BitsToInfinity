using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Behaviours
{
    class ButtonBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameObject _button;
        [SerializeField]
        private UnityEvent _onPushed;

        private bool _triggered;

        private void Update()
        {
            if(!_triggered && _button.transform.localPosition.y <= 0.21)
            {
                _triggered = true;
                _onPushed.Invoke();
            }
        }
    }
}
