using UnityEngine;

namespace DroneTargets
{
    public class PlayerTarget : IDroneTarget
    {
        public bool CanAttack(Drone drone)
        {
            return Vector3.Distance(Player.Instance.transform.position, drone.transform.position) < 12f
                   && !InFence()
                   && !InHouse();
        }

        private static bool InFence()
        {
            var player = Player.Instance;

            return Base.Instance.IsFenceEnabled()
                   && Fence.Instance.GetComponent<Health>().Get() > 0
                   && IsInCollider(player.fenceCollider.bounds, player.transform.position);
        }

        private static bool InHouse()
        {
            var player = Player.Instance;

            return Base.Instance.GetLevel() > BaseLevel.None
                   && IsInCollider(player.houseCollider.bounds, player.transform.position);
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
            return Player.Instance.transform;
        }

        public float GetAttackDistance()
        {
            return 2.4f;
        }
    }
}