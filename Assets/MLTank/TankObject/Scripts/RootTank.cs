﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class RootTank : Agent
{
    public GameObject shellPrefab;
    public GameObject Shells;
    public GameObject tankTop;
    public bool EnableMove=false;
    public bool learningMode;
    public float speedTank = 1.0f;
    public int maxColCount;
    public int maxShellNum;
    public int shellNum;
    public float shot_speed;
    public float shotInterval;
    public int launch_cnt=0;
    public float last_launch_time=-100;
    private float a=1.0f;

    //public AudioClip shotSound;
    // Update is called once per frame
    public void shotShell()
    {
        if((Time.time-last_launch_time)>shotInterval){
            if(shellNum<maxShellNum){
                shellNum+=1;
                Vector3 shellPos=transform.position;
                shellPos+=1.5f*tankTop.transform.forward;
                GameObject shell = Instantiate(shellPrefab, shellPos,transform.rotation ,Shells.transform);
                shell.GetComponent<shellScript>().maxCol=maxColCount;
                shell.GetComponent<shellScript>().shotDirection=shot_speed*tankTop.transform.forward;
                shell.GetComponent<shellScript>().tank_gameobject=this.gameObject;
                shell.GetComponent<shellScript>().learningMode=learningMode;
                last_launch_time=Time.time;
                launch_cnt+=1;
            }
        }
    }
    public void forwardTank(float delta){
        if(EnableMove){
            transform.position +=a*transform.forward * speedTank * delta;
        }
    }
    public void backwardTank(float delta){
        if(EnableMove){
            transform.position -= a*transform.forward * speedTank * delta;
        }
    }
    public void rightTank(float delta){
        if(EnableMove){
            transform.position += a*transform.right * speedTank * delta;
        }
    }
    public void leftTank(float delta){
        if(EnableMove){
            transform.position -= a*transform.right * speedTank * delta;
        }
    }
    public void DestroyTank(){
        //GameObject refObj;
        //refObj = GameObject.Find("Exposion");
        //effectStart es = refObj.GetComponent<effectStart>();
        //es.startEffect();
        //// このスクリプトがついているオブジェクトを破壊する（thisは省略が可能）
        //Destroy(gameObject.transform.Find("tank").gameObject);
        ////Destroy(gameObject.transform.Find("bottom").gameObject);
        //GameObject scriptholder = GameObject.Find("ScriptHolder");
        //if (scriptholder != null)
        //{
            //StageMaker stagemaker = scriptholder.GetComponent<StageMaker>();
            //stagemaker.dropout_tank();
            //aliving = false;
        //}
    }
}
