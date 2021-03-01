using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class targetML : Agent
{
    Rigidbody rBody;
    public GameObject tankObj;
    MLtankA_1 script;
    public float targetSpeed=4.0f;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shell")
        {
            SetReward(-1.0f);
            script.gameset(1.0f);
            EndEpisode();
        }
    }

    public override void Initialize()
    {
        rBody=GetComponent<Rigidbody>();
        script=tankObj.GetComponent<MLtankA_1>();
    }
    public override void OnEpisodeBegin()
    {
        rBody.velocity=Vector3.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(tankObj.transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (actionBuffers.DiscreteActions[0] == 1)
        {
            transform.localPosition +=targetSpeed* transform.forward * Time.deltaTime;
        }
        if (actionBuffers.DiscreteActions[0] == 2)
        {
            transform.localPosition -=targetSpeed*transform.forward * Time.deltaTime;
        }
        if(actionBuffers.DiscreteActions[1] == 1)
        {
            transform.localPosition +=targetSpeed* transform.right * Time.deltaTime;
        }
        if(actionBuffers.DiscreteActions[1] == 2)
        {
            transform.localPosition -=targetSpeed* transform.right * Time.deltaTime;
        }
        if(script.t>2000){
            SetReward(1.0f);
            script.gameset(-1.0f);
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey("w"))
        {
            discreteActionsOut[0] = 1;
        }
    }
}