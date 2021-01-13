using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours.Switchables
{
    class MoveOnTriggerBehaviour : MoveableBehaviour
    {
        [SerializeField]
        private float _resetDelay = -1;

        private float _lastArrivedAtSwitchedPositionTime = -1;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if(IsAtSwitchedPosition && _lastArrivedAtSwitchedPositionTime == -1)
            {
                _lastArrivedAtSwitchedPositionTime = Time.fixedTime;
            }
            if(IsAtSwitchedPosition && _resetDelay != -1 && Time.fixedTime - _lastArrivedAtSwitchedPositionTime >= _resetDelay)
            {
                SwitchTargetPosition();
                _lastArrivedAtSwitchedPositionTime = -1;
            }
        }
    }
}
