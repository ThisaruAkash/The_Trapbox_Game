using UnityEngine;

public class SingleSkyboxRotator : MonoBehaviour
{
    [Header("Skybox Rotation Settings")]
    public float rotationSpeed = 1f; // Speed at which the skybox rotates

    void Update()
    {
        RotateSkybox();
    }

    private void RotateSkybox()
    {
        // Gradually rotate the skybox around the Y-axis
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
