using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public int maxColCount=2;
    private int col_count=0;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 v=rb.velocity;
        transform.LookAt(v + transform.localPosition);
    }
    void OnCollisionEnter(Collision collision){
        col_count+=1;
        if(col_count==maxColCount){
            Destroy(this.gameObject);
        }
    }
}
