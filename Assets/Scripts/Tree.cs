using UnityEngine;

public class Tree : Interactable
{
    public Rigidbody ownRigidBody;

    private int _health = 3;

    private bool _cutting = false;
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
        
        if (!Focus || !Input.GetMouseButton(0) || !IsActive())
        {
            return;
        }

        if (!_cutting)
        {
            Player.Instance.axe.SetActiveTree(this);
            Player.Instance.axe.StartCutting();
            _cutting = true;

            return;
        }
    }

    public bool TakeDamage()
    {
        _health--;

        if (_health > 0)
        {
            return false;
        }

        Active = false;
        ownRigidBody.isKinematic = false;

        var x = Random.Range(0, 100) <= 50 ? -2 : 2;
        var z = Random.Range(0, 100) <= 50 ? -2 : 2;
        ownRigidBody.AddForce(x, 0, z, ForceMode.VelocityChange);

        return true;
    }

    public void Remove()
    {
        Resources.Instance.Add(ResourceType.Wood, 10);
        Destroy(ownRigidBody.gameObject);
    }
}