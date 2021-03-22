using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankB : MLTank
{
    public float limit_rotation=100.0f;
    public float sum_rotation;
    public GameObject target;
    float start_time;
    public override void Initialize()
    {
    }
    public override void OnEpisodeBegin()
    {
        start_time = Time.time;
        base.OnEpisodeBegin();
        sum_rotation=0;
        int w=25;
        Vector3 newPosition1 = new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        transform.localPosition = newPosition1;
        /*
        Vector3 newPosition2 = new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        float limit = target.transform.localScale.x / 2 * 1.414f + 1.5f;
        while (true)
        {
            if (Vector3.Distance(newPosition1,newPosition2) >= limit)break;
            newPosition1 = new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
            newPosition2 = new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        }
        transform.localPosition = newPosition1;
        target.transform.localPosition = newPosition2;
        */
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        /*
        RaycastHit raycastHit;
        Physics.Raycast(tankTop.transform.position, tankTop.transform.forward, out raycastHit, 30);
        if (raycastHit.collider != null && raycastHit.collider.name == "target")
        {
            //target.GetComponent<Renderer>().material.color = Color.red;
            //AddReward(0.01f);
        }
        else
        {
            target.GetComponent<Renderer>().material.color = Color.blue;
            //AddReward(-0.001f);
        }
        */
        AddReward(-0.005f);

        action_control(actionBuffers);
        if(Time.time - start_time >= 20){
            //gameset(-1.0f);
            EndEpisode();
            //gamesetAll();
        }
    }
}
