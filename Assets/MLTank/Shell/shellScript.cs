using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public GameObject shotShell;
    public GameObject shotshell_gameobject;
    public GameObject tank_gameobject;
    public float shotSpeed;
    private int col_count=0;
    public int maxCol;
    void Start()
    {
        //Time.timeScale=1.0f;
        rb=GetComponent<Rigidbody>();
        rb.velocity= 10.0f*transform.forward * shotSpeed;
    }
    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter(Collision collision){
        //Debug.Log(collision.gameObject.tag);
        rb=GetComponent<Rigidbody>();
        Vector3 v=rb.velocity;
        transform.LookAt(v+transform.position);
        col_count+=1;
        if(collision.gameObject.tag=="tank"){
        Debug.Log(collision.gameObject.tag);
            collision.gameObject.GetComponent<MLTank>().gameset(-0.1f);
            if(tank_gameobject!=collision.gameObject)tank_gameobject.GetComponent<MLTank>().gameset(1.0f);
        }
        else if(collision.gameObject.tag=="target"){
            tank_gameobject.GetComponent<MLTank>().gameset(1.0f);
        }
        else if(col_count==maxCol||collision.gameObject.tag=="Shell"){
            shotshell_gameobject.GetComponent<ShotShell>().shellNum-=1;
            Destroy(this.gameObject);
        }
    }
}