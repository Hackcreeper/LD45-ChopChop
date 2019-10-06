using UnityEngine;

public class Spear : MonoBehaviour
{
    public Drone drone;
    
    public void Hit()
    {
        drone.GetTarget().GetComponent<Health>().Sub();
        drone.ResetAttackTimer();
    }
}
