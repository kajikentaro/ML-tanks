using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankC : MLTank
{
    public float endT=0;
    public float now;
    public float limit_rotation=100.0f;
    public float sum_rotation;
    public GameObject target;
    float T=0;
    public override void Initialize()
    {
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        sum_rotation=0;
        int w=25;
        T=Time.time;
        transform.localPosition=new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        target.transform.localPosition=new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
        sum_rotation+=Mathf.Abs(actionBuffers.DiscreteActions[3]);
        now=(Time.time-T)/Time.deltaTime;
        if(now>endT||launch_cnt>20||sum_rotation>limit_rotation){
            gameset(-1.0f);
            //gamesetAll();
        }
    }
}
