using System;
using System.Security.Permissions;
using UnityEngine;

public class Drone : Interactable
{
    public float speed = 5f;
    public float searchDistance = 12f;
    
    private Transform _target;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (!_target)
        {
            FindTarget();
            return;
        }

        var lastFrame = _transform.rotation;
        
        _transform.LookAt(_target);
        var original = _transform.rotation.eulerAngles;
        var euler = Quaternion.Euler(0, original.y, 0);

        _transform.rotation = Quaternion.Lerp(lastFrame, euler, 5 * Time.deltaTime);
        _transform.Translate(Time.deltaTime * speed * Vector3.forward, Space.Self);
    }

    private void FindTarget()
    {
        // TODO: Check for base
        
        // Check for player
        if (Vector3.Distance(Player.Instance.transform.position, _transform.position) > searchDistance)
        {
            return;
        }

        _target = Player.Instance.transform;
    }
}
