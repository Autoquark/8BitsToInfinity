using Assets.Extensions;
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
        [SerializeField]
        private float _resetDelay = -1;

        private float _triggeredAt = -1;
        private Rigidbody _buttonRigidbody;

        private readonly float _untriggeredY = 0.3f;
        private readonly float _triggeredY = 0.2f;
        private readonly float _resetTime = 0.5f;

        private void Start()
        {
            _buttonRigidbody = _button.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(_triggeredAt == -1 && _button.transform.localPosition.y <= 0.21)
            {
                _triggeredAt = Time.time;
                _button.transform.localPosition = _button.transform.localPosition.WithY(_triggeredY);
                _buttonRigidbody.isKinematic = true;
                _onPushed.Invoke();
            }
            if(_triggeredAt != -1 && _resetDelay != -1 && Time.time - _triggeredAt > _resetDelay)
            {
                var offset = _button.transform.position - _button.transform.localPosition;
                _buttonRigidbody.MovePosition(
                    offset + Vector3.MoveTowards(
                        _button.transform.localPosition, _button.transform.localPosition.WithY(_untriggeredY),
                        (Mathf.Abs(_triggeredY - _untriggeredY) * Time.fixedDeltaTime) / _resetTime));

                if(_buttonRigidbody.transform.localPosition.y >= _untriggeredY - 0.001f)
                {
                    _buttonRigidbody.isKinematic = false;
                    _triggeredAt = -1;
                }
            }
        }
    }
}
