using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankA_1 : MLTank
{
    public Transform target;
    public float t=0;
    public float limit_rotation=100.0f;
    public float sum_rotation;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shell")
        {
            gameset(-1.0f);
        }
    }
    public override void Initialize()
    {
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        sum_rotation=0;
        int w=20;
        aliving=true;
        t=0;
        rBody.velocity=Vector3.zero;
        transform.localPosition=new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        //target.localPosition=new Vector3(w*(Random.value-0.5f),0.5f,w*(Random.value-0.5f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(tankTop.transform.rotation);
        sensor.AddObservation(target.transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
        sum_rotation+=Mathf.Abs(actionBuffers.ContinuousActions[0]);
        if(t>200||launch_cnt>5||sum_rotation>limit_rotation){
            gameset(-1.0f);
            Debug.Log("target win");
        }
        else t+=1;
    }
    public void gameset(float reward){
        SetReward(reward);
        EndEpisode();
    }
}
