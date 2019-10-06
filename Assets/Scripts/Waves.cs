using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Waves : MonoBehaviour
{
    public static Waves Instance { private set; get; }

    public GameObject dronePrefab;
    private float waveMultiplicator = 1.4f;
    
    private int _droneAmount = 4;
    private int _currentWave = 1;

    private GameObject[] _spawner;
    private readonly List<GameObject> _drones = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _spawner = GameObject.FindGameObjectsWithTag("Spawner");
    }
    
    public void StartWave()
    {
        for (var i = 0; i < _droneAmount; i++)
        {
            var spawner = SelectRandomSpawner();

            _drones.Add(Instantiate(dronePrefab, spawner.transform.position, Quaternion.identity));
        }
    }

    public void EndWave()
    {
        _currentWave++;
        _droneAmount = Mathf.CeilToInt(_droneAmount * waveMultiplicator);
        
        _drones.ForEach(drone => drone.GetComponent<Drone>().Run());
        _drones.Clear();
    }

    private GameObject SelectRandomSpawner()
    {
        return _spawner[Random.Range(0, _spawner.Length - 1)];
    }
}
