﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class TankMe : RootTank
{
    //public float rotate_speed = 3.0f;
    //Update is called once per frame
    public GameObject tankTop;
    rotate rotate_script;
    public GameObject shotShell;
    ShotShell shotShell_script;
    void Start(){
        shotShell_script = shotShell.GetComponent<ShotShell>();
        rotate_script = tankTop.GetComponent<rotate>();
    }
    void Update()
    {
        if (aliving)//&& StageMaker.canMove)
        {
            //平行移動
            if (Input.GetKey("w"))
            {
                forwardTank(Time.deltaTime);
            }
            if (Input.GetKey("s"))
            {
                backwardTank(Time.deltaTime);
            }
            if (Input.GetKey("d"))
            {
                rightTank(Time.deltaTime);
            }
            if (Input.GetKey("a"))
            {
                leftTank(Time.deltaTime);
            }
            if (Input.GetMouseButtonDown(0)){
                shotShell_script.shotShell();
            }
        }
        rotate_script.rotateByMouse();

    }
    private void OnCollisionEnter(Collision other)
    {
        //public GameObject top = transform.Find("top").gameObject;
        //public GameObject bottom = transform.Find("bottom").gameObject;
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば（条件）
        if (other.gameObject.tag == "Shell")
        {
            Destroy(other.gameObject);
            DestroyTank();
            aliving = false;
        }
    }
}