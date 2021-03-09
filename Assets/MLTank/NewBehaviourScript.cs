using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        Vector3 v=new Vector3(-2.0f,0.0f,0.5f);
        rb.velocity=1.0f*v;
        //rb.AddForce(10*v);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v=new Vector3(-2.0f,0.0f,0.0f);
        //rb.AddForce(v);
        //rb.velocity=v;
        
    }
}
