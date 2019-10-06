using UnityEngine;

namespace DroneTargets
{
    public interface IDroneTarget
    {
        bool CanAttack(Drone drone);
        
        Transform GetTransform();
        
        float GetAttackDistance();
    }
}