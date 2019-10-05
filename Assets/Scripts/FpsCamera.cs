using System;
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

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        HandleMovement();
        HandleCursor();
    }

    private void HandleCursor()
    {
        var ray = new Ray(_transform.position, _transform.forward);
        var hits = Physics.RaycastAll(ray, 100);
        
        var state = hits.Count(CheckHit()) > 0
            ? CursorState.HoveringOverInteractable
            : CursorState.Normal;
        
        Crosshair.Instance.SetState(state);
    }

    private static Func<RaycastHit, bool> CheckHit()
    {
        return hit => hit.collider.GetComponent<Interactable>() != null &&
                      hit.collider.GetComponent<Interactable>().distance >= hit.distance;
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
