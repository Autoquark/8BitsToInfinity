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
        public static Vector2 ToVector2(this Vector3 vector) => vector;

        public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);
    }
}
