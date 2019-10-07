using UnityEngine;

public class Rock : Interactable
{
    private bool _hitting;
    
    private void Update()
    {
        if (IsActive() && !DayNight.Instance.IsDay())
        {
            Active = false;
        }
        
        if (DayNight.Instance.IsDay())
        {
            Active = true;
        }
        
        if (!Focus || !Input.GetMouseButton(0) || !IsActive())
        {
            if (_hitting)
            {
                Player.Instance.pickaxe.StopHitting();
                _hitting = false;
            }

            return;
        }

        if (_hitting)
        {
            return;
        }
        
        Player.Instance.pickaxe.StartHitting();
        _hitting = true;
    }
    
    public override bool IsActive()
    {
        return base.IsActive() && Toolbar.Instance.GetActiveTool() == Tool.Pickaxe;
    }
}