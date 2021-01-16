using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Behaviours.Ui;
using System;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class BallAudioSystem : MonoBehaviour
{

    AudioSource audioSource;
    Rigidbody myRigidbody;
    float maxSoundVelocityMagnitudeSqr = 10f;
    float minPitch = 0.5f;
    float maxPitch = 1.0f;
    private bool airborne;
    public AudioClip hitSound;
    public AudioClip hitBallSound;
    Coroutine leaveCheck;
    Lazy<LevelUiBehaviour> _ui; 
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody>();
        audioSource.volume = 0;
        airborne = true;
        _ui = new Lazy<LevelUiBehaviour>(FindObjectOfType<LevelUiBehaviour>);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            
            AudioSource.PlayClipAtPoint(hitBallSound, collision.GetContact(0).point, percentBasedOnRelativeVelocityCalc(collision.relativeVelocity.sqrMagnitude, 0.5f));
        }
        if (airborne)
        {
            //Debug.Log("Playing Hit");
            airborne = false;
            AudioSource.PlayClipAtPoint(hitSound, collision.GetContact(0).point, percentBasedOnVelocityCalc(0.5f));
            //audioSource.PlayOneShot(hitSound, percentBasedOnVelocityCalc());
        }
        //audioSource.volume = 1f;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!airborne)
        {
            
            leaveCheck = StartCoroutine(interruptableLeaveCheck());
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {



        if (leaveCheck != null)
        {
            StopCoroutine(leaveCheck);
        }
        if (_ui.Value.pauseMenuActive())
        {
            audioSource.volume = 0;
        }
        else
        {
            float percentChange = percentBasedOnVelocityCalc(1f);
            audioSource.volume = percentChange;
            audioSource.pitch = minPitch + (percentChange * (maxPitch - minPitch));
        }
        
    }

    private float percentBasedOnVelocityCalc(float velocityMultiplier)
    {
        float rigidbodyVelocityMagnitude = myRigidbody.velocity.sqrMagnitude * velocityMultiplier;
        //Debug.Log("Velocity is" + rigidbodyVelocityMagnitude.ToString());
        return rigidbodyVelocityMagnitude / maxSoundVelocityMagnitudeSqr;
    }

    private float percentBasedOnRelativeVelocityCalc(float inMagnitude, float velocityMultiplier)
    {
        float rigidbodyVelocityMagnitude = inMagnitude * velocityMultiplier;
        //Debug.Log("Velocity is" + rigidbodyVelocityMagnitude.ToString());
        return rigidbodyVelocityMagnitude / maxSoundVelocityMagnitudeSqr;
    }

    IEnumerator interruptableLeaveCheck()
    {
        yield return new WaitForSeconds(0.2f);
        //Debug.Log("Left the zone");
        audioSource.volume = 0f;
        airborne = true;
    }
}
