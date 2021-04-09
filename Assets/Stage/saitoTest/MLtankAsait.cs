using System.Collections;
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
        Debug.Log(Time.deltaTime);
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        int w=26;
        int h=26;
        tankTop.transform.rotation=Quaternion.Euler(0.0f,360*Random.value,0.0f);
        rayCount=0;
        Vector3 newPosition = new Vector3(6+w*(Random.value-0.5f),0.3f,h*(Random.value-0.5f));
        //Vector3 newPosition2 = new Vector3(-16,0.3f,0);
        Vector3 newPosition2 = new Vector3(w*(Random.value-0.5f),0.3f,h*(Random.value-0.5f));
        target.transform.localPosition = newPosition;
        transform.localPosition = newPosition2;
        start_time=Time.time;
    }
    int rayCount=0;
    public int clear=1000;
    public float distLimit=3.0f;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
        var rayOutputs=RayPerceptionSensor.Perceive(rayInput).RayOutputs;
        bool f=true;
        foreach(var element in rayOutputs){
            if(element.HitTagIndex==0){
                AddReward(0.004f);
                //target.GetComponent<target>().hitRay=true;
                //Debug.Log("addreward");
                f=false;
                rayCount++;
            }
        }
        if(f){
            AddReward(-0.1f);
            if(rayCount!=0){
                SetReward(-1.0f);
                EndEpisode();
                //AddReward(-0.02f);
                //AddReward(-0.05f);
                //rayCount=0;
            }
        }
        //float distanceTarget=Vector3.Distance(target.transform.localPosition,transform.localPosition);
        //if(distanceTarget<distLimit){
            //AddReward((distanceTarget-distLimit)/100);
        //}
        //if(rayCount>clear){
            //SetReward(1.0f);
            //EndEpisode();
        //}
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
        if(Time.time - start_time >= 30){
            EndEpisode();
        }
        //AddReward(-0.0001f);
    }
    void OnCollisionEnter(Collision collision){
        var Tag=collision.gameObject.tag;
        if(Tag=="Shell"||Tag=="block"){
            SetReward(-1.0f);
            EndEpisode();
        }
    }
}
