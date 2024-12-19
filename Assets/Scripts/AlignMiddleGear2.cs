using UnityEngine;
using System.Collections;

public class AlignMiddleGear2 : MonoBehaviour
{
    public float rotationDuration = 2f;       // Duration of each 20-degree rotation
    public float rotationDelay = 0f;          // Delay before starting rotation
    public Vector3 rotationAxis = Vector3.right; // Axis of rotation (default X-axis)
    public float totalRotationAngle = 20f;    // Angle for each rotation step
    public Transform player;                  // Reference to the player object

    private Quaternion initialRotation;       // Store the initial rotation of the gear
    private Quaternion playerInitialRotation; // Store the initial rotation of the player
    private int currentStep = 0;              // Tracks the current step in the pattern
    private int totalSteps = 24;              // Total steps in the rotation pattern (6 + 12 + 6)

    void Start()
    {
        initialRotation = transform.rotation; // Store the initial rotation state of the gear
        if (player != null)
            playerInitialRotation = player.rotation; // Store the player's initial rotation

        if (rotationDelay > 0)
        {
            Invoke(nameof(StartRotation), rotationDelay); // Delay the start if specified
        }
        else
        {
            StartRotation(); // Start immediately
        }
    }

    private void StartRotation()
    {
        StartCoroutine(SmoothRotate()); // Start continuous rotation
    }

    private IEnumerator SmoothRotate()
    {
        while (true) // Infinite loop for continuous rotation
        {
            float elapsedTime = 0f;

            // Determine rotation direction based on the current step
            bool isClockwise = currentStep >= 6 && currentStep < 18;

            // Calculate the target rotation
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.AngleAxis(
                isClockwise ? totalRotationAngle : -totalRotationAngle,
                rotationAxis
            );

            // Temporarily parent the player to the gear
            if (player != null)
                player.SetParent(transform);

            // Smoothly rotate the gear
            while (elapsedTime < rotationDuration)
            {
                float t = elapsedTime / rotationDuration;

                // Smoothly interpolate the gear's rotation
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

                // Keep the player upright by resetting its world rotation
                if (player != null)
                {
                    player.rotation = playerInitialRotation;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Snap to the exact final rotation to avoid rounding errors
            transform.rotation = endRotation;

            // Ensure the player's alignment remains upright
            if (player != null)
            {
                player.SetParent(null); // Detach the player from the gear
                player.rotation = playerInitialRotation; // Reset the player's world rotation
            }

            // Increment the current step
            currentStep++;

            // Reset the pattern after completing all steps
            if (currentStep >= totalSteps)
            {
                currentStep = 0;
            }

            // Wait before starting the next rotation
            yield return new WaitForSeconds(rotationDelay);
        }
    }

    // Method to set rotation settings dynamically
    public void SetRotationSettings(float duration, float delay, Vector3 axis, float angle, Transform playerTransform)
    {
        rotationDuration = duration;
        rotationDelay = delay;
        rotationAxis = axis;
        totalRotationAngle = angle;
        player = playerTransform;

        // Reset rotation and restart the process
        StopAllCoroutines();
        transform.rotation = initialRotation;
        if (player != null)
            playerInitialRotation = player.rotation; // Update the player's initial rotation

        if (rotationDelay > 0)
        {
            Invoke(nameof(StartRotation), rotationDelay);
        }
        else
        {
            StartRotation();
        }
    }
}
