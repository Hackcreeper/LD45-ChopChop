using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform mounts;
    private float searchRadius = 15f;
    
    private Transform _target;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        FindTarget();

        if (!_target)
        {
            return;
        }
        
        var lastFrame = mounts.rotation;

        mounts.LookAt(_target.transform);
        var original = mounts.rotation.eulerAngles;
        var euler = Quaternion.Euler(-90, 0, original.y);

        mounts.rotation = Quaternion.Lerp(lastFrame, euler, 5 * Time.deltaTime);
    }

    private void FindTarget()
    {
        Waves.Instance.GetDrones().ForEach(drone =>
        {
            if (Vector3.Distance(drone.transform.position, _transform.position) > searchRadius)
            {
                return;
            }

            _target = drone.transform;
        });
    }
}