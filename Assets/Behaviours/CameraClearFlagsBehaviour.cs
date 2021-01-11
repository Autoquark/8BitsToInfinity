using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClearFlagsBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
    }

    
}
