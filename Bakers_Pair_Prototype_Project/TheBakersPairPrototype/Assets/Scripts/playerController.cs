// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;

// public class playerController : MonoBehaviour
// {
//     public GameObject towerObj;

//     public float moveSpeed = 5f;
//     public float minX = -10f;      // Minimum X value (left boundary)
//     public float maxX = 10f;       // Maximum X value (right boundary)
//     public float thrust = 20.0f;

//     private bool rightOnceCheck = true;
//     private bool leftOnceCheck = true;

//     public List<GameObject> collectedObjects = new List<GameObject>();
//     public objectCombiner objectCombiner;


//     // Start is called before the first frame update
//     void Start()
//     {
//        GameObject combinerObject = GameObject.Find("Tower"); // Replace "Tower" with the actual name
//         if (combinerObject != null) {
//             objectCombiner = combinerObject.GetComponent<objectCombiner>();
//         } else {
//             Debug.LogError("ObjectCombiner not found!");
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Create a movement vector only along the X-axis
//         Vector3 movement = new Vector3(1.0f, 0f, 0f);

//         if (Input.GetKey(KeyCode.D))
//         {
//             this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
//             transform.Translate(movement * moveSpeed * Time.deltaTime);
//             Vector3 force = new Vector3(thrust, 0.0f, 0.0f);
//             //if (rightOnceCheck)
//             //{
//             foreach (Transform child in towerObj.transform)
//             {
//                 child.gameObject.GetComponent<Rigidbody>().velocity = force;
//             }
//                 //towerObj.GetComponent<Rigidbody>().AddForce(force);
//            // }
//             rightOnceCheck = false;
//         }
//         if(Input.GetKeyUp(KeyCode.D)) 
//         {
//             rightOnceCheck = true;
//         }

//         if (Input.GetKey(KeyCode.A))
//         {
//             this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
//             transform.Translate(-1.0f * movement * moveSpeed * Time.deltaTime);
//             Vector3 force = new Vector3(-thrust, 0.0f, 0.0f);
//             //if (leftOnceCheck)
//             //{
//             foreach (Transform child in towerObj.transform)
//             {
//                 child.gameObject.GetComponent<Rigidbody>().velocity = force;
//             }                //towerObj.GetComponent<Rigidbody>().AddForce(force);
//             //}
//             leftOnceCheck = false;
//         }
//         if (Input.GetKeyUp(KeyCode.A))
//         {
//             leftOnceCheck = true;
//         }

//         if(transform.position.x < -6.0f)
//         {
//             transform.position = new Vector3(-6.0f, transform.position.y, transform.position.z);
//         }

//         if (transform.position.x > 6.0f)
//         {
//             transform.position = new Vector3(6.0f, transform.position.y, transform.position.z);
//         }

//         this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);


//         CheckForCombiningObjects();

//     }

//     // Check for object combining
//     void CheckForCombiningObjects() {
//         Dictionary<string, int> objectCount = new Dictionary<string, int>();

//         foreach (GameObject obj in collectedObjects) {
//             string objName = obj.name;
//             if (objectCount.ContainsKey(objName)) {
//                 objectCount[objName]++;
//             } else {
//                 objectCount[objName] = 1;
//             }

//             if (objectCount[objName] >= 3) {
//                 // Find the first three objects with the same name
//                 List<GameObject> objectsToCombine = collectedObjects
//                                                     .FindAll(o => o.name == objName)
//                                                     .Take(3)  // LINQ method Take()
//                                                     .ToList();
                
//                 // Check if they are aligned within a tolerance 
//                 if (AreObjectsAligned(objectsToCombine, 1.0f)) {
//                     objectCombiner.CombineObjects(objectsToCombine, objName);
//                     break;
//                 }
//             }
//         }
//     }

//     // Method to check if three objects are aligned
//     bool AreObjectsAligned(List<GameObject> objects, float tolerance) {
//         if (objects.Count != 3) return false;

//         // Check if positions are aligned within a tolerance (only on X and Z axis, for example)
//         Vector3 pos1 = objects[0].transform.position;
//         Vector3 pos2 = objects[1].transform.position;
//         Vector3 pos3 = objects[2].transform.position;

//         return Mathf.Abs(pos1.x - pos2.x) < tolerance &&
//                Mathf.Abs(pos2.x - pos3.x) < tolerance &&
//                Mathf.Abs(pos1.z - pos2.z) < tolerance &&
//                Mathf.Abs(pos2.z - pos3.z) < tolerance;
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject towerObj;  // The parent object holding the stacked objects
    public float moveSpeed = 5f;
    public float minX = -10f;      // Minimum X value (left boundary)
    public float maxX = 10f;       // Maximum X value (right boundary)
    public float thrust = 20.0f;

    private bool rightOnceCheck = true;
    private bool leftOnceCheck = true;

    private List<GameObject> stackedBoxes = new List<GameObject>();

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
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.Translate(movement * moveSpeed * Time.deltaTime);
            ApplyForceToStackedObjects(Vector3.right * thrust);
            rightOnceCheck = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightOnceCheck = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.Translate(-movement * moveSpeed * Time.deltaTime);
            ApplyForceToStackedObjects(Vector3.left * thrust);
            leftOnceCheck = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            leftOnceCheck = true;
        }

        // Clamp movement within boundaries
        ClampPosition();

        // Check if 3 stacked boxes are aligned perfectly
        CheckAndCombineStackedBoxes();
    }

    // Apply force to all objects stacked on the tower
    void ApplyForceToStackedObjects(Vector3 force)
    {
        foreach (Transform child in towerObj.transform)
        {
            if (child.gameObject.CompareTag("Box"))
            {
                child.gameObject.GetComponent<Rigidbody>().velocity = force;
            }
        }
    }

    // Clamp the player within the given X boundaries
    void ClampPosition()
    {
        if (transform.position.x < -6.0f)
        {
            transform.position = new Vector3(-6.0f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 6.0f)
        {
            transform.position = new Vector3(6.0f, transform.position.y, transform.position.z);
        }

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Check if three consecutive boxes are perfectly aligned
    void CheckAndCombineStackedBoxes()
    {
        stackedBoxes.Clear();

        // Find all the stacked boxes in the tower object
        foreach (Transform child in towerObj.transform)
        {
            if (child.gameObject.CompareTag("Box"))
            {
                stackedBoxes.Add(child.gameObject);
            }
        }

        // Check if we have 3 boxes perfectly aligned
        for (int i = 0; i <= stackedBoxes.Count - 3; i++)
        {
            if (AreBoxesAligned(stackedBoxes[i], stackedBoxes[i + 1], stackedBoxes[i + 2]))
            {
                CombineBoxes(stackedBoxes[i], stackedBoxes[i + 1], stackedBoxes[i + 2]);
                break;  // Only combine once per frame to avoid combining in bulk
            }
        }
    }

    // Check if three boxes are aligned (position and rotation)
    bool AreBoxesAligned(GameObject box1, GameObject box2, GameObject box3)
    {
        float positionThreshold = 0.1f;  // Adjust for how strict the alignment should be
        float rotationThreshold = 5.0f;  // Degrees tolerance

        bool positionAligned = Mathf.Abs(box1.transform.position.x - box2.transform.position.x) < positionThreshold &&
                               Mathf.Abs(box2.transform.position.x - box3.transform.position.x) < positionThreshold;

        bool rotationAligned = Quaternion.Angle(box1.transform.rotation, box2.transform.rotation) < rotationThreshold &&
                               Quaternion.Angle(box2.transform.rotation, box3.transform.rotation) < rotationThreshold;

        return positionAligned && rotationAligned;
    }

    // Combine three aligned boxes into one
    void CombineBoxes(GameObject box1, GameObject box2, GameObject box3)
    {
        // Create a new combined object
        GameObject combinedBox = new GameObject("CombinedBox");
        combinedBox.transform.position = (box1.transform.position + box2.transform.position + box3.transform.position) / 3.0f;
        combinedBox.transform.rotation = box1.transform.rotation;
        combinedBox.AddComponent<BoxCollider>();  // Assuming boxes are cubes
        combinedBox.AddComponent<Rigidbody>().isKinematic = true;  // Prevent physics interactions

        // Add the combined box to the tower
        combinedBox.transform.parent = towerObj.transform;

        // Destroy the original three boxes
        Destroy(box1);
        Destroy(box2);
        Destroy(box3);
    }
}
