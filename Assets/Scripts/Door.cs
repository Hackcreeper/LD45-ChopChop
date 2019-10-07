using UnityEngine;

public class Door : Interactable
{
    private Animator _animator;
    private bool _open = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (!Focus || !IsActive() || !Input.GetMouseButtonDown(0))
        {
            return;
        }

        _open = !_open;
        _animator.SetBool("open", _open);
    }
}