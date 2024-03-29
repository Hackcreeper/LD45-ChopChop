﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { private set; get; }
    public float speed = 10.0f;
    public Axe axe;
    public Pickaxe pickaxe;
    public BoxCollider fenceCollider;
    public BoxCollider houseCollider;
    public AudioClip pickupClip;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private Health _health;
    private bool _hasAxe;
    private bool _hasPickaxe;
    private AudioSource _audioSource;

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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _health = GetComponent<Health>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_health.Get() <= 0)
        {
            ScoreTransmitter.Instance.SetScore(DayNight.Instance.GetNightsSurvived());
            ScoreTransmitter.Instance.SetReason(GameOverReason.PlayerDied);
            SceneManager.LoadScene("GameOver");
            return;
        }

        if (axe.IsActive())
        {
            return;
        }
        
        var velocity = _rigidbody.velocity.y + Physics.gravity.y * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            velocity = 10f;
        }
        
        var vertical = Input.GetAxis("Vertical") * speed;
        var horizontal = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = transform.rotation * new Vector3(horizontal, velocity, vertical);
    }

    private bool IsGrounded()
    {
        var ray = new Ray(transform.position, -_transform.up);

        return Physics.Raycast(ray, out _, 1.6f, terrainLayer);
    }

    public bool HasAxe() => _hasAxe;
    
    public bool HasPickaxe() => _hasPickaxe;

    public void GainAxe()
    {
        _hasAxe = true;
        Toolbar.Instance.SetActiveTool(Tool.Axe);
    }
    
    public void GainPickaxe()
    {
        _hasPickaxe = true;
        Toolbar.Instance.SetActiveTool(Tool.Pickaxe);
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        _audioSource.clip = clip;
        _audioSource.loop = loop;
        _audioSource.Play();
    }

    public void StopSound()
    {
        _audioSource.loop = false;
        _audioSource.Stop();
    }
}