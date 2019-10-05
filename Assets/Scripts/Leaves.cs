using UnityEngine;

public class Leaves : MonoBehaviour
{
    public LayerMask terrainLayer;
    public Tree tree;
    
    private void OnTriggerEnter(Collider other)
    {
        if (terrainLayer != (terrainLayer | (1 << other.gameObject.layer)))
        {
            return;
        }

        tree.Remove();
    }
}