using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceWithEarthCube : MonoBehaviour
{
    public GameObject earthCubePrefab; // Assign your Earth Cube prefab here

    void Start()
    {
        // Find all ProBuilder faces or tiles with a specific tag
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            // Replace the tile with an Earth Cube instance
            Vector3 position = tile.transform.position;
            Quaternion rotation = tile.transform.rotation;
            Instantiate(earthCubePrefab, position, rotation);

            // Optional: Disable or destroy the original tile
            Destroy(tile);
        }
    }
}
