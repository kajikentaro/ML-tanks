using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class targetML : Agent
{
    Rigidbody rBody;
    public GameObject tankTop;
    public GameObject tankObj;
    MLtankA_1 script;
    public float targetSpeed=4.0f;
    public float t=0;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shell")
        {
            //SetReward(-1.0f);
            script.gameset(1.0f);
            EndEpisode();
            Debug.Log("tank win");
        }
    }

    public override void Initialize()
    {
        rBody=GetComponent<Rigidbody>();
        script=tankObj.GetComponent<MLtankA_1>();
    }
    public override void OnEpisodeBegin()
    {
        int w=20;
        rBody.velocity=Vector3.zero;
        transform.localPosition=new Vector3(w*(Random.value-0.5f),0.5f,w*(Random.value-0.5f));
        t=0;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(tankTop.transform.rotation);
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
        if(t>1000){
            SetReward(1.0f);
            EndEpisode();
        }
        else t+=1;
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