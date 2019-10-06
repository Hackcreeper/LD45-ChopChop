using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { private set; get; }
    public float speed = 10.0f;
    public Axe axe;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private Health _health;
    private bool _hasAxe;

    public LayerMask terrainLayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!SceneManager.GetSceneByName("Level").isLoaded)
        {
            SceneManager.LoadScene("Level", LoadSceneMode.Additive);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health.Get() <= 0)
        {
            ScoreTransmitter.Instance.Set(DayNight.Instance.GetNightsSurvived());
            SceneManager.LoadScene("GameOver");
            return;
        }

        if (axe.IsActive())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GainAxe();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Resources.Instance.Add(ResourceType.Wood, 10);
            Resources.Instance.Add(ResourceType.Stone, 10);
            Resources.Instance.Add(ResourceType.Oil, 10);
            Resources.Instance.Add(ResourceType.Bone, 10);
        }
        
        var vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        var horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(horizontal, 0, vertical);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigidbody.AddForce(Time.deltaTime * 14000f * transform.up);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO: Move to pause script when pause menu is added
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private bool IsGrounded()
    {
        var ray = new Ray(transform.position, -_transform.up);

        return Physics.Raycast(ray, out _, 1.6f, terrainLayer);
    }

    public bool HasAxe() => _hasAxe;

    public void GainAxe()
    {
        _hasAxe = true;
        axe.gameObject.SetActive(true);
    }
}