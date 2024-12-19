using UnityEngine;
using System.Collections;

public class AlignMiddleGear : MonoBehaviour
{
    public float rotationDuration = 2f;       // Duration of each 90-degree rotation
    public float rotationDelay = 0f;          // Delay before starting rotation
    public Vector3 rotationAxis = Vector3.right; // Axis of rotation (default X-axis)
    public float totalRotationAngle = 90f;    // Total angle for each rotation step
    public Transform player;                  // Reference to the player object

    private Quaternion initialRotation;       // Store the initial rotation of the mesh

    void Start()
    {
        initialRotation = transform.rotation; // Store the initial rotation state
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

            // Temporarily parent the player to the mesh
            if (player != null)
                player.SetParent(transform);

            // Calculate the target rotation
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.AngleAxis(-totalRotationAngle, rotationAxis); // Always anti-clockwise (-90°)

            // Smoothly rotate the mesh to the target angle over the specified duration
            while (elapsedTime < rotationDuration)
            {
                float t = elapsedTime / rotationDuration;

                // Smoothly rotate the mesh
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

                // Rotate the player in the opposite direction to maintain alignment
                if (player != null)
                    player.Rotate(-rotationAxis, (totalRotationAngle / rotationDuration) * Time.deltaTime, Space.World);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Snap to the exact final rotation to avoid rounding errors
            transform.rotation = endRotation;

            // Ensure final alignment for the player
            if (player != null)
                player.SetParent(null);

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
