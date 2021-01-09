using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours.Switchables
{
    class MoveOnSwitchBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _movement;
        [SerializeField]
        private Quaternion _rotation;
        [SerializeField]
        private float _switchingDuration = 0.5f;

        private Vector3 _unswitchedPosition;
        private Quaternion _unswitchedRotation;
        private bool _pendingSwitch = false;
        private bool _isSwitched = false;
        private bool _isSwitching = false;
        private float _moveSpeed;

        private void Awake()
        {
            _unswitchedPosition = transform.position;
            _unswitchedRotation = transform.rotation;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && !_isSwitching)
            {
                _pendingSwitch = true;
            }
        }

        private void FixedUpdate()
        {
            if(_pendingSwitch)
            {
                _pendingSwitch = false;
                StartCoroutine(Switch());
            }
        }

        private IEnumerator Switch()
        {
            var moveSpeedPerUpdate = (_movement.magnitude / _switchingDuration) * Time.fixedDeltaTime;
            var targetPosition = _isSwitched ? _unswitchedPosition : _unswitchedPosition + _movement;
            var targetRotation = _isSwitched ? _unswitchedRotation : _unswitchedRotation * _rotation;
            targetRotation.Normalize();
            var rotateSpeedPerUpdate = (Quaternion.Angle(transform.rotation, targetRotation) / _switchingDuration) * Time.fixedDeltaTime;
            var startTime = Time.fixedTime;
            _isSwitching = true;

            while (Time.fixedTime < startTime + _switchingDuration)
            {
                GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeedPerUpdate));
                GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, targetPosition, moveSpeedPerUpdate));
                yield return null;
            }

            _isSwitching = false;
            _isSwitched = !_isSwitched;
        }
    }
}
