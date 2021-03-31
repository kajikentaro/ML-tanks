using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class target : Agent
{
    float start_time;
    public GameObject tank; 
    public bool received_attack=false;
    public bool hitRay=false;
    public int cnt;
    public int cnt_limit=2000;
    Rigidbody rBody;
    public override void Initialize()
    {
        base.Initialize();
        rBody=GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        cnt=0;
        int w=30;
        int h=20;
        Vector3 newPosition2 = new Vector3(w*(Random.value-0.5f),1.0f,h*(Random.value-0.5f));
        transform.localPosition = newPosition2;
        start_time=Time.time;
    }
    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(tank.transform.localPosition);
        //sensor.AddObservation(tank1.transform.localPosition);
        //sensor.AddObservation(tank2.transform.localPosition);
        //sensor.AddObservation(tank3.transform.localPosition);
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }
    public float speed=0.1f;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //transform.localPosition=new Vector3(transform.localPosition.x,1.0f,transform.localPosition.z);
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.DiscreteActions[0]-1;
        controlSignal.z = actionBuffers.DiscreteActions[1]-1;
        this.transform.localPosition+=controlSignal*speed;
        if(hitRay){
            hitRay=false;
            //Debug.Log("HitRay");
            cnt+=1;
            GetComponent<Renderer>().material.color=Color.red;
        }
        else {
            GetComponent<Renderer>().material.color=Color.blue;
            AddReward(0.001f);
        }
        if(cnt>cnt_limit){
            SetReward(-1.0f);
            EndEpisode();
        }
        if(received_attack){
            SetReward(-1.0f);
            received_attack=false;
            Debug.Log("target received_attack");
            EndEpisode();
        }
        if(Time.time - start_time >= 20){
            SetReward(1.0f);
            EndEpisode();
        }
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag=="block"){
            Debug.Log("collision block");
            SetReward(-1.0f);
            EndEpisode();
        }
    }
}
