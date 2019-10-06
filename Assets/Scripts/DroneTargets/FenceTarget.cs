using UnityEngine;

namespace DroneTargets
{
    public class FenceTarget : IDroneTarget
    {
        public bool CanAttack(Drone drone)
        {
            return Base.Instance.IsFenceEnabled() && Fence.Instance.GetComponent<Health>().Get() > 0;
        }

        public Transform GetTransform()
        {
            return Fence.Instance.transform;
        }

        public float GetAttackDistance()
        {
            return 9f;
        }
    }
}