using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    class CameraControlBehaviour : MonoBehaviour
    {
        private Vector2 _previousMousePosition;
        private float _minY;
        private float _maxY;
        private float _mousePivotSensitivity = 500;
        private float _mousePanSensitivity = 150;
        private float _keyboardPivotSensitivity = 50;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            var bounds = FindObjectsOfType<CameraBoundBehaviour>();
            _minY = bounds.Min(x => x.transform.position.y);
            _maxY = bounds.Max(x => x.transform.position.y);
            _previousMousePosition = Input.mousePosition;
            transform.LookAt(new Vector3(0, (_minY + _maxY) / 2));
        }

        private void Update()
        {
            var xDelta = Input.GetAxis("Mouse X") * _mousePivotSensitivity;
            var yDelta = Input.GetAxis("Mouse Y") * _mousePanSensitivity;
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                xDelta -= _keyboardPivotSensitivity;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                xDelta += _keyboardPivotSensitivity;
            }
            transform.RotateAround(Vector3.zero, Vector3.up, xDelta * Time.deltaTime);
            _previousMousePosition = Input.mousePosition;

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + yDelta * Time.deltaTime, _minY, _maxY), transform.position.z);
        }
    }
}
