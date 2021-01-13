using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Behaviours.Editor
{
    class RandomBallColourBehaviour : MonoBehaviour
    {
        static readonly Lazy<Material> Red = new Lazy<Material>(() => Resources.Load<Material>("Ball Palette/CellShadeSphereRed"));
        static readonly Lazy<Material> Green = new Lazy<Material>(() => Resources.Load<Material>("Ball Palette/CellShadeSphereGreen"));
        static readonly Lazy<Material> Blue = new Lazy<Material>(() => Resources.Load<Material>("Ball Palette/CellShadeSphereBlue"));
        static readonly Lazy<Material> Yellow = new Lazy<Material>(() => Resources.Load<Material>("Ball Palette/CellShadeSphereYellow"));
        static readonly Lazy<Material> White = new Lazy<Material>(() => Resources.Load<Material>("Ball Palette/CellShadeSphereWhite"));

        private void Awake()
        {
            Material mat = Red.Value;

            if (UnityEngine.Random.value < 0.5f)
            {
                mat = Green.Value;
            }

            if (UnityEngine.Random.value < 0.333f)
            {
                mat = Blue.Value;
            }

            if (UnityEngine.Random.value < 0.1f)
            {
                mat = Yellow.Value;
            }

            if (UnityEngine.Random.value < 0.02f)
            {
                mat = White.Value;
            }

            var mr = GetComponent<MeshRenderer>();

            mr.materials = new[] { mat };
        }
    }
}
