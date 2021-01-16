using Assets.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallGeneratorBehaviour : MonoBehaviour
{
    public int RemainingBalls => _spawnCount;

    [SerializeField]
    private int _spawnCount = -1;
    [SerializeField]
    private float _interval = 2;
    [SerializeField]
    private float _delay = 0;
    [SerializeField]
    private GameObject _displayRoot;
    [SerializeField]
    private Text _displayTextBallsRemaining;
    [SerializeField]
    private Image _timeRemainingProgressCircle;

    private float _lastSpawnTime = -999;
    private Lazy<LevelControllerBehaviour> _levelController;

    private readonly Lazy<Camera> _camera;

    public BallGeneratorBehaviour()
    {
        _levelController = new Lazy<LevelControllerBehaviour>(FindObjectOfType<LevelControllerBehaviour>);
        _camera = new Lazy<Camera>(FindObjectOfType<Camera>);
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

    private void Update()
    {
        _displayRoot.transform.forward = _camera.Value.transform.forward;
        float spawnTimeRemaining = 0f;
        float maxSpawnTimeRemaining = 0f;

        //if the level has not yet started
        if (!_levelController.Value.LevelStarted)
        {
            maxSpawnTimeRemaining = spawnTimeRemaining = _delay > 0 ? _delay : _interval;
        }
        //if the level has started
        else
        {
            if (_delay > 0)
            {
                //if we have not spawned the first ball yet
                if (Time.time < _levelController.Value.LevelStartTime + _delay)
                {
                    maxSpawnTimeRemaining = _delay;
                    spawnTimeRemaining = _delay - (Time.time - _levelController.Value.LevelStartTime);
                }
                //after the first ball has spawned
                else
                {
                    maxSpawnTimeRemaining = _interval;
                    spawnTimeRemaining = _interval - (Time.fixedTime - _lastSpawnTime);
                }
            }
            //if there is no delay in the level
            else
            {
                maxSpawnTimeRemaining = _interval;
                spawnTimeRemaining = _interval - (Time.fixedTime - _lastSpawnTime);
            }
            
        }

        //_displayTextTimeRemaining.text = $"Time: {spawnTimeRemaining.ToString("F2")}";
        _timeRemainingProgressCircle.fillAmount = spawnTimeRemaining / maxSpawnTimeRemaining;
        _displayTextBallsRemaining.text = _spawnCount.ToString();
    }
}
