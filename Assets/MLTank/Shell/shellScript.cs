using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public GameObject shotShell;
    public GameObject shotshell_gameobject;
    public float shotSpeed;
    private int col_count=0;
    public int maxCol;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        rb.velocity= transform.forward * shotSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 v=rb.velocity;
        transform.LookAt(v + transform.position);
    }
    void OnCollisionEnter(Collision collision){
        col_count+=1;
        if(col_count==maxCol||collision.gameObject.tag!="block"){
            shotshell_gameobject.GetComponent<ShotShell>().shellNum-=1;
            Destroy(this.gameObject);
        }
    }
}