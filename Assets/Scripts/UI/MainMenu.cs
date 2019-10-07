using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.anyKeyDown)
            {
                return;
            }

            SceneManager.LoadScene("Game");
        }
    }
}