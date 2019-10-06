using UnityEngine;

public class Collectable : Interactable
{
    public ResourceType type;
    public int amount = 1;

    private bool _wasActive = true;

    private void Update()
    {
        if (IsActive() && !DayNight.Instance.IsDay())
        {
            Active = false;
            _wasActive = true;
        }

        if (_wasActive && DayNight.Instance.IsDay())
        {
            Active = true;
            _wasActive = false;
        }
        
        if (!Focus || !Input.GetMouseButtonDown(0) || !IsActive())
        {
            return;
        }
        
        Resources.Instance.Add(type, amount);
        Destroy(gameObject);
    }
}
