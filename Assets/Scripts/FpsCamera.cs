using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class FpsCamera : MonoBehaviour
{
    public float sensitivity = 5f;

    public float smoothing = 2f;

    public GameObject player;

    private Vector2 _mouseLook;
    private Vector2 _smoothVector;
    private Transform _transform;
    private Player _playerInstance;
    private readonly List<Interactable> _interactables = new List<Interactable>();

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _playerInstance = player.GetComponent<Player>();
    }

    private void Update()
    {
        if (_playerInstance.axe.IsActive())
        {
            _smoothVector = Vector2.zero;
            return;
        }
        
        HandleMovement();
        HandleCursor();
    }

    private void HandleCursor()
    {
        _interactables.ForEach(interactable => interactable.SetFocus(false));
        _interactables.Clear();
        
        var ray = new Ray(_transform.position, _transform.forward);
        var hits = Physics.RaycastAll(ray, 100);

        if (hits.Count(CheckHit()) > 0)
        {
            Crosshair.Instance.SetState(CursorState.HoveringOverInteractable);
            foreach (var hit in hits)
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (!interactable || hit.distance > interactable.distance)
                {
                    continue;
                }
                
                _interactables.Add(interactable);
            }
            
            _interactables.ForEach(interactable => interactable.SetFocus(true));
            
            return;
        }
        
        Crosshair.Instance.SetState(CursorState.Normal);
    }

    private static Func<RaycastHit, bool> CheckHit()
    {
        return hit => hit.collider.GetComponent<Interactable>() != null &&
                      hit.collider.GetComponent<Interactable>().distance >= hit.distance &&
                      hit.collider.GetComponent<Interactable>().IsActive();
    }

    private void HandleMovement()
    {
        var mouseDelta = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );

        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        _smoothVector.x = Mathf.Lerp(_smoothVector.x, mouseDelta.x, 1f / smoothing);
        _smoothVector.y = Mathf.Lerp(_smoothVector.y, mouseDelta.y, 1f / smoothing);

        _mouseLook += _smoothVector;
        _mouseLook.y = Mathf.Clamp(_mouseLook.y, -90, 90);

        _transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, player.transform.up);
    }
}
