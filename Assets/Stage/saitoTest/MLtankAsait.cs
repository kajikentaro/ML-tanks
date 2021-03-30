﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankAsait : MLTank
{
    float start_time;
    int NotHitCount;
    RayPerceptionSensorComponent3D rayPer;
    public RayPerceptionInput rayInput;
    public override void Initialize()
    {
        base.Initialize();
        rayPer=tankTop.GetComponent<RayPerceptionSensorComponent3D>();
        rayInput=rayPer.GetRayPerceptionInput();
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        int w=30;
        int h=20;
        //Vector3 newPosition2 = new Vector3(w*(Random.value-0.5f),0.3f,h*(Random.value-0.5f));
        //target.transform.localPosition = newPosition2;
        start_time=Time.time;
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
        var rayOutputs=RayPerceptionSensor.Perceive(rayInput).RayOutputs;
        foreach(var element in rayOutputs){
            if(element.HitTagIndex==0){
                AddReward(0.0001f);
                target.GetComponent<target>().hitRay=true;
                //Debug.Log("addreward");
            }
        }
        if(received_attack){
            SetReward(-1.0f);
            received_attack=false;
            //Debug.Log("received_attack");
            EndEpisode();
        }
        if(hitTank){
            SetReward(-0.5f);
            hitTank=false;
            //Debug.Log("hit tank");
            EndEpisode();
        }
        if(hitTarget){
            SetReward(1.0f);
            target.GetComponent<target>().received_attack=true;
            hitTarget=false;
            Debug.Log("hit target");
            EndEpisode();
        }
        if(hitShell){
            AddReward(0.3f);
            hitShell=false;
        }
        if(notHit){
            AddReward(-0.01f);
            notHit=false;
            Debug.Log("Not hit");
        }
        if(Time.time - start_time >= 20){
            EndEpisode();
        }
        //AddReward(-0.0001f);
    }
}