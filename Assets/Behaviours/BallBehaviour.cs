using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    class BallBehaviour : MonoBehaviour
    {
        private readonly Lazy<LevelControllerBehaviour> _levelController;

        public BallBehaviour()
        {
            _levelController = new Lazy<LevelControllerBehaviour>(() => FindObjectOfType<LevelControllerBehaviour>());
        }

        private void Update()
        {
            if(transform.position.y < _levelController.Value.MinimumLevelGeometryY)
            {
                this.DestroyGameObject();
            }
        }
    }
}
