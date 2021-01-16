using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Behaviours.Ui;
using System.Collections;

namespace Assets.Behaviours
{
    class CameraControlBehaviour : MonoBehaviour
    {
        private float _minY;
        private float _maxY;

        private float _mousePivotSensitivity = 500;
        private float _mousePanSensitivity = 150;
        private float _mouseRotateSensitivity = 200;

        private float _keyboardPivotSensitivity = 50;
        private float _keyboardPanSensitivity = 20;
        private float _keyboardRotateSensitivity = 150;

        private float _maxDistance = 20;
        private float _minDistance = 5;

        private float _maxXAngle = 60;
        private float _minXAngle = 0;

        private bool _invertRotation = true;

        // The point with y = 0 on the axis which the camera should pivot around
        private Vector3 _pivotPointXZ = Vector3.zero;

        private bool _manualMode = true;

        private float _distance = 0;
        private float _pivot = 0;
        private float _rotate = 0;
        private float _pan = 0;

        private Coroutine _animateToCoroutine;

        private readonly float _transitionTime = 0.5f;

        private static Vector3? _previousCameraPosition;
        private static Quaternion? _previousCameraRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            var geometry = GameObject.Find("LevelGeometry").transform.Children().ToList();
            _minY = geometry.Min(x => x.transform.position.y);
            _maxY = geometry.Max(x => x.transform.position.y) + 5;

            if (_previousCameraPosition != null)
            {
                transform.position = _previousCameraPosition.Value;
                _previousCameraPosition = null;
            }

            if (_previousCameraRotation != null)
            {
                transform.rotation = _previousCameraRotation.Value;
                _previousCameraRotation = null;
            }

            // Set pivot point to the centre of the bounding box of all level geometry
            _pivotPointXZ = new Vector3((geometry.Min(t => t.position.x) + geometry.Max(t => t.position.x)) / 2, 0, (geometry.Min(t => t.position.z) + geometry.Max(t => t.position.z)) / 2);
            transform.Rotate(0, Vector3.SignedAngle(transform.forward.WithY(0), _pivotPointXZ - transform.position.WithY(0), Vector3.up), 0, Space.World);

            _distance = (_pivotPointXZ - transform.position.WithY(0)).magnitude;
            _pivot = transform.rotation.eulerAngles.y;
            _rotate = transform.rotation.eulerAngles.x;
            _pan = transform.position.y;
        }

        private void Update()
        {
            float xDelta = 0;
            float yDelta = 0;
            float zDelta = 0;
            float elevationDelta = 0;

            if (_manualMode)
            {
                Debug.DrawLine(_pivotPointXZ, _pivotPointXZ.WithY(10), Color.green);

                // Left/right pivot around pivot point
                xDelta = Input.GetMouseButton(0) ? Input.GetAxis("Mouse X") * _mousePivotSensitivity : 0;
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    xDelta -= _keyboardPivotSensitivity;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    xDelta += _keyboardPivotSensitivity;
                }

                _pivot += xDelta * Time.deltaTime;

                // Up/down pan
                yDelta = Input.GetMouseButton(0) ? Input.GetAxis("Mouse Y") * _mousePanSensitivity : 0;
                if (Settings.InvertCameraPan)
                {
                    yDelta = -yDelta;
                }
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    yDelta += _keyboardPanSensitivity;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    yDelta -= _keyboardPanSensitivity;
                }

                _pan += yDelta * Time.deltaTime;

                // 'zoom' - adjusts camera distance from pivot
                zDelta = -Input.mouseScrollDelta.y;

                _distance += zDelta;

                // Camera up/down rotation
                elevationDelta = Input.GetMouseButton(1) ? Input.GetAxis("Mouse Y") * _mouseRotateSensitivity : 0;
                if (Input.GetKey(KeyCode.E))
                {
                    elevationDelta += _keyboardPanSensitivity;
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    elevationDelta -= _keyboardPanSensitivity;
                }
                elevationDelta = _invertRotation ? -elevationDelta : elevationDelta;

                _rotate += elevationDelta * Time.deltaTime;
            }

            _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
            _pan = Mathf.Clamp(_pan, _minY, _maxY);
            _rotate = Mathf.Clamp(_rotate, _minXAngle, _maxXAngle);
            _pivot %= 360f;

            transform.rotation = Quaternion.Euler(_rotate, _pivot, 0);
            transform.position = _pivotPointXZ - transform.forward.WithY(0).normalized * _distance;
            transform.position = transform.position.WithY(_pan);
        }

        public void SetAutomaticMode()
        {
            _manualMode = false;
        }

        public void AnimateTo(MainMenuUiBehaviour.MenuPosition where)
        {
            if (_animateToCoroutine != null)
            {
                StopCoroutine(_animateToCoroutine);
            }
            _animateToCoroutine = StartCoroutine(AnimateToCoroutine(where));
        }

        public void StorePosition()
        {
            _previousCameraPosition = transform.position;
            _previousCameraRotation = transform.rotation;
        }

        private IEnumerator AnimateToCoroutine(MainMenuUiBehaviour.MenuPosition where)
        {
            var difference = where.Pivot - _pivot;
            var candidates = new[] { difference, difference + 360, difference - 360 };
            var pivotRate = candidates.MinBy(Mathf.Abs) / _transitionTime;

            var distanceRate = (where.Distance - _distance) / _transitionTime;
            var rotateRate = (where.Rotate - _rotate) / _transitionTime;
            var panRate = (where.Pan - _pan) / _transitionTime;
            var finishTime = Time.time + _transitionTime;
            
            while(Time.time < finishTime)
            {
                _pivot += pivotRate * Time.deltaTime;
                _distance += distanceRate * Time.deltaTime;
                _pan += panRate * Time.deltaTime;
                _rotate += rotateRate * Time.deltaTime;
                yield return null;
            }

            _pivot = where.Pivot;
            _distance = where.Distance;
            _pan = where.Pan;
            _rotate = where.Rotate;
        }
    }
}
