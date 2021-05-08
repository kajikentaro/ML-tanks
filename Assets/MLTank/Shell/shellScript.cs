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
    private Vector3 pre_collision_point = Vector3.up;
    private Vector3 pre_collision_velocity = Vector3.up;
    private Vector3 pre_collision_normal = Vector3.up;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        shotDirection *= 10f;
        rb.velocity= shotDirection;
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = shotDirection;
        transform.LookAt(rb.velocity+transform.position);
        pre_collision_velocity = rb.velocity;
    }
    void OnCollisionEnter(Collision collision){
        if (pre_collision_point == collision.contacts[0].point && pre_collision_normal == collision.contacts[0].normal) return;
        else
        {
            pre_collision_point = collision.contacts[0].point;
            pre_collision_velocity = rb.velocity;
        }
        //transform.LookAt(rb.velocity+transform.position);
        if (collision.gameObject.tag == "block")
        {
            // 当たった物体の法線ベクトルを取得
            Vector3 reflectVec = Vector3.Reflect(shotDirection, collision.contacts[0].normal);
            pre_collision_normal = collision.contacts[0].normal;
            shotDirection = reflectVec;
            // 計算した反射ベクトルを保存

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