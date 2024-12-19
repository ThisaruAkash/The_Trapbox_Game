using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMechanics : MonoBehaviour
{
    [Header("Game Settings")]
    public string obstacleTag = "Obstacle"; // Tag for obstacles
    public GameObject gameOverUI;          // Reference to the Game Over UI panel

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag))
        {
            // Show the Game Over UI
            gameOverUI.SetActive(true);

            // Optional: Pause the game
            Time.timeScale = 0f;
        }
    }
}
