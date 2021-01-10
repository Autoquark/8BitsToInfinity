using Assets.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGeneratorBehaviour : MonoBehaviour
{
    public int RemainingBalls => _spawnCount;

    [SerializeField]
    private int _spawnCount = -1;
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
            if(_spawnCount > 0)
            {
                _spawnCount--;
                if(_spawnCount == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.35f);
    }
}
