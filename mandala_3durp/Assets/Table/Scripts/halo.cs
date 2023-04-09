using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderRotator : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float rotationThreshold = 2f; // Distance at which the cylinder should rotate
    public float rotationSpeed = 2f; // How fast the cylinder should rotate
    public float returnDelay = 3f; // How long to wait before returning to original position

    private bool hasRotated = false; // Keep track of whether the cylinder has already rotated
    private Vector3 originalPosition; // Store the original position of the cylinder
    private Quaternion originalRotation; // Store the original rotation of the cylinder
    private float returnTimer = 0f; // Keep track of how long to wait before returning to original position

    void Start()
    {
        // Store the original position and rotation of the cylinder
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < rotationThreshold && !hasRotated)
        {
            transform.Rotate(90f, 0f, 0f, Space.World);
            hasRotated = true;
            returnTimer = returnDelay;
        }

        if (hasRotated)
        {
            // Wait for the return delay to expire
            returnTimer -= Time.deltaTime;
            if (returnTimer <= 0f)
            {
                // Move the cylinder back to its original position and rotation
                transform.position = originalPosition;
                transform.rotation = originalRotation;
                hasRotated = false;
            }
        }
    }
}
