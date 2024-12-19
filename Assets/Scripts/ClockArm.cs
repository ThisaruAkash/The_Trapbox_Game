using UnityEngine;

public class ClockArm : MonoBehaviour
{
    private Rigidbody clockArmRigidbody; // Rigidbody for the clock arm

    void Start()
    {
        clockArmRigidbody = GetComponent<Rigidbody>();

        if (clockArmRigidbody != null)
        {
            clockArmRigidbody.isKinematic = false; // Ensure clock arm is free-moving
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PuzzleGroundChild puzzleGroundChild = collision.gameObject.GetComponent<PuzzleGroundChild>();

        if (puzzleGroundChild != null)
        {
            if (puzzleGroundChild.IsActive())
            {
                Debug.Log($"Clock arm collided with active puzzle: {collision.gameObject.name}");
                StopClockArm(); // Stop the clock arm when colliding with an active puzzle
            }
            else
            {
                Debug.Log($"Clock arm ignored collision with inactive puzzle: {collision.gameObject.name}");
                // Ignore collision with inactive ground puzzle
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
            }
        }
    }

    private void StopClockArm()
    {
        if (clockArmRigidbody != null)
        {
            clockArmRigidbody.velocity = Vector3.zero;        // Stop all movement
            clockArmRigidbody.angularVelocity = Vector3.zero; // Stop rotation
        }

        Debug.Log("Clock arm stopped by collision with an active puzzle ground.");
    }

    private void OnCollisionExit(Collision collision)
    {
        PuzzleGroundChild puzzleGroundChild = collision.gameObject.GetComponent<PuzzleGroundChild>();

        if (puzzleGroundChild != null && !puzzleGroundChild.IsActive())
        {
            // Re-enable collision after the clock arm leaves an inactive puzzle
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), false);
            Debug.Log($"Re-enabled collision with inactive puzzle: {collision.gameObject.name}");
        }
    }
}
