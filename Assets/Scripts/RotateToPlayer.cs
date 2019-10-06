using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    private Transform _transform;
    private Transform _player;

    private void Start()
    {
        _transform = transform;
        _player = Player.Instance.transform;
    }

    private void Update()
    {
        var lastFrame = _transform.rotation;

        _transform.LookAt(_player);
        var original = _transform.rotation.eulerAngles;
        var euler = Quaternion.Euler(0, original.y + 180, 0);

        _transform.rotation = Quaternion.Lerp(lastFrame, euler, 5 * Time.deltaTime);
    }
}