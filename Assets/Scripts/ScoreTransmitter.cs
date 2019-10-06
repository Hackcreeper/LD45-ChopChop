using UnityEngine;

public class ScoreTransmitter : MonoBehaviour
{
    public static ScoreTransmitter Instance { private set; get; }

    private int _score;
    private GameOverReason _reason;
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetScore(int score)
    {
        _score = score;
    }

    public int GetScore() => _score;
    
    public void SetReason(GameOverReason reason)
    {
        _reason = reason;
    }

    public GameOverReason GetReason() => _reason;
}

public enum GameOverReason
{
    PlayerDied,
    BaseDestroyed
}