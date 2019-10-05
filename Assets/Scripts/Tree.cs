using UnityEngine;

public class Tree : Interactable
{
    public Rigidbody ownRigidBody;
    
    private int _health = 3;
    private float _animationTimer = 1f;

    private void Update()
    {
        if (!Focus || !Input.GetMouseButton(0) || !IsActive())
        {
            return;
        }

        // TODO: Use real animation
        _animationTimer -= Time.deltaTime;
        if (_animationTimer > 0)
        {
            return;
        }

        _health--;
        _animationTimer = 1f;

        if (_health > 0)
        {
            return;
        }

        Active = false;
        ownRigidBody.isKinematic = false;

        var x = Random.Range(0, 100) <= 50 ? -2 : 2;
        var z = Random.Range(0, 100) <= 50 ? -2 : 2;

        ownRigidBody.AddForce(x, 0, z, ForceMode.VelocityChange);
    }

    public void Remove()
    {
        Resources.Instance.Add(ResourceType.Wood, 10);
        Destroy(ownRigidBody.gameObject);
    }
}