using UnityEngine;

public class PuzzleGround : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material transparentMaterial; // Transparent material for inactive state
    [SerializeField] private Material activeMaterial;      // Active material for active state

    private Renderer[] childRenderers; // Renderers for all child objects
    private Collider[] childColliders; // Colliders for all child objects

    void Start()
    {
        // Get all child renderers and colliders
        childRenderers = GetComponentsInChildren<Renderer>();
        childColliders = GetComponentsInChildren<Collider>();

        // Initialize the objects as inactive (transparent)
        SetInactiveForAllChildren();
    }

    private void SetInactiveForAllChildren()
    {
        // Apply transparent material to all child renderers
        foreach (var renderer in childRenderers)
        {
            if (renderer != null)
            {
                renderer.material = transparentMaterial;
            }
        }

        // Set all child colliders to trigger mode (non-collidable but detectable by raycast)
        foreach (var collider in childColliders)
        {
            if (collider != null)
            {
                collider.isTrigger = true; // Make sure the colliders are triggers initially
            }
        }
    }
}
