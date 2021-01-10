using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    class TintMeshesBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Color _color;

        private void Start()
        {
            foreach (var child in GetComponentsInChildren<MeshRenderer>())
            {
                child.material.color = _color;
            }
        }
    }
}
