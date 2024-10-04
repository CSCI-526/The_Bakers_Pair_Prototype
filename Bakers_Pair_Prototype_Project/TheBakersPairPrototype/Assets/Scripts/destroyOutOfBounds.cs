using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float bottomY = -8f;  // The Y position below which objects will be destroyed

    void Update()
    {
        // Check if the object has fallen below the bottomY position
        if (transform.position.y < bottomY) // Only destroy if not part of the tower
        {
            Destroy(gameObject); // Destroy the object
        }
    }
}
