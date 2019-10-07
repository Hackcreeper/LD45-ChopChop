using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartHitting()
    {
        _animator.SetBool("hitting", true);
    }
    
    public void StopHitting()
    {
        _animator.SetBool("hitting", false);
    }

    public void AddStone()
    {
        Resources.Instance.Add(ResourceType.Stone, 1);
    }
}