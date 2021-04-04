using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using Unity.Barracuda;


public class kajitankB : MLTank
{
    float start_time;
    public override void OnEpisodeBegin()
    {
        target.GetComponent<targetMoveKaji>().reset_position();
        start_time = Time.time;
        hitTarget = false;
        transform.position = transform.parent.position;
        launch_cnt = 0;
        shellNum = 0;
        foreach (Transform child in Shells.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        last_launch_time = -100f;
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Debug.Log(hitTarget);
        if (hitTarget == true)
        {
            Debug.Log("hitted");
            AddReward(1.0f);
            EndEpisode();
        }
        AddReward(-0.005f);
        if (Time.time - start_time >= 30)
        {
            EndEpisode();
        }
        action_control(actionBuffers);
    }
}
