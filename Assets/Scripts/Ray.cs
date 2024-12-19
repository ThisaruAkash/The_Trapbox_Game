using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;  // Layer mask to filter raycast

    void Start()
    {
        Debug.Log("Raycasting script initialized!");
    }

    void Update()
    {
        // Perform the raycast from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Log the ray's origin and direction
        Debug.Log($"Ray origin: {ray.origin}, Ray direction: {ray.direction}");

        // Cast the ray with the specified layer mask
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            // Log the object hit
            Debug.Log($"Raycast hit: {hit.collider.gameObject.name} on layer {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
        }
        else
        {
            Debug.Log("Raycast didn't hit anything or was blocked.");
        }
    }
}
