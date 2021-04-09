using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShell : MonoBehaviour
{
    // Start is called before the first frame update
    public float startX;
    public float startZ;
    public float endX;
    public float endZ;
    public float speed=5.0f;
    Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        rb.velocity=new Vector3(speed,0,speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Mathf.Abs(rb.velocity.x)<0.001f||Mathf.Abs(rb.velocity.z)<0.001f)rb.velocity=new Vector3(speed,0,speed);
    }

}
