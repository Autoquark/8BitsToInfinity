using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCubeRotation : MonoBehaviour
{
    public float maxTorque = 100000f;
    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(0f, maxTorque);
        float randomY = Random.Range(0f, maxTorque);
        float randomZ = Random.Range(0f, maxTorque);
        GetComponent<Rigidbody>().AddTorque(new Vector3(randomX, randomY, randomZ));
    }

    
}
