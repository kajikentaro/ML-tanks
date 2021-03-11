using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankA : MLTank
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
        transform.localPosition=new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        Vector3 newPosition = new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        float limit = target.transform.localScale.x / 2 * 1.414f + 1.5f;
        while (true)
        {
            if (Vector3.Distance(newPosition, transform.localPosition) >= limit)break;
            newPosition = new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        }
        target.transform.localPosition = newPosition;
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        RaycastHit raycastHit;
        Physics.Raycast(tankTop.transform.position, tankTop.transform.forward, out raycastHit, 30);
        if (raycastHit.collider != null && raycastHit.collider.name == "target")
        {
            target.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            target.GetComponent<Renderer>().material.color = Color.blue;
            AddReward(-0.005f);
        }

        action_control(actionBuffers);
        if(Time.time - start_time >= 20||launch_cnt>20){
            gameset(-1.0f);
            //gamesetAll();
        }
    }
}
