using UnityEngine;

namespace DroneTargets
{
    public class Gun2Target : IDroneTarget
    {
        public bool CanAttack(Drone drone)
        {
            var gun2 = Base.Instance.transform.Find("Gun2");
            var gunPosition = gun2.position;

            return Vector3.Distance(gunPosition, drone.transform.position) < 20f
                   && Base.Instance.IsGun2Enabled()
                   && gun2.GetComponent<Health>().Get() > 0
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
            return Base.Instance.transform.Find("Gun2");
        }

        public float GetAttackDistance()
        {
            return 3.4f;
        }
    }
}