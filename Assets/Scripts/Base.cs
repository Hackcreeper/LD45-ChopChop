using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public static Base Instance { private set; get; }

    public GameObject healthBar;
    
    private BaseLevel _level = BaseLevel.None;
    private Transform _transform;
    private Health _health;
    private bool _fenceEnabled;
    
    private void Awake()
    {
        Instance = this;
        _transform = transform;
        _health = GetComponent<Health>();
    }

    public void SetLevel(BaseLevel level)
    {
        _level = level;
        RerenderBase();
    }

    public BaseLevel GetLevel() => _level;

    public void RerenderBase()
    {
        RemoveOldBase();

        switch (_level)
        {
            case BaseLevel.None:
                break;
            case BaseLevel.WoodenHouse:
                CreateWoodenHouse();
                EnableAddons();
                break;
            case BaseLevel.StoneHouse:
                CreateStoneHouse();
                EnableAddons();
                break;
            default:
                Debug.LogError("Level not integrated!");
                break;
        }
    }

    private void CreateStoneHouse()
    {
        _transform.Find("StoneHouse").gameObject.SetActive(true);
    }

    private void CreateWoodenHouse()
    {
        _transform.Find("WoodenHouse").gameObject.SetActive(true);
    }

    private void RemoveOldBase()
    {
        for (var i = 0; i < _transform.childCount; i++)
        {
            _transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void EnableAddons()
    {
        healthBar.SetActive(_level > BaseLevel.None);

        if (_fenceEnabled)
        {
            _transform.Find("Fence").gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (_health.Get() > 0)
        {
            return;
        }
        
        ScoreTransmitter.Instance.SetScore(DayNight.Instance.GetNightsSurvived());
        ScoreTransmitter.Instance.SetReason(GameOverReason.BaseDestroyed);
        SceneManager.LoadScene("GameOver");
    }

    public void EnableFence()
    {
        _fenceEnabled = true;
        RerenderBase();
    }

    public bool IsFenceEnabled() => _fenceEnabled;
}

public enum BaseLevel
{
    None,
    WoodenHouse,
    StoneHouse,
    Tower
}