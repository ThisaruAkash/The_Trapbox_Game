using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Ensure game time is normal before restarting
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeUI(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeUI");
    }
}
