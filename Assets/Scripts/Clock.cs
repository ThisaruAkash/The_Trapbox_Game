using UnityEngine;
using System.Collections;

public class RotateWithGear : MonoBehaviour
{
    public float rotationDuration = 2f;    // Duration of each 90-degree rotation
    public float rotationDelay = 0f;       // Delay before starting rotation
    public Vector3 rotationAxis = Vector3.right; // Axis of rotation
    public float totalRotationAngle = 90f; // Total rotation angle per rotation step
    public Transform middleGear;           // The middle gear object
    
    private Quaternion initialRotation;    // Store the initial rotation of the mesh

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
        transform.SetParent(middleGear); // Parent the clock arm to the middle gear
        transform.localPosition = Vector3.zero; // Reset position relative to the middle gear
        StartCoroutine(SmoothRotate()); // Start rotation coroutine
    }

    private IEnumerator SmoothRotate()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.AngleAxis(-totalRotationAngle, rotationAxis);

        // Rotate smoothly over the specified duration
        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Snap to exact final rotation
        ReleaseClockArm(); // Call method to release the arm after rotation
    }


    private void ReleaseClockArm()
{
    // Detach the clock arm from the middle gear
    transform.SetParent(null); // Remove the clock arm from the middle gear
    Rigidbody rb = GetComponent<Rigidbody>(); // Get Rigidbody component attached to the clock arm
    
    if (rb != null)
    {
        rb.isKinematic = false;  // Enable physics (gravity)
    }
}

}
