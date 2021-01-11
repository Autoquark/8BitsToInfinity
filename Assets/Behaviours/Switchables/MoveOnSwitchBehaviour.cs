using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours.Switchables
{
    [RequireComponent(typeof(Rigidbody))]
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
        private readonly Lazy<LevelControllerBehaviour> _controller;

        public MoveOnSwitchBehaviour()
        {
            _controller = new Lazy<LevelControllerBehaviour>(() => FindObjectOfType<LevelControllerBehaviour>());
        }

        private void Awake()
        {
            _rotation.Normalize();

            _unswitchedPosition = transform.position;
            _unswitchedRotation = transform.rotation;
            _moveSpeed = (_movement.magnitude / _switchingDuration) * Time.fixedDeltaTime;
            _rotateSpeed = (Quaternion.Angle(_unswitchedRotation, _rotation * _unswitchedRotation) / _switchingDuration) * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            var temp = _rotation * Vector3.zero;
            //var temp = Vector3.zero * _rotation;
            var targetPosition = _controller.Value.IsSwitched ? _unswitchedPosition + _movement : _unswitchedPosition;
            var targetRotation = _controller.Value.IsSwitched ? _rotation * _unswitchedRotation : _unswitchedRotation;
            targetRotation.Normalize();

            GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed));
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed));
        }

        private void OnDrawGizmos()
        {
            foreach(var child in GetComponentsInChildren<MeshFilter>())
            {
                // Rotate each child's position around the position of this object
                var offset = child.transform.position - transform.position;
                offset = _rotation * offset;
                Gizmos.DrawWireMesh(child.sharedMesh, transform.position + offset, (_rotation * child.transform.rotation).normalized, child.transform.lossyScale);
                //Gizmos.DrawWireMesh(child.sharedMesh, child.transform.position + _movement, (child.transform.rotation * _rotation).normalized, child.transform.lossyScale);
            }
        }
    }
}
