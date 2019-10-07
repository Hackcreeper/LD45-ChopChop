using UnityEngine;

public class Spear : MonoBehaviour
{
    public Drone drone;
    public AudioClip playerHitSound;
    
    public void Hit()
    {
        drone.GetTarget().GetComponent<Health>().Sub();
        if (drone.GetTarget().GetComponent<Player>() != null)
        {
            Player.Instance.PlaySound(playerHitSound);
        }
        
        drone.ResetAttackTimer();
    }
}
