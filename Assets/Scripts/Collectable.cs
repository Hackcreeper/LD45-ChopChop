using UnityEngine;

public class Collectable : Interactable
{
    public ResourceType type;

    public int amount = 1;

    private void Update()
    {
        if (!Focus || !Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        Resources.Instance.Add(type, amount);
        Destroy(gameObject);
    }
}
