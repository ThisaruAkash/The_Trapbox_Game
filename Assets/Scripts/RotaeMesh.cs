using UnityEngine;
using System.Collections;

public class MeshRotator: MonoBehaviour
{
    public float rotationDelay = 3f;
    public float rotationDuration = 2f;
    public Vector3 rotationAxis = Vector3.right;

    public float totalRotationAngle = 90f;
    public bool hasRotated = false;
    public Transform player;
    private bool rotatePositive = true;
    private Quaternion initialRotation;
    


    
}