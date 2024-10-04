using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class combineBlocks : MonoBehaviour
{
    public GameObject stacked;
    public GameObject onTop;
    public GameObject box;
    public float leeway = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stacked != null && stacked.GetComponent<combineBlocks>().stacked != null)
        {
            Vector3 avgPos = Vector3.zero;
            avgPos += this.transform.position;
            avgPos += stacked.transform.position;
            avgPos += stacked.GetComponent<combineBlocks>().stacked.transform.position;
            avgPos /= 3.0f;
            GameObject combined = GameObject.Instantiate(box,avgPos, Quaternion.identity);

            combined.transform.localScale = new Vector3(combined.transform.localScale.x,combined.transform.localScale.y + (stacked.transform.localScale.y * 2.0f), combined.transform.localScale.z);
            combined.transform.SetParent(this.transform.parent);
            Destroy(stacked.GetComponent<combineBlocks>().stacked);
            Destroy(stacked);
            Destroy(this.gameObject);
        }
        else if(onTop != null)
        {
            Vector3 direction = (onTop.transform.position - this.transform.position).normalized;
            if (Vector3.Dot(Vector3.up, direction) > leeway)
            {
                stacked = onTop.gameObject;
                onTop = null;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = (collision.gameObject.transform.position - this.transform.position).normalized;
        if(collision.gameObject.transform.position.y >= this.transform.position.y + ((this.transform.localScale.y / 2.0f) * 0.8f)) 
        {
            if(Vector3.Dot(Vector3.up, direction) > leeway)
            {
                stacked = collision.gameObject;
            }
            else
            {
                onTop = collision.gameObject;
            }
        }
    }
}
