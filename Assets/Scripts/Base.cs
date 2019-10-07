using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public static Base Instance { private set; get; }

    public GameObject healthBar;
    public AudioClip destroyClip;
    public AudioClip buildClip;
    public int healthWood = 15;
    public int healthStone = 40;

    private BaseLevel _level = BaseLevel.None;
    private Transform _transform;
    private Health _health;
    private bool _fenceEnabled;
    private bool _gun1Enabled;
    private bool _gun2Enabled;
    private bool _gun3Enabled;
    private bool _gun4Enabled;
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _transform = transform;
        _health = GetComponent<Health>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetLevel(BaseLevel level)
    {
        _level = level;

        switch (level)
        {
            case BaseLevel.None:
                break;
            case BaseLevel.WoodenHouse:
                _health.maxHealth = healthWood;
                break;
            case BaseLevel.StoneHouse:
                _health.maxHealth = healthStone;
                break;
            default:
                Debug.LogError("Base level not implemented!");
                break;
        }
        
        _health.HealFull();
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

        if (_gun1Enabled)
        {
            _transform.Find("Gun1").gameObject.SetActive(true);
        }

        if (_gun2Enabled)
        {
            _transform.Find("Gun2").gameObject.SetActive(true);
        }

        if (_gun3Enabled)
        {
            _transform.Find("Gun3").gameObject.SetActive(true);
        }

        if (_gun4Enabled)
        {
            _transform.Find("Gun4").gameObject.SetActive(true);
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

    public void EnableGun1()
    {
        _gun1Enabled = true;
        RerenderBase();
    }

    public bool IsGun1Enabled() => _gun1Enabled;

    public void EnableGun2()
    {
        _gun2Enabled = true;
        RerenderBase();
    }

    public bool IsGun2Enabled() => _gun2Enabled;

    public void EnableGun3()
    {
        _gun3Enabled = true;
        RerenderBase();
    }

    public bool IsGun3Enabled() => _gun3Enabled;

    public void EnableGun4()
    {
        _gun4Enabled = true;
        RerenderBase();
    }

    public bool IsGun4Enabled() => _gun4Enabled;

    public void Regenerate()
    {
        _health.HealFull();

        if (_fenceEnabled)
        {
            Fence.Instance.GetComponent<Health>().HealFull();
        }

        if (_gun1Enabled)
        {
            _transform.Find("Gun1").GetComponent<Health>().HealFull();
        }

        if (_gun2Enabled)
        {
            _transform.Find("Gun2").GetComponent<Health>().HealFull();
        }

        if (_gun3Enabled)
        {
            _transform.Find("Gun3").GetComponent<Health>().HealFull();
        }

        if (_gun4Enabled)
        {
            _transform.Find("Gun4").GetComponent<Health>().HealFull();
        }

        RerenderBase();
    }

    public void PlayDestroySound()
    {
        _audioSource.clip = destroyClip;
        _audioSource.Play();
    }
    
    public void PlayBuildSound()
    {
        _audioSource.clip = buildClip;
        _audioSource.Play();
    }
}

public enum BaseLevel
{
    None,
    WoodenHouse,
    StoneHouse
}