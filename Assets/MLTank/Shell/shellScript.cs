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
        print(Time.timeScale);
        Time.timeScale=1.0f;
        rb=GetComponent<Rigidbody>();
        rb.velocity= 50.0f*transform.forward * shotSpeed;
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
        if(collision.gameObject.tag=="tank"){
            shotshell_gameobject.GetComponent<ShotShell>().shellNum-=1;
            tank_gameobject.GetComponent<MLTank>().gameset(1.0f);
            collision.gameObject.GetComponent<MLTank>().gameset(-1.0f);
        }
        col_count+=1;
        if(col_count==maxCol||collision.gameObject.tag!="block"){
            shotshell_gameobject.GetComponent<ShotShell>().shellNum-=1;
            Destroy(this.gameObject);
        }
    }
}