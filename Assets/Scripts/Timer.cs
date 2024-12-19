using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class RotationTimer : MonoBehaviour
{
    [Header("Rotation Settings")]
    public MeshRotator meshRotator;   // Reference to the RotateMesh script

    [Header("UI Elements")]
    public Slider timerSlider;       // Slider to represent the timer bar
    public TextMeshProUGUI timerText; // Text to display remaining time

    private float rotationDelay;     // Rotation delay from RotateMesh
    private float rotationDuration; // Rotation duration from RotateMesh
    private float timer;             // Timer for countdown

    private bool isResetting = false; // Tracks if timer is resetting

    void Start()
    {
        // Ensure the required components are assigned
        if (meshRotator == null || timerSlider == null || timerText == null)
        {
            Debug.LogError("References to RotateMesh, Slider, or Text are missing!");
            return;
        }

        // Fetch rotation settings from RotateMesh
        rotationDelay = meshRotator.rotationDelay;
        rotationDuration = meshRotator.rotationDuration;

        // Initialize timer
        ResetTimer();
    }

    void Update()
    {
        // Update the timer only when the mesh is not rotating
        if (!meshRotator.IsRotating() && !isResetting)
        {
            timer -= Time.deltaTime;

            // Ensure timer does not go below zero
            if (timer <= 0)
            {
                timer = 0;

                if (!isResetting)
                {
                    ResetTimer(); // Reset timer for next rotation cycle
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

        // Wait for the mesh rotation duration before allowing countdown
        StartCoroutine(WaitForRotation());
    }

    private System.Collections.IEnumerator WaitForRotation()
    {
        yield return new WaitForSeconds(rotationDuration);

        // Allow the countdown to resume
        isResetting = false;
    }

    private void UpdateUI()
    {
        // Update slider value and text
        timerSlider.value = timer;
        timerText.text = $"{Mathf.CeilToInt(timer)}s"; // Show whole seconds with "s"
    }
}
