using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    class PauseControlBehaviour : MonoBehaviour
    {
        int _pauseCount = 0;

        public void Pause()
        {
            _pauseCount++;

            Time.timeScale = 0;
        }

        public void Unpause()
        {
            _pauseCount--;

            if (_pauseCount <= 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
