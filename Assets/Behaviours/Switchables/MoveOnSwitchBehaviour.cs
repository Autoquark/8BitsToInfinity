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
        private float _moveSpeed;
        private float _rotateSpeed;
        private readonly Lazy<ControllerBehaviour> _controller;

        public MoveOnSwitchBehaviour()
        {
            _controller = new Lazy<ControllerBehaviour>(() => FindObjectOfType<ControllerBehaviour>());
        }

        private void Awake()
        {
            _unswitchedPosition = transform.position;
            _unswitchedRotation = transform.rotation;
            _moveSpeed = (_movement.magnitude / _switchingDuration) * Time.fixedDeltaTime;
            _rotateSpeed = (Quaternion.Angle(transform.rotation, _rotation) / _switchingDuration) * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            var targetPosition = _controller.Value.IsSwitched ? _unswitchedPosition + _movement : _unswitchedPosition;
            var targetRotation = _controller.Value.IsSwitched ? _unswitchedRotation * _rotation : _unswitchedRotation;
            targetRotation.Normalize();

            GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed));
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed));
        }
    }
}
