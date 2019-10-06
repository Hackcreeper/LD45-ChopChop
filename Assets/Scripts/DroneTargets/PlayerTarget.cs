using UnityEngine;

namespace DroneTargets
{
    public class PlayerTarget : IDroneTarget
    {
        public bool CanAttack(Drone drone)
        {
            return Vector3.Distance(Player.Instance.transform.position, drone.transform.position) < 12f;
        }

        public Transform GetTransform()
        {
            return Player.Instance.transform;
        }

        public float GetAttackDistance()
        {
            return 2.4f;
        }
    }
}