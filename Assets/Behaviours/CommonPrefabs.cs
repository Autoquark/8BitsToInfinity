using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    class CommonPrefabs : MonoBehaviour
    {
        public static CommonPrefabs Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public GameObject Ball;
    }
}
