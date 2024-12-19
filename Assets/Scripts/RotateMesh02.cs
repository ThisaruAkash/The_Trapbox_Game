using UnityEngine;
using System.Collections;

public class RotateMesh02 : MonoBehaviour
{
    public float rotationDuration = 2f;       // Duration of each 90-degree rotation
    public float rotationDelay = 5f;          // Delay before starting rotation
    public Vector3 rotationAxis = Vector3.right; // Axis of rotation (default X-axis)
    public float totalRotationAngle = 90f;    // Total angle for each rotation step
    public Transform player;                  // Reference to the player object
    public Transform clockArm;                // Reference to the clock arm to exclude from alignment
    public Transform[] groundObjects;         // Ground objects to rotate with the mesh

    private Quaternion initialRotation;       // Store the initial rotation of the mesh
    private SoundManager soundManager;

    void Start()
    {
        initialRotation = transform.rotation; // Store the initial rotation state
        // Find the SoundManager
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found in the scene!");
        }
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

            // Calculate the target rotation
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.AngleAxis(-totalRotationAngle, rotationAxis); // Always anti-clockwise (-90°)

            // Temporarily parent the player and ground objects to the mesh
            if (player != null && player != clockArm)
                player.SetParent(transform);
            
                if (soundManager != null)
        {
            soundManager.PlaySFX("MeshRotate");
        }

            foreach (Transform ground in groundObjects)
            {
                if (ground != null)
                {
                    ground.SetParent(transform);
                }
            }

            // Smoothly rotate the mesh
            while (elapsedTime < rotationDuration)
            {
                float progress = elapsedTime / rotationDuration;

                // Smoothly rotate the mesh
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);

                // Keep the player aligned with the original rotation
                if (player != null && player != clockArm)
                    player.localRotation = Quaternion.Inverse(Quaternion.Slerp(startRotation, endRotation, progress));

                // Ensure clock arm remains unaffected
                if (clockArm != null)
                    clockArm.localRotation = Quaternion.Inverse(transform.rotation) * initialRotation;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Snap to the exact final rotation to avoid rounding errors
            transform.rotation = endRotation;

            // Detach the player and ground objects
            if (player != null && player != clockArm)
                player.SetParent(null);

            foreach (Transform ground in groundObjects)
            {
                if (ground != null)
                {
                    ground.SetParent(null);
                }
            }

            // Wait before starting the next rotation
            yield return new WaitForSeconds(rotationDelay);
        }
    }

    // Method to set rotation settings dynamically
    public void SetRotationSettings(
        float duration,
        float delay,
        Vector3 axis,
        float angle,
        Transform playerTransform,
        Transform clockArmTransform,
        Transform[] groundTransforms
    )
    {
        rotationDuration = duration;
        rotationDelay = delay;
        rotationAxis = axis;
        totalRotationAngle = angle;
        player = playerTransform;
        clockArm = clockArmTransform;
        groundObjects = groundTransforms;

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
