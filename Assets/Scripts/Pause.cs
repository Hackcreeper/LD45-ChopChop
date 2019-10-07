using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance { private set; get; }

    public GameObject pauseCanvas;
    
    private bool _paused;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.Backspace))
        {
            return;
        }

        _paused = !_paused;

        pauseCanvas.SetActive(_paused);
        
        if (_paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool IsPaused() => _paused;
}