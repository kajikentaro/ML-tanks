using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankA_1 : MLTank
{
    public Transform target;
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
        int w=20;
        aliving=true;
        rBody.velocity=Vector3.zero;
        transform.localPosition=new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        target.localPosition=new Vector3(w*(Random.value-0.5f),0.5f,w*(Random.value-0.5f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.localPosition);
        sensor.AddObservation(tankTop.transform.rotation);
        sensor.AddObservation(target.transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
    }
    public void gameset(float reward){
        last_launch_time=0;
        SetReward(reward);
        EndEpisode();
    }
}
