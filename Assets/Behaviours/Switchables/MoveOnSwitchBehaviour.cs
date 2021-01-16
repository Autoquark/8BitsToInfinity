using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    
namespace Assets.Behaviours.Switchables
{
    [RequireComponent(typeof(AudioSource))]
    class MoveOnSwitchBehaviour : MoveableBehaviour
    {
        private readonly Lazy<LevelControllerBehaviour> _controller;
        private AudioSource _audioSource;

        public MoveOnSwitchBehaviour()
        {
            _controller = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
        }

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.clip = Resources.Load<AudioClip>("Audio/gearTurning");
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
