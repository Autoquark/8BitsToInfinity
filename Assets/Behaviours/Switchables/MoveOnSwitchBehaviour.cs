using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Assets.Behaviours.Ui;



namespace Assets.Behaviours.Switchables
{
    
    class MoveOnSwitchBehaviour : MoveableBehaviour
    {
        private readonly Lazy<LevelControllerBehaviour> _controller;
        private readonly Lazy<LevelUiBehaviour> _ui;
        private AudioSource _audioSource;

        public MoveOnSwitchBehaviour()
        {
            _controller = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
            _ui = new Lazy<LevelUiBehaviour>(FindObjectOfType<LevelUiBehaviour>);
        }

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.clip = Resources.Load<AudioClip>("Audio/gearTurning");
            AudioMixer mixer = Resources.Load("Audio/MainMixer") as AudioMixer;
            _audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("MovingParts")[0];
            _audioSource.volume = 0;
            _audioSource.Play();
        }

        protected override void FixedUpdate()
        {
            GoTowardsSwitchedPosition = _controller.Value.IsSwitched;
            
            if (IsMoving)
            {
                _audioSource.volume = 1f;
                
            } else
            {
                _audioSource.volume = 0f;
            }
            
            base.FixedUpdate();
        }
    }
}
