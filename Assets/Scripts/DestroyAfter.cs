using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float timer = 3f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f)
        {
            return;
        }
        
        Destroy(gameObject);
    }
}