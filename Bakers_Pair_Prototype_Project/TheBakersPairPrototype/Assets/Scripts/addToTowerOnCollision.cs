using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addToTowerOnCollision : MonoBehaviour
{
    public GameObject towerObj;  // Reference to the Tower object

    void Start()
    {
        // Find the Tower object by its tag or assign it manually if it's not set
        if (towerObj == null)
        {
            towerObj = GameObject.FindWithTag("Tower"); // Ensure your Tower has the tag 'Tower'
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object collides with PlayerPlatform or any child of the Tower
        if (collision.gameObject.name == "PlayerPlatform" || 
            IsChildOfTower(collision.transform))
        {
            // Make the falling object a child of the Tower
            SetParentToTower();

        }
    }

    // Method to check if the collision object is a child of the Tower
    private bool IsChildOfTower(Transform collidingTransform)
    {
        // Check if the colliding object is a child of the Tower object
        return collidingTransform.IsChildOf(towerObj.transform);
    }

    // Method to set the tower as the parent of this object
    private void SetParentToTower()
    {
        transform.SetParent(towerObj.transform);
    }

}

