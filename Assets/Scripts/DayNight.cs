using UnityEngine;

public class DayNight : MonoBehaviour
{
    public static DayNight Instance { private set; get; }

    private float _speed = 1f;
    private float _rotation;
    private bool _isDay = true;
    private int _nightsSurvived;
    
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

        if (_rotation >= 180 && _isDay)
        {
            StartNight();
            _isDay = false;
        } else if (_rotation <= 180 && !_isDay)
        {
            StartDay();
            _isDay = true;
        }
        
        transform.rotation = Quaternion.Euler(
            _rotation, 0, 0            
        );

        if (Input.GetKeyDown(KeyCode.L))
        {
            _speed = _speed > 1.1f ? 1f : 50f;
        }
    } 

    private void StartDay()
    {
        Waves.Instance.EndWave();
        _nightsSurvived++;
        
        Base.Instance.Regenerate();
        Player.Instance.GetComponent<Health>().HealFull();
    }

    private void StartNight()
    {
        Waves.Instance.StartWave();
    }

    public void Skip()
    {
        _rotation = 0;
    }
    
    public bool IsDay() => _rotation <= 180f;

    public int GetNightsSurvived() => _nightsSurvived;
}