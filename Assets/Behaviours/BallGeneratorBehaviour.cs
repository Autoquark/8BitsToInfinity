using Assets.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGeneratorBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _interval = 2;

    private float _lastSpawnTime = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.fixedTime - _lastSpawnTime > _interval)
        {
            Instantiate(CommonPrefabs.Instance.Ball, transform.position, Quaternion.identity);
            _lastSpawnTime = Time.fixedTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.35f);
    }
}
