using System.Collections;
using System.Linq;
using DroneTargets;
using UnityEngine;

public class Drone : Interactable
{
    public float speed = 5f;
    public Texture redTexture;
    public MeshRenderer meshRenderer;
    public Animator spearAnimator;
    public AudioClip hitSound;
    public AudioSource hitSource;

    protected IDroneTarget[] Targets =
    {
        new PlayerTarget(),
        new FenceTarget(), 
        new Gun1Target(),
        new Gun2Target(),
        new Gun3Target(),
        new Gun4Target(),
        new BaseTarget()
    };

    protected Health Health;
    protected float Flashing;
    protected Rigidbody Rigidbody;
    protected BoxCollider BoxCollider;
    protected CapsuleCollider CapsuleCollider;
    protected Animator Animator;
    
    private Transform _transform;
    private float _attackTimer = 1f;
    private bool _running;
    private float _deathTimer = 2f;
    private Texture _originalTexture;
    private IDroneTarget _target;

    private void Start()
    {
        _transform = transform;
        Health = GetComponent<Health>();
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        BoxCollider = GetComponent<BoxCollider>();
        Animator = GetComponentInChildren<Animator>();
        _originalTexture = meshRenderer.material.mainTexture;
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

        Flashing -= Time.deltaTime;
        if (Flashing <= 0)
        {
            meshRenderer.material.mainTexture = _originalTexture;
        }

        if (Health.Get() <= 0)
        {
            _deathTimer -= Time.deltaTime;
            if (_deathTimer <= 0)
            {
                Destroy(gameObject);
            }

            return;
        }

        FindTarget();

        if (_target == null)
        {
            return;
        }

        RotateAndMove();
        Attack();
    }

    private void Attack()
    {
        if (Vector3.Distance(_transform.position, _target.GetTransform().position) > _target.GetAttackDistance() + 2)
        {
            return;
        }

        _attackTimer -= Time.deltaTime;
        if (_attackTimer > 0)
        {
            return;
        }

        _attackTimer = 10f;
        hitSource.Play();
        StartCoroutine(StartSpearAnimation());
    }

    private IEnumerator StartSpearAnimation()
    {
        spearAnimator.SetBool("attacking", true);
        yield return null;
        spearAnimator.SetBool("attacking", false);
    }

    private void RotateAndMove()
    {
        var lastFrame = _transform.rotation;

        _transform.LookAt(_target.GetTransform());
        var original = _transform.rotation.eulerAngles;
        var euler = Quaternion.Euler(0, original.y, 0);

        _transform.rotation = Quaternion.Lerp(lastFrame, euler, 5 * Time.deltaTime);

        if (Vector3.Distance(_transform.position, _target.GetTransform().position) < _target.GetAttackDistance())
        {
            return;
        }

        _transform.Translate(Time.deltaTime * speed * Vector3.forward, Space.Self);
    }

    private void FindTarget()
    {
        _target = Targets.FirstOrDefault(target => target.CanAttack(this));
    }

    public void Run()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        _running = true;
    }

    public virtual void TakeDamage(int amount = 1)
    {
        Player.Instance.PlaySound(hitSound);
        Health.Sub(amount);

        Flashing = .15f;
        meshRenderer.material.mainTexture = redTexture;

        Rigidbody.AddRelativeForce(0, 0, -10f, ForceMode.Impulse);
        ResetAttackTimer();

        if (Health.Get() > 0)
        {
            return;
        }

        BoxCollider.enabled = true;
        CapsuleCollider.enabled = false;
        Animator.enabled = false;
        Active = false;
        
        Waves.Instance.RemoveDrone(gameObject);

        Resources.Instance.Add(ResourceType.Metal, Random.Range(1, 5));
        Resources.Instance.Add(ResourceType.Oil, Random.Range(1, 5));
    }

    public override bool IsActive()
    {
        return base.IsActive() && Toolbar.Instance.GetActiveTool() == Tool.Axe;
    }

    public Transform GetTarget() => _target.GetTransform();

    public void ResetAttackTimer()
    {
        _attackTimer = 1f;
    }
}