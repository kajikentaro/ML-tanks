using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    private int col_count=0;
    public int maxColCount;
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
        if(col_count==maxColCount||collision.gameObject.tag!="block"){
            Destroy(this.gameObject);
        }
    }
}