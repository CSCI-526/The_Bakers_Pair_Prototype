using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject towerObj;

    public float moveSpeed = 5f;
    public float minX = -10f;      // Minimum X value (left boundary)
    public float maxX = 10f;       // Maximum X value (right boundary)
    public float thrust = 20.0f;

    private bool rightOnceCheck = true;
    private bool leftOnceCheck = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create a movement vector only along the X-axis
        Vector3 movement = new Vector3(1.0f, 0f, 0f);

        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            transform.Translate(movement * moveSpeed * Time.deltaTime);
            Vector3 force = new Vector3(thrust, 0.0f, 0.0f);
            //if (rightOnceCheck)
            //{
                towerObj.GetComponent<Rigidbody>().velocity = force;
                //towerObj.GetComponent<Rigidbody>().AddForce(force);
           // }
            rightOnceCheck = false;
        }
        if(Input.GetKeyUp(KeyCode.D)) 
        {
            rightOnceCheck = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            transform.Translate(-1.0f * movement * moveSpeed * Time.deltaTime);
            Vector3 force = new Vector3(-thrust, 0.0f, 0.0f);
            //if (leftOnceCheck)
            //{
                towerObj.GetComponent<Rigidbody>().velocity = force;
                //towerObj.GetComponent<Rigidbody>().AddForce(force);
            //}
            leftOnceCheck = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            leftOnceCheck = true;
        }

        if(transform.position.x < -6.0f)
        {
            transform.position = new Vector3(-6.0f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 6.0f)
        {
            transform.position = new Vector3(6.0f, transform.position.y, transform.position.z);
        }

        this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);

    }
}
