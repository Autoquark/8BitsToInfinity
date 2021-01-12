using Assets.Behaviours;
using System;
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
    [SerializeField]
    private float _delay = 0;

    private float _lastSpawnTime = -999;
    private Lazy<LevelControllerBehaviour> _levelController;

    public BallGeneratorBehaviour()
    {
        _levelController = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!_levelController.Value.LevelStarted || Time.time < _levelController.Value.LevelStartTime + _delay)
        {
            return;
        }
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
