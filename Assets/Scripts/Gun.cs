using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform mounts;
    public GameObject spear;
    public GameObject spearPrefab;
    
    private float searchRadius = 15f;

    private Transform _target;
    private Transform _transform;
    private float _attackTimer = 3f;
    private float _respawnTimer;
    private Health _health;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _transform = transform;
        _health = GetComponent<Health>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_health.Get() <= 0)
        {
            Base.Instance.PlayDestroySound();
            gameObject.SetActive(false);
            return;
        }
        
        
        FindTarget();

        if (!_target)
        {
            return;
        }
        
        RotateToTarget();

        _attackTimer -= Time.deltaTime;
        _respawnTimer -= Time.deltaTime;

        if (_respawnTimer <= 0)
        {
            spear.SetActive(true);
        }
        
        if (_attackTimer > 0)
        {
            return;
        }

        Attack();
    }

    private void Attack()
    {
        // Hide original spear
        spear.SetActive(false);
        _respawnTimer = 2f;
        
        var newSpear = Instantiate(spearPrefab, spear.transform.position, spear.transform.rotation);
        newSpear.GetComponent<CrossbowSpear>().SetTarget(_target);
        
        _audioSource.Play();
        _attackTimer = 3f;
    }

    private void RotateToTarget()
    {
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