using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    [SerializeField] private int minSpeed = 12;
    [SerializeField] private int maxSpeed = 16;
    [SerializeField] private int maxTorque = 10;
    [SerializeField] private int xRange = 4;
    [SerializeField] private int ySpawnPos = -6;
    [SerializeField] private ParticleSystem explosionFx;
    
    private Rigidbody _targetRb;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _targetRb = GetComponent<Rigidbody>();
        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPos();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private int RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        if (_gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
            if (gameObject.CompareTag("Bad"))
            {
                _gameManager.GameOver();
            }
            _gameManager.UpdateScore(5);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
