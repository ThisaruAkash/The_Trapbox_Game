using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelStartDisplay : MonoBehaviour
{
    [Header("UI Setup")]
    public Image levelImage;          // The UI Image component
    public Sprite[] levelSprites;     // Array of level images (assign in Inspector)
    public float displayTime = 2f;    // Time in seconds to display the image

    private void Start()
    {
        ShowLevelImage();
    }

    private void ShowLevelImage()
    {
        // Get the current level index (adjust based on your level loading logic)
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        // Ensure the level index matches the array bounds
        if (currentLevelIndex >= 0 && currentLevelIndex < levelSprites.Length)
        {
            // Set the sprite for the current level
            levelImage.sprite = levelSprites[currentLevelIndex];
        }
        else
        {
            Debug.LogWarning("No sprite assigned for this level in the LevelStartDisplay script.");
        }

        // Enable the image and start the timer to hide it
        levelImage.gameObject.SetActive(true);
        Invoke(nameof(HideLevelImage), displayTime);
    }

    private void HideLevelImage()
    {
        // Hide the image after the timer
        levelImage.gameObject.SetActive(false);
    }
}
