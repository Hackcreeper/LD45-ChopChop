using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { private set; get; }
    public float speed = 10.0f;

    private Rigidbody _rigidbody;
    private Transform _transform;

    public LayerMask terrainLayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
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
}
