using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float moveSpeed = 5f;
    public float minX = -10f;      // Minimum X value (left boundary)
    public float maxX = 10f;       // Maximum X value (right boundary)
    // Update is called once per frame
    void Update()
    {
         // Get horizontal input from the user (A/D keys or Left/Right arrow keys)
        float inputX = Input.GetAxis("Horizontal");

        // Create a movement vector only along the X-axis
        Vector3 movement = new Vector3(inputX, 0f, 0f);

        // Move the platform along the X-axis based on input and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Clamp the platform's position between minX and maxX
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);

        // Apply the clamped X position back to the platform's transform
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
