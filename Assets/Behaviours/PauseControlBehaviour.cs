using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Behaviours
{
    class PauseControlBehaviour : MonoBehaviour
    {
        int _pauseCount = 0;
        [SerializeField]
        AudioMixer _mainMixer;

        public void Pause()
        {
            _pauseCount++;
            _mainMixer.FindSnapshot("Snapshot-Mute").TransitionTo(0f);
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            _pauseCount--;

            if (_pauseCount <= 0)
            {
                _mainMixer.FindSnapshot("Snapshot").TransitionTo(0f);
                Time.timeScale = 1;
            }
        }
    }
}
