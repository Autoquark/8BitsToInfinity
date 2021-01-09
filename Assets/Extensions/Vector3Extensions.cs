using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Extensions
{
    static class Vector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 vector) => (Vector2)vector;
    }
}
