using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayAudio(AudioClip audioClip)
    {
        DontDestroyOnLoad(gameObject);
        audioSource.PlayOneShot(audioClip);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
