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

    [RequireComponent(typeof(Rigidbody))]
    class SpinningBehaviour : MonoBehaviour
    {
        [SerializeField]
        RotationAxis Axis = RotationAxis.Y;
        [SerializeField]
        private float SecondsForCompleteSpin = 3.0f;

        private float CurrentAngle = 0.0f;
        private Quaternion BaseRotation;

        private void Awake()
        {
            BaseRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            CurrentAngle += 360.0f / SecondsForCompleteSpin * Time.fixedDeltaTime;

            Quaternion rotation = Quaternion.Euler(
                Axis == RotationAxis.X ? CurrentAngle : 0.0f,
                Axis == RotationAxis.Y ? CurrentAngle : 0.0f,
                Axis == RotationAxis.Z ? CurrentAngle : 0.0f);

            GetComponent<Rigidbody>().rotation = BaseRotation * rotation;
        }
    }
}
