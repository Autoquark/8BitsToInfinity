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
        private void OnTriggerEnter(Collider other)
        {
            if(other.HasComponent<GoalZoneBehaviour>())
            {
                FindObjectOfType<LevelControllerBehaviour>().BallsInGoal++;
                this.DestroyGameObject();
            }
        }
    }
}
