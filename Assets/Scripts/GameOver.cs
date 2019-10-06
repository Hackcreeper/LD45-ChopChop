using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    public int score;

    private void Awake()
    {
        score = ScoreTransmitter.Instance.Get();
    }

    private void Start()
    {
        scoreText.text = $"You survived {score} nights!";
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