using UnityEngine;

public class PuzzleGroundChild : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material transparentMaterial; // Material for inactive state
    [SerializeField] private Material activeMaterial;      // Material for active state

    private Renderer objectRenderer;  // Renderer of the current ground object
    private Collider objectCollider;  // Collider of the current ground object
    private bool isActive = false;    // Tracks the current activation state

    void Start()
    {
        // Get the renderer and collider components
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        // Ensure collider is enabled initially for interactions like clicks
        if (objectCollider != null)
        {
            objectCollider.enabled = true; // Ensure collider is enabled
        }

        // Start in the inactive state (transparent and non-collidable for player and clock arm)
        SetInactive();
    }

    void OnMouseDown() // Detect when the player clicks this object
    {
        if (isActive)
        {
            // If the object is already active, deactivate it
            SetInactive();
        }
        else
        {
            // If the object is inactive, activate it
            SetActive();
        }
    }

    private void SetInactive()
    {
        isActive = false;

        // Apply transparent material to this object
        objectRenderer.material = transparentMaterial;

        // Disable the collider to prevent interaction with clock arm and player
        if (objectCollider != null)
        {
            objectCollider.isTrigger = true; // Fully disable collision but allow detection by raycast
        }

        Debug.Log($"{gameObject.name} set to inactive (transparent and non-collidable).");
    }

    private void SetActive()
    {
        isActive = true;

        // Apply active material to this object
        objectRenderer.material = activeMaterial;

        // Enable the collider to allow interaction with clock arm and player
        if (objectCollider != null)
        {
            objectCollider.isTrigger = false; // Enable full collision
        }

        Debug.Log($"{gameObject.name} set to active (collidable and original material).");
    }

    // Method to check if the puzzle ground is active
    public bool IsActive()
    {
        return isActive;
    }
}
