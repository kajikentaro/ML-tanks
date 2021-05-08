using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class shellScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public GameObject tank_gameobject;
    public Vector3 shotDirection;
    public int maxCol;
    public bool learningMode;
    public bool aliving=true;
    private int col_count=0;
    private Vector3 pre_collision_point;
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
        rb=GetComponent<Rigidbody>();
        if (pre_collision_point == collision.contacts[0].point) return;
        else pre_collision_point = collision.contacts[0].point;
        Vector3 v=rb.velocity;
        transform.LookAt(v+transform.position);
        if (collision.gameObject.tag == "block")
        {
            // 当たった物体の法線ベクトルを取得
            Vector3 reflectVec = Vector3.Reflect(shotDirection, collision.contacts[0].normal);
            rb.velocity = 10.0f * reflectVec;
            // 計算した反射ベクトルを保存
            shotDirection = 10.0f * rb.velocity;

            col_count += 1;
        }
        if(collision.gameObject.tag=="tank"){
            try{
                tank_gameobject.GetComponent<RootTank>().shellNum-=1;
            }catch(Exception ignored){}
            if(learningMode){
                collision.gameObject.GetComponent<RootTank>().received_attack=true;
                if(tank_gameobject!=collision.gameObject)tank_gameobject.GetComponent<MLTank>().hitTank=true;
            }
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag=="target"){
            try{
                tank_gameobject.GetComponent<RootTank>().hitTarget=true;
                tank_gameobject.GetComponent<RootTank>().shellNum-=1;
            }catch(Exception ignored){}
            Destroy(this.gameObject);
        }
        else if(col_count==maxCol||collision.gameObject.tag=="Shell"){
            try{
                if(collision.gameObject.tag=="Shell")tank_gameobject.GetComponent<RootTank>().hitShell=true;
                else tank_gameobject.GetComponent<RootTank>().notHit=true;
                tank_gameobject.GetComponent<RootTank>().shellNum-=1;
            }catch(Exception ignored){}
            Destroy(this.gameObject);
        }
    }
}