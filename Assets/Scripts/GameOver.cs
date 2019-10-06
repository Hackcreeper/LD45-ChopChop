using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        scoreText.text = $"You survived {ScoreTransmitter.Instance.Get()} nights!";
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