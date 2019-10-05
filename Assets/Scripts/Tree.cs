using UnityEngine;

public class Tree : Interactable
{
    private int _health = 3;

    private bool _hovering;

    private float _animationTimer = 1f;

    private void OnMouseEnter()
    {
        _hovering = true;
    }

    private void OnMouseExit()
    {
        _hovering = false;
    }

    private void Update()
    {
        if (!_hovering || !Input.GetMouseButton(0))
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
        
        Resources.Instance.Add(ResourceType.Wood, 10);
        Destroy(gameObject);
    }
}
