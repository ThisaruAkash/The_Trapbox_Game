using UnityEngine;
using System.Collections;

public class RotateWithGear02 : MonoBehaviour
{
    public float rotationDuration = 2f;        // Duration of each 90-degree rotation
    public float rotationDelay = 0f;           // Delay before starting rotation
    public Vector3 rotationAxis = Vector3.right; // Axis of rotation
    public float totalRotationAngle = 90f;     // Total rotation angle per rotation step
    public Transform middleGear;               // The middle gear object
    private bool isStopped = false;            // Tracks if the clock arm has stopped due to collision

    void Start()
    {
        if (rotationDelay > 0)
        {
            Invoke(nameof(StartRotation), rotationDelay);
        }
        else
        {
            StartRotation();
        }
    }

    private void StartRotation()
    {
        StartCoroutine(SmoothRotate());
    }

    private IEnumerator SmoothRotate()
    {
        while (true) // Infinite loop for continuous rotation
        {
            if (isStopped) yield break; // Exit if clock arm is stopped

            float elapsedTime = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.AngleAxis(-totalRotationAngle, rotationAxis);

            while (elapsedTime < rotationDuration)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = endRotation; // Snap to final rotation
            yield return new WaitForSeconds(rotationDelay); // Wait before the next rotation
        }
    }
}
