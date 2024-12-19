using UnityEngine;

public class GearRotator : MonoBehaviour
{
    [Header("Gear Rotation Settings")]
    [Tooltip("Speed of rotation (positive values for clockwise, negative for counter-clockwise).")]
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        // Rotate the gear around its Z-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
