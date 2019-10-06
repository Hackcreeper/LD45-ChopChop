using UnityEngine;

public class Drone : Interactable
{
    public float speed = 5f;
    public float searchDistance = 12f;
    
    private Transform _target;
    private Transform _transform;
    private float _attackTimer = 1f;
    private bool _running;
    private float _flashing;
    private Health _health;
    private MeshRenderer[] _meshRenderer;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private CapsuleCollider _capsuleCollider;
    private float _deathTimer = 2f;

    private void Start()
    {
        _transform = transform;
        _health = GetComponent<Health>();
        _meshRenderer = GetComponentsInChildren<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (_running)
        {
            _transform.Translate(0, speed * Time.deltaTime * 5f, 0);
            if (Vector3.Distance(_transform.position, Player.Instance.transform.position) >= 300f)
            {
                Destroy(gameObject);
            }
            
            return;
        }

        if (Focus && IsActive() && Input.GetMouseButtonDown(0))
        {
            Player.Instance.axe.SetActiveDrone(this);
            Player.Instance.axe.StartAttack();
        }

        _flashing -= Time.deltaTime;
        if (_flashing <= 0)
        {
            foreach (var meshRenderer in _meshRenderer)
            {
                meshRenderer.material.color = Color.white;
            }
        }

        if (_health.Get() <= 0)
        {
            _deathTimer -= Time.deltaTime;
            if (_deathTimer <= 0)
            {
                Destroy(gameObject);
            }
            
            return;
        }
        
        if (!_target)
        {
            FindTarget();
            return;
        }

        RotateAndMove();
        Attack();
    }

    private void Attack() 
    {
        if (Vector3.Distance(_transform.position, _target.position) > 4f)
        {
            return;
        }
        
        _attackTimer -= Time.deltaTime;
        if (_attackTimer > 0)
        {
            return;
        }

        _attackTimer = 1f;
        _target.GetComponent<Health>().Sub(1);
    }

    private void RotateAndMove()
    {
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

    public void Run()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        _running = true;
    }

    public void TakeDamage()
    {
        _health.Sub();

        _flashing = .15f;
        foreach (var meshRenderer in _meshRenderer)
        {
            if (!meshRenderer)
            {
                continue;
            }
            
            meshRenderer.material.color = Color.red;
        }
        
        _rigidbody.AddRelativeForce(0, 0, -10f, ForceMode.Impulse);
        _attackTimer = 1f;
        
        if (_health.Get() > 0)
        {
            return;
        }

        _boxCollider.enabled = true;
        _capsuleCollider.enabled = false;
        Active = false;
        
        Resources.Instance.Add(ResourceType.Bone, Random.Range(1, 5));
        Resources.Instance.Add(ResourceType.Oil, Random.Range(2, 10));
    }

    public override bool IsActive()
    {
        return base.IsActive() && Player.Instance.HasAxe();
    }
}
