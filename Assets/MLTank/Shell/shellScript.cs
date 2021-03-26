using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public GameObject tank_gameobject;
    public Vector3 shotDirection;
    public int maxCol;
    public bool learningMode;
    private int col_count=0;
    void Start()
    {
        //Time.timeScale=1.0f;
        rb=GetComponent<Rigidbody>();
        rb.velocity= 10.0f*shotDirection;
    }
    // Update is called once per frame
    void Update()
    {
        rb=GetComponent<Rigidbody>();
        Vector3 v=rb.velocity;
        transform.LookAt(v+transform.position);
    }
    void OnCollisionEnter(Collision collision){
        //Debug.Log(collision.gameObject.tag);
        rb=GetComponent<Rigidbody>();
        Vector3 v=rb.velocity;
        transform.LookAt(v+transform.position);
        col_count+=1;
        if(collision.gameObject.tag=="tank"){
            Debug.Log(collision.gameObject.tag);
            if(learningMode){
                collision.gameObject.GetComponent<MLTank>().gameset(-0.1f);
                if(tank_gameobject!=collision.gameObject)tank_gameobject.GetComponent<MLTank>().gameset(1.0f);
            }
        }
        else if(collision.gameObject.tag=="target"){
            tank_gameobject.GetComponent<MLTank>().gameset(1.0f);
        }
        else if(col_count==maxCol||collision.gameObject.tag=="Shell"){
            tank_gameobject.GetComponent<RootTank>().shellNum-=1;
            Destroy(this.gameObject);
        }
    }
}