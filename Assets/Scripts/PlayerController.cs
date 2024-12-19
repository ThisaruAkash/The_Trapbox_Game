using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 45f;   // Speed of forward and backward movement
    public float jumpForce = 40f;    // Force of the jump

    private Animator animator;      // Animator reference
    private Rigidbody rb;
    private bool isGrounded;

    private bool firstCollisionOccurred = false; // Tracks if the first collision has happened

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        animator.SetBool("IsGrounded", isGrounded);
    }

    void Move()
    {
        float moveDirection = 0;
        animator.SetBool("IsMoving", false);

        // Check for input and determine movement direction
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("IsMoving", true);
            moveDirection = 1;
            transform.forward = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("IsMoving", true);
            moveDirection = -1;
            transform.forward = Vector3.back;
        }

        // Apply movement only along the Z-axis (forward or backward)
        Vector3 movement = new Vector3(0, rb.velocity.y, moveDirection * moveSpeed);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, movement.z);
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);

            // Set the first collision flag
            if (!firstCollisionOccurred)
                firstCollisionOccurred = true;
        }
    }

    public void AlignToGround(Vector3 newUp)
    {
        // Align the character's up direction to match the new ground
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;
        transform.rotation = targetRotation;

        // Ensure the Rigidbody respects the new orientation
        rb.velocity = Quaternion.FromToRotation(transform.up, newUp) * rb.velocity;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private bool IsGrounded()
{
    return Physics.Raycast(transform.position, Vector3.down, 0.1f);
}

}
