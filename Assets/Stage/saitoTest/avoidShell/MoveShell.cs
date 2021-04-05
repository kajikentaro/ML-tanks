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
        //float x=transform.localPosition.x;
        //float z=transform.localPosition.z;
        //if(x>startX||z>startZ){
            //this.transform.localPosition=new Vector3(startX,1.0f,startZ);
            //speed*=-1;
        //}
        //if(x<endX||z<endZ){
            //this.transform.localPosition=new Vector3(endX,1.0f,endZ);
            //speed*=-1;
        //}
        //this.transform.localPosition+=new Vector3(speed*(startX-endX),0,speed*(startZ-endZ));
    }

    private int count;
    void OnCollisionEnter(Collision collision){
        //count++;
        //if(count==50){
            //int w=30;
            //int h=30;
            //transform.localPosition = new Vector3(w,0.6f,h*(Random.value-0.5f));
            //rb.velocity=new Vector3(speed,0,speed);
            //count=0;
        //}
    }
}
