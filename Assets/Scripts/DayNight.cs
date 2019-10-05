using UnityEngine;

public class DayNight : MonoBehaviour
{
    public static DayNight Instance { private set; get; }

    private float _speed = 1f;
    private float _rotation;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _rotation += Time.deltaTime / 1.5f * _speed;
        if (_rotation >= 360f)
        {
            _rotation = 0f;
        }
        
        transform.rotation = Quaternion.Euler(
            _rotation, 0, 0            
        );

        if (Input.GetKeyDown(KeyCode.L))
        {
            _speed = _speed > 1.1f ? 1f : 50f;
        }
    }

    public bool IsDay() => _rotation <= 180f;
}