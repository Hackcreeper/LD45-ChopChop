using UnityEngine;

public class CrossbowSpear : MonoBehaviour
{
    private float speed = 20f;
    private int damage = 3;

    private Transform _target;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (!_target)
        {
            Destroy(gameObject);
            return;
        }
        
        RotateToTarget();
        transform.Translate(Time.deltaTime * speed * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Drone>() == null)
        {
            return;
        }
        
        other.GetComponent<Drone>().TakeDamage(damage);
        Destroy(gameObject);
    }

    private void RotateToTarget()
    {
        var lastFrame = _transform.rotation;

        _transform.LookAt(_target.transform);
        var original = _transform.rotation;

        _transform.rotation = Quaternion.Lerp(lastFrame, original, 5 * Time.deltaTime); 
    }
}