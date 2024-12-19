using UnityEngine;

public class BuildingGrounds : MonoBehaviour
{
    public static BuildingGrounds current;

    public GridLayout gridLayout;   // Reference to the GridLayout component
    private Grid grid;             // Reference to the Grid component

    void Awake()
    {
        current = this;
        grid = gridLayout.GetComponent<Grid>(); // Get the Grid component
    }

    // Get the mouse position in world space dynamically
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Raycast to determine mouse position in world space
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point; // Return the point where the ray hits
        }

        return Vector3.zero;
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        // Snap the position to the nearest grid cell using the Grid component
        Vector3Int cellPos = gridLayout.WorldToCell(position); // Get the cell position from world coordinates
        return grid.GetCellCenterWorld(cellPos);              // Convert cell position back to world space
    }
}
