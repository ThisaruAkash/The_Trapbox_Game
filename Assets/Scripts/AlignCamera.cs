using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform groundMesh;   // Reference to the ground mesh
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10); // Offset for the camera relative to the mesh

    [Header("Transition Settings")]
    [SerializeField] private bool smoothTransition = true; // Enable smooth transition
    [SerializeField] private float transitionSpeed = 2f;   // Speed of the camera transition

    private Camera mainCamera;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        if (groundMesh != null)
        {
            // Align the camera at the start
            AlignCameraToGroundMesh();
        }
        else
        {
            Debug.LogError("Ground Mesh is not assigned. Please assign it in the Inspector.");
        }
    }

    public void AlignCameraToGroundMesh()
    {
        if (mainCamera == null || groundMesh == null) return;

        // Calculate the target position
        Vector3 targetPosition = groundMesh.position + offset;

        if (smoothTransition)
        {
            // Start a coroutine for a smooth transition
            StartCoroutine(SmoothAlign(targetPosition));
        }
        else
        {
            // Instantly move the camera to the target position
            mainCamera.transform.position = targetPosition;
            mainCamera.transform.LookAt(groundMesh);
        }
    }

    private System.Collections.IEnumerator SmoothAlign(Vector3 targetPosition)
    {
        // Smoothly move the camera to the target position
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * transitionSpeed);
            mainCamera.transform.LookAt(groundMesh); // Ensure the camera always looks at the ground mesh
            yield return null;
        }

        // Snap to the final position after smoothing
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.LookAt(groundMesh);
    }
}
