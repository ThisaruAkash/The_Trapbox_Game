using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    private bool isDragging = false;        // Tracks whether the object is being dragged
    private Vector3 offset;                // Offset to maintain position relative to the cursor

    void Update()
    {
        if (isDragging)
        {
            DragObject(); // Update object position while dragging
        }
    }

    private void OnMouseDown()
    {
        // Start dragging and calculate the initial offset
        isDragging = true;
        offset = transform.position - BuildingGrounds.GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        // Stop dragging
        isDragging = false;
    }

    private void DragObject()
    {
        // Get the mouse position in world space
        Vector3 mouseWorldPos = BuildingGrounds.GetMouseWorldPosition();

        // Snap the position to the grid
        Vector3 snappedPosition = BuildingGrounds.current.SnapCoordinateToGrid(mouseWorldPos + offset);

        // Update the object's position directly to the snapped position
        transform.position = snappedPosition;
    }
}

