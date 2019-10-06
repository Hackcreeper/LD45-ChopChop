using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Tree _activeTree;
    private Animator _animator;
    private bool _active;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetActiveTree(Tree tree)
    {
        _activeTree = tree;
    }

    public void StartCutting()
    {
        _animator.SetBool("cutting", true);
        _active = true;
    }
    
    public void MakeDamage()
    {
        if (!_activeTree.TakeDamage())
        {
            return;
        }

        _active = false;
    }

    private void Update()
    {
        if (_active)
        {
            _animator.SetBool("cutting", false);
        }
    }
    
    public bool IsActive() => _active;
}