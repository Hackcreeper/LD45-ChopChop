using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Waves : MonoBehaviour
{
    public static Waves Instance { private set; get; }

    public GameObject dronePrefab;
    public GameObject skipToDayInfo;
    
    private float waveMultiplicator = 1.4f;
    
    private int _droneAmount = 4;
    private bool _waveRunning;

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

        _waveRunning = true;
    }

    public void EndWave()
    {
        _droneAmount = Mathf.CeilToInt(_droneAmount * waveMultiplicator);
        
        _drones.ForEach(drone =>
        {
            if (drone)
            {
                drone.GetComponent<Drone>().Run();
            }
        });
        _drones.Clear();

        _waveRunning = false;
    }

    public List<GameObject> GetDrones() => _drones;

    public void RemoveDrone(GameObject drone)
    {
        _drones.Remove(drone);
    }
    
    private GameObject SelectRandomSpawner()
    {
        return _spawner[Random.Range(0, _spawner.Length - 1)];
    }

    private void Update()
    {
        if (_waveRunning && _drones.Count == 0)
        {
            skipToDayInfo.SetActive(true);
            if (!Input.GetKeyDown(KeyCode.T))
            {
                return;
            }
            
            DayNight.Instance.Skip();

            return;
        }
        
        skipToDayInfo.SetActive(false);
    }
}
