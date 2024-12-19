using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RotationTimer02 : MonoBehaviour
{
    [Header("Rotation Settings")]
    public RotateMesh02 meshRotator;  // Reference to the RotateMesh02 script

    [Header("UI Elements")]
    public Slider timerSlider;       // Slider to represent the timer bar
    public TextMeshProUGUI timerText; // Text to display remaining time

    private float rotationDelay;     // Delay time fetched from RotateMesh02
    private float timer;             // Timer to track the countdown

    private bool isResetting = false; // Tracks if the timer is resetting

    void Start()
    {
        // Ensure references are set
        if (meshRotator == null || timerSlider == null || timerText == null)
        {
            Debug.LogError("References to RotateMesh02, Timer Slider, or Timer Text are missing!");
            return;
        }

        // Fetch rotation delay from RotateMesh02
        rotationDelay = meshRotator.rotationDelay;

        // Initialize timer
        ResetTimer();
    }

    void Update()
    {
        // Handle countdown only if not resetting
        if (!isResetting)
        {
            timer -= Time.deltaTime;

            // Ensure timer doesn't go below 0
            if (timer <= 0)
            {
                timer = 0;

                // Reset timer for the next cycle
                if (!isResetting)
                {
                    ResetTimer();
                }
            }

            // Update UI elements
            UpdateUI();
        }
    }

    private void ResetTimer()
    {
        isResetting = true;

        // Reset timer to the rotation delay
        timer = rotationDelay;
        timerSlider.maxValue = rotationDelay;
        timerSlider.value = rotationDelay;

        // Update UI immediately
        UpdateUI();

        // Wait for the rotation to finish before allowing countdown
        StartCoroutine(WaitForRotation());
    }

    private System.Collections.IEnumerator WaitForRotation()
    {
        yield return new WaitForSeconds(meshRotator.rotationDuration);

        // Allow countdown to start again
        isResetting = false;
    }

    private void UpdateUI()
    {
        // Update slider and text with the current timer value
        timerSlider.value = timer;
        timerText.text = $"{Mathf.CeilToInt(timer)}s"; // Show integer seconds with "s"
    }
}
