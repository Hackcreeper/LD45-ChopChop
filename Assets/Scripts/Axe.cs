using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Tree _activeTree;
    private Drone _activeDrone;
    private Animator _animator;
    private bool _active;
    private bool _attacking;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetActiveTree(Tree tree)
    {
        _activeTree = tree;
    }

    public void SetActiveDrone(Drone drone)
    {
        _activeDrone = drone;
    }

    public void StartCutting()
    {
        _animator.SetBool("cutting", true);
        _active = true;
    }

    public void StartAttack()
    {
        _animator.SetBool("attacking", true);
        _attacking = true;
    }
    
    public void MakeDamage()
    {
        if (_active)
        {
            if (!_activeTree.TakeDamage())
            {
                return;
            }

            _active = false;   
        }

        if (_attacking)
        {
            _activeDrone.TakeDamage();
        }
    }

    private void Update()
    {
        if (_active)
        {
            _animator.SetBool("cutting", false);
        }

        if (_attacking)
        {
            _animator.SetBool("attacking", false);
        }
    }
    
    public bool IsActive() => _active;
}