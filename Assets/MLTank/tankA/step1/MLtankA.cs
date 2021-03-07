using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankA : MLTank
{
    public float t=0;
    public float limit_rotation=100.0f;
    public float sum_rotation;
    public bool targetMode=false;
    public override void Initialize()
    {
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        sum_rotation=0;
        int w=20;
        t=0;
        transform.localPosition=new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
        sum_rotation+=Mathf.Abs(actionBuffers.ContinuousActions[0]);
        if(t>200||launch_cnt>5||sum_rotation>limit_rotation){
            if(!targetMode)gameset(-1.0f);
        }
        else t+=1;
    }
}
