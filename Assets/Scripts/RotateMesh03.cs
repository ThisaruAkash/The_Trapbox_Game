using UnityEngine;
using System.Collections;

public class RotateMesh03 : MonoBehaviour
{
    public float rotationDuration = 2f;        // Duration of each 20-degree rotation
    public float rotationDelay = 0f;           // Delay between rotations
    public Vector3 rotationAxis = Vector3.right; // Axis of rotation (default X-axis)
    public float rotationStepAngle = 20f;      // Angle for each rotation step
    public Transform player;                   // Reference to the player object
    public Transform clockArm;                 // Reference to the clock arm to exclude from alignment
    public Transform[] groundObjects;          // Ground objects to rotate with the mesh

    private Quaternion initialRotation;        // Store the initial rotation of the mesh
    private int currentStep = 0;               // Tracks the current step in the pattern
    private int totalSteps = 24;   
     private SoundManager soundManager;            // Total steps in the rotation pattern (6 + 12 + 6)

    void Start()
    {
        initialRotation = transform.rotation; // Store the initial rotation state

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

            // Determine rotation direction based on the current step
            bool isClockwise = currentStep >= 6 && currentStep < 18;

            // Calculate the target rotation
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.AngleAxis(
                isClockwise ? rotationStepAngle : -rotationStepAngle,
                rotationAxis
            );

            // Temporarily parent the player and ground objects to the mesh
            if (player != null && player != clockArm)
                player.SetParent(transform);
            if (soundManager != null)
        {
            soundManager.PlaySFX("MeshRotate");
        }


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
    public void SetRotationSettings(
        float duration,
        float delay,
        Vector3 axis,
        float stepAngle,
        Transform playerTransform,
        Transform clockArmTransform,
        Transform[] groundTransforms
    )
    {
        rotationDuration = duration;
        rotationDelay = delay;
        rotationAxis = axis;
        rotationStepAngle = stepAngle;
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
