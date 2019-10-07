using UnityEngine;

public class Door : Interactable
{
    private Animator _animator;
    private bool _open;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!Focus || !IsActive() || !Input.GetMouseButtonDown(0))
        {
            return;
        }

        _open = !_open;
        _animator.SetBool("open", _open);
        _audioSource.Play();
    }
}