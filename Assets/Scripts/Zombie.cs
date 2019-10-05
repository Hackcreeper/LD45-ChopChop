using System;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Transform _target;

    private void Start()
    {
        _target = Player.Instance.transform;
    }
    
     
}
