using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    private float sensitivity = 5f;

    private float smoothing = 2f;

    public GameObject player;

    private Vector2 mouseLook;

    private Vector2 smoothVector;

    private void Update()
    {
        var mouseDelta = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );

        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothVector.x = Mathf.Lerp(smoothVector.x, mouseDelta.x, 1f / smoothing);
        smoothVector.y = Mathf.Lerp(smoothVector.y, mouseDelta.y, 1f / smoothing);

        mouseLook += smoothVector;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
