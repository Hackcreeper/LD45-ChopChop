using System.Collections;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public AudioClip cuttingClip;
    
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
        Player.Instance.PlaySound(cuttingClip);
        _active = true;
        StartCoroutine(StartCuttingAnimation());
    }

    public void StartAttack()
    {
        _attacking = true;
        StartCoroutine(StartAttackingAnimation());
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

    private IEnumerator StartCuttingAnimation()
    {
        _animator.SetBool("cutting", true);
        yield return null;
        _animator.SetBool("cutting", false);
    }
    
    private IEnumerator StartAttackingAnimation()
    {
        _animator.SetBool("attacking", true);
        yield return null;
        _animator.SetBool("attacking", false);
    }
    
    public bool IsActive() => _active;
}