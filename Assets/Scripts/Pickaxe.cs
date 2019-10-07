using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public AudioClip hittingClip;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartHitting()
    {
        Player.Instance.PlaySound(hittingClip, true);
        _animator.SetBool("hitting", true);
    }
    
    public void StopHitting()
    {
        Player.Instance.StopSound();
        _animator.SetBool("hitting", false);
    }

    public void AddStone()
    {
        Resources.Instance.Add(ResourceType.Stone, 1);
    }
}