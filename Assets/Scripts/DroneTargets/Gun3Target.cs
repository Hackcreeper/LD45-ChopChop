using UnityEngine;

namespace DroneTargets
{
    public class Gun3Target : IDroneTarget
    {
        public bool CanAttack(Drone drone)
        {
            var gun3 = Base.Instance.transform.Find("Gun3");
            var gunPosition = gun3.position;

            return Vector3.Distance(gunPosition, drone.transform.position) < 30f
                   && gun3.GetComponent<Health>().Get() > 0
                   && !InFence(gunPosition);
        }
        
        private static bool InFence(Vector3 gunPosition)
        {
            var player = Player.Instance;

            return Base.Instance.IsFenceEnabled()
                   && Fence.Instance.GetComponent<Health>().Get() > 0
                   && IsInCollider(player.fenceCollider.bounds, gunPosition);
        }
        
        private static bool IsInCollider(Bounds bounds, Vector3 position)
        {
            var l = bounds.center.x - bounds.extents.x;
            var r = bounds.center.x + bounds.extents.x;
            var b = bounds.center.z - bounds.extents.z;
            var t = bounds.center.z + bounds.extents.z;
            var x = position.x;
            var y = position.z;

            var inFence = x > l && x < r && y > b && y < t;
            return inFence;
        }

        public Transform GetTransform()
        {
            return Base.Instance.transform.Find("Gun3");
        }

        public float GetAttackDistance()
        {
            return 3.4f;
        }
    }
}