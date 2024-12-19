using UnityEngine;
using UnityEngine.SceneManagement; // For loading levels

public class LevelComplete : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private string playerTag = "Player"; // Tag assigned to the player

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player reached the door. Loading next level...");
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Calculate the next scene index
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene by its index
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("This is the last level. No more levels to load.");
        }
    }
}
