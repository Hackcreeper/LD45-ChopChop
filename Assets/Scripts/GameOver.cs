using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    public Text reasonText;
    
    public int score;
    public GameOverReason reason;

    private void Awake()
    {
        score = ScoreTransmitter.Instance.GetScore();
        reason = ScoreTransmitter.Instance.GetReason();
    }

    private void Start()
    {
        scoreText.text = $"You survived {score} nights!";
        reasonText.text = reason == GameOverReason.PlayerDied
            ? "You died!"
            : "Your base was destroyed!";
        
        Analytics.CustomEvent("game_over", new Dictionary<string, object>
        {
            {
                "score", score
            },
            {
                "reason", reason
            }
        });
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        Destroy(ScoreTransmitter.Instance.gameObject);
        SceneManager.LoadScene("Game");
    }
}