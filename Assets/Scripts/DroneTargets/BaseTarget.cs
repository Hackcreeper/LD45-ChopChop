using UnityEngine;

namespace DroneTargets
{
    public class BaseTarget : IDroneTarget
    {
        public bool CanAttack(Drone drone)
        {
            return Base.Instance.GetLevel() > BaseLevel.None;
        }

        public Transform GetTransform()
        {
            return Base.Instance.transform;
        }

        public float GetAttackDistance()
        {
            return 6f;
        }
    }
}