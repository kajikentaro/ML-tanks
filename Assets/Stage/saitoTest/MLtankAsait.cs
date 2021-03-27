using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankAsait : MLTank
{
    float start_time;
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
        start_time=Time.time;
        int w=30;
        int h=20;
        Vector3 newPosition2 = new Vector3(w*(Random.value-0.5f),0.3f,h*(Random.value-0.5f));
        target.transform.localPosition = newPosition2;
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
        var rayOutputs=RayPerceptionSensor.Perceive(rayInput).RayOutputs;
        foreach(var element in rayOutputs){
            if(element.HitTagIndex==0){
                AddReward(0.0001f);
                Debug.Log("addreward");
            }
        }
        if(Time.time - start_time >= 20){
            //gameset(-1.0f);
            EndEpisode();
            //gamesetAll();
        }
    }
}
