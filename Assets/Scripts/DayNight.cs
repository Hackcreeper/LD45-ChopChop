using UnityEngine;

public class DayNight : MonoBehaviour
{
    public static DayNight Instance { private set; get; }

    public GameObject skipText;

    private float _speed = 1f;
    private float _rotation = 90f;
    private bool _isDay = true;
    private int _nightsSurvived;
    private bool _active = false;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Pause.Instance.IsPaused())
        {
            return;
        }
        
        skipText.SetActive(_isDay && _active);
        
        if (_isDay && _active && Input.GetKeyDown(KeyCode.T))
        {
            _rotation = 200;
        }

        if (_active)
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
        }

        transform.rotation = Quaternion.Euler(
            _rotation, 0, 0            
        );
    } 

    private void StartDay()
    {
        Waves.Instance.EndWave();
        _nightsSurvived++;
        
        Base.Instance.Regenerate();
        Player.Instance.GetComponent<Health>().HealFull();
    }

    public void Activate()
    {
        _active = true;
    }
    
    private void StartNight()
    {
        Waves.Instance.StartWave();
    }

    public void Skip()
    {
        _rotation = 0;
    }

    public void MakeNight()
    {
        _rotation = 200;
    }
    
    public bool IsDay() => _rotation <= 180f;

    public int GetNightsSurvived() => _nightsSurvived;
}