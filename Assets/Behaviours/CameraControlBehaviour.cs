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
        private float _keyboardPanSensitivity = 40;
        private float _maxDistance = 50;
        private float _minDistance = 5;
        // The point with y = 0 on the axis which the camera should pivot around
        private Vector3 _pivotPointXZ = Vector3.zero;

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
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                xDelta -= _keyboardPivotSensitivity;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                xDelta += _keyboardPivotSensitivity;
            }
            transform.RotateAround(_pivotPointXZ, Vector3.up, xDelta * Time.deltaTime);
            _previousMousePosition = Input.mousePosition;

            var yDelta = Input.GetAxis("Mouse Y") * _mousePanSensitivity;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                yDelta += _keyboardPanSensitivity;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                yDelta -= _keyboardPanSensitivity;
            }
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + yDelta * Time.deltaTime, _minY, _maxY), transform.position.z);

            var zDelta = -Input.mouseScrollDelta.y;
            var currentDistance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_pivotPointXZ.x, 0, _pivotPointXZ.z));
            var distance = Mathf.Clamp(currentDistance + zDelta, _minDistance, _maxDistance);
            var newPositionXZ = (new Vector3(transform.position.x, 0, transform.position.z) - _pivotPointXZ).normalized * distance;
            transform.position = new Vector3(newPositionXZ.x, transform.position.y, newPositionXZ.z);
        }
    }
}
