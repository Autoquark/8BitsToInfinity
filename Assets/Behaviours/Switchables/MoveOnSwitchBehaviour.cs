using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours.Switchables
{
    class MoveOnSwitchBehaviour : MoveableBehaviour
    {
        private readonly Lazy<LevelControllerBehaviour> _controller;

        public MoveOnSwitchBehaviour()
        {
            _controller = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
        }

        protected override void FixedUpdate()
        {
            GoTowardsSwitchedPosition = _controller.Value.IsSwitched;
            base.FixedUpdate();
        }
    }
}
