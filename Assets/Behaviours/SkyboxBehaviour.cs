using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxBehaviour : MonoBehaviour
{
    //Code https://answers.unity.com/questions/651780/rotate-skybox-constantly.html
    public float speedMultiplier = 1f;

    private void Start()
    {
        GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speedMultiplier);
    }
}
