using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours.Switchables
{
    [RequireComponent(typeof(Rigidbody))]
    abstract class MoveableBehaviour : MonoBehaviour
    {
        protected bool GoTowardsSwitchedPosition { get; set; } = false;
        protected bool IsAtSwitchedPosition { get; private set; } = false;

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

        private void Awake()
        {
            _rotation.Normalize();

            _unswitchedPosition = transform.position;
            _unswitchedRotation = transform.rotation;
            _moveSpeed = (_movement.magnitude / _switchingDuration) * Time.fixedDeltaTime;
            _rotateSpeed = (Quaternion.Angle(_unswitchedRotation, _rotation * _unswitchedRotation) / _switchingDuration) * Time.fixedDeltaTime;
        }

        protected virtual void FixedUpdate()
        {
            var targetPosition = GoTowardsSwitchedPosition ? _unswitchedPosition + _movement : _unswitchedPosition;
            var targetRotation = GoTowardsSwitchedPosition ? _rotation * _unswitchedRotation : _unswitchedRotation;
            targetRotation.Normalize();

            GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed));
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed));

            IsAtSwitchedPosition = GoTowardsSwitchedPosition && transform.position == targetPosition && transform.rotation == targetRotation;
        }

        private void OnDrawGizmos()
        {
            var originalPosition = transform.position;
            var originalRotation = transform.rotation;
            transform.position += _movement;
            transform.rotation = _rotation.normalized * transform.rotation;

            foreach (var child in GetComponentsInChildren<MeshFilter>())
            {
                Gizmos.DrawWireMesh(child.sharedMesh, child.transform.position, child.transform.rotation, child.transform.lossyScale);
            }

            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }

        public void SwitchTargetPosition() => GoTowardsSwitchedPosition = !GoTowardsSwitchedPosition;
    }
}
