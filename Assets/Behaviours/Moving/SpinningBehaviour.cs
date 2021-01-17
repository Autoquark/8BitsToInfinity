using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours.Moving
{
    enum RotationAxis
    {
        X,
        Y,
        Z
    }

    class SpinningBehaviour : MonoBehaviour
    {
        [SerializeField]
        RotationAxis Axis = RotationAxis.Y;
        [SerializeField]
        private float SecondsForCompleteSpin = 3.0f;
        [SerializeField]
        private bool Backwards = false;

        private float CurrentAngle = 0.0f;
        private Quaternion BaseRotation;

        private void Awake()
        {
            BaseRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            if (!Backwards)
            {
                CurrentAngle += 360.0f / SecondsForCompleteSpin * Time.fixedDeltaTime;
            }
            else
            {
                CurrentAngle -= 360.0f / SecondsForCompleteSpin * Time.fixedDeltaTime;
            }
            Quaternion rotation = Quaternion.Euler(
                Axis == RotationAxis.X ? CurrentAngle : 0.0f,
                Axis == RotationAxis.Y ? CurrentAngle : 0.0f,
                Axis == RotationAxis.Z ? CurrentAngle : 0.0f);

            var rigidBody = GetComponent<Rigidbody>();
            if (rigidBody)
            {
                rigidBody.rotation = BaseRotation * rotation;
            }
            else
            {
                transform.rotation = BaseRotation * rotation;
            }
        }
    }
}
