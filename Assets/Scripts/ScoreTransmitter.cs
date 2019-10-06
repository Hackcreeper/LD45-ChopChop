using UnityEngine;

public class ScoreTransmitter : MonoBehaviour
{
    public static ScoreTransmitter Instance { private set; get; }

    private int _score;
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Set(int score)
    {
        _score = score;
    }

    public int Get() => _score;
}