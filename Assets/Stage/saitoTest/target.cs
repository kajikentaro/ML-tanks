using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class target : Agent
{
    float start_time;
    public GameObject tank1;
    public GameObject tank2;
    public GameObject tank3;
    public bool received_attack=false;
    public bool hitRay=false;
    Rigidbody rBody;
    public override void Initialize()
    {
        base.Initialize();
        rBody=GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        int w=30;
        int h=20;
        Vector3 newPosition2 = new Vector3(w*(Random.value-0.5f),0.3f,h*(Random.value-0.5f));
        transform.localPosition = newPosition2;
        start_time=Time.time;
    }
    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(tank1.transform.localPosition);
        sensor.AddObservation(tank2.transform.localPosition);
        sensor.AddObservation(tank3.transform.localPosition);
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }
    public float  forceMultiplier=10.0f;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        transform.localPosition=new Vector3(transform.localPosition.x,1.0f,transform.localPosition.z);
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);
        if(hitRay){
            AddReward(0.001f);
            hitRay=false;
            //Debug.Log("HitRay");
        }
        if(received_attack){
            SetReward(-1.0f);
            received_attack=false;
            Debug.Log("target received_attack");
            EndEpisode();
        }
        if(this.transform.localPosition.y<0){
            SetReward(-1.0f);
            Debug.Log("fall");
            EndEpisode();
        }
        if(Time.time - start_time >= 20){
            SetReward(1.0f);
            EndEpisode();
        }
        //AddReward(-0.0001f);
    }
}
