using System;
using UnityEngine;

public class Fence : MonoBehaviour
{
    public static Fence Instance { get; private set; }

    private Health _health;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health.Get() > 0)
        {
            return;
        }
        
        gameObject.SetActive(false);
    }
}