using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCombiner : MonoBehaviour
{
    public float distanceThreshold = 0.5f;  // How close the objects need to be in world units
    public float alignmentThreshold = 0.9f;  // Dot product threshold for vertical alignment
    public GameObject playerPlatform;  // The platform to attach to
    public GameObject solidFallingBox;  // The prefab to use for the larger combined object
    public float snapHeight = 0.5f;  // The height to align the combined object to

    // Method to combine the objects into a larger one
    public void CombineObjects(List<GameObject> objectsToCombine, string objectName)
    {
        if (objectsToCombine.Count != 3)  // Only combine exactly 3 objects
        {
            Debug.LogError("Exactly 3 objects are required to combine.");
            return;
        }

        // Calculate the average position for the new combined object
        Vector3 averagePosition = Vector3.zero;
        foreach (GameObject obj in objectsToCombine)
        {
            averagePosition += obj.transform.position;
        }
        averagePosition /= objectsToCombine.Count;

        // Instantiate the new larger box (3x height)
        GameObject combinedBox = Instantiate(solidFallingBox, averagePosition, Quaternion.identity);

        // Set the new box height to 3x the original height
        Vector3 originalScale = combinedBox.transform.localScale;
        combinedBox.transform.localScale = new Vector3(originalScale.x, originalScale.y * 3, originalScale.z);

        // Snap the combined object to a multiple of snapHeight (e.g., 0.5f)
        float snappedY = Mathf.Round(combinedBox.transform.position.y / snapHeight) * snapHeight;
        combinedBox.transform.position = new Vector3(combinedBox.transform.position.x, snappedY, combinedBox.transform.position.z);

        // Attach the new box to the player platform so it moves with the platform
        combinedBox.transform.SetParent(playerPlatform.transform, true);  // true preserves world position

        // Optionally disable physics interactions if needed
        Rigidbody rb = combinedBox.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // Disable physics if desired
        }

        // Destroy the original boxes
        foreach (GameObject obj in objectsToCombine)
        {
            Destroy(obj);
        }

        Debug.Log("Three boxes combined into a single larger rectangular box, snapped to height " + snapHeight);
    }

    // Method to check if two objects can combine based on distance and alignment
    public bool CanCombineObjects(GameObject obj1, GameObject obj2)
    {
        // Calculate the vector from obj1 to obj2
        Vector3 direction = obj2.transform.position - obj1.transform.position;
        float distance = direction.magnitude;

        // Check if they are close enough in distance
        if (distance > distanceThreshold)
        {
            return false;
        }

        // Normalize the direction vector
        Vector3 normalizedDirection = direction.normalized;

        // Compare the normalized direction with the world up vector (assuming Y-axis is up)
        float dotProduct = Vector3.Dot(normalizedDirection, Vector3.up);

        // If the dot product is above the threshold, they are aligned vertically enough to combine
        return dotProduct > alignmentThreshold;
    }

    // Method to check and combine three objects if possible
    public void CheckForCombiningObjects(List<GameObject> collectedObjects, string objectName)
    {
        List<GameObject> objectsToCombine = new List<GameObject>();

        // Check for exactly three objects to combine
        for (int i = 0; i < collectedObjects.Count; i++)
        {
            GameObject obj1 = collectedObjects[i];
            if (obj1.name != objectName) continue;

            for (int j = i + 1; j < collectedObjects.Count; j++)
            {
                GameObject obj2 = collectedObjects[j];
                if (obj2.name != objectName) continue;

                if (CanCombineObjects(obj1, obj2))
                {
                    // Add the first two objects
                    objectsToCombine.Add(obj1);
                    objectsToCombine.Add(obj2);

                    for (int k = j + 1; k < collectedObjects.Count; k++)
                    {
                        GameObject obj3 = collectedObjects[k];
                        if (obj3.name != objectName) continue;

                        if (CanCombineObjects(obj2, obj3))
                        {
                            // Add the third object
                            objectsToCombine.Add(obj3);
                            break;
                        }
                    }

                    // If we have 3 objects, combine them
                    if (objectsToCombine.Count == 3)
                    {
                        CombineObjects(objectsToCombine, objectName);
                        return;  // Stop checking once a combination is made
                    }
                    else
                    {
                        objectsToCombine.Clear();  // Reset if fewer than 3 objects found
                    }
                }
            }
        }
    }
}
