using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLTank: RootTank
{
    Rigidbody rBody;
    rotate tankTop_script;
	public override void Initialize(){
        tankTop_script = tankTop.GetComponent<rotate>();
        rBody=GetComponent<Rigidbody>();
	}
    void Update(){
        if(!EnableMove){
            rBody.velocity=Vector3.zero;
        }
    }
    public override void OnEpisodeBegin(){
        launch_cnt=0;
        shellNum=0;
        foreach(Transform child in Shells.transform){
            GameObject.Destroy(child.gameObject);
        }
        last_launch_time = -100f;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition.x);
        sensor.AddObservation(this.transform.localPosition.z);
        sensor.AddObservation(tankTop.transform.rotation.y);
        sensor.AddObservation(maxShellNum-shellNum);
    }
    public void action_control(ActionBuffers actionBuffers){
        rBody.velocity=Vector3.zero;
        if(EnableMove){
            if (actionBuffers.DiscreteActions[0] == 1)
            {
                forwardTank(Time.deltaTime);
            }
            if (actionBuffers.DiscreteActions[0] == 2)
            {
                backwardTank(Time.deltaTime);
            }
            if(actionBuffers.DiscreteActions[1] == 1)
            {
                rightTank(Time.deltaTime);
            }
            if(actionBuffers.DiscreteActions[1] == 2)
            {
                leftTank(Time.deltaTime);
            }
        }
        if(actionBuffers.DiscreteActions[2] == 1)
        {
            shotShell();
        }
        tankTop_script.rotateByFloat(actionBuffers.ContinuousActions[0]);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        action_control(actionBuffers);
    }
    public void gameset(float reward){
        SetReward(reward);
        EndEpisode();
    }
    public void gamesetAll(){
        GameObject [] objs=GameObject.FindGameObjectsWithTag("tank");
        foreach(GameObject obj in objs){
            obj.GetComponent<MLTank>().gameset(0.0f);
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        var continuousActionOut = actionsOut.ContinuousActions;
        if (Input.GetKey("w"))
        {
            discreteActionsOut[0] = 1;
        }else if (Input.GetKey("s"))
        {
            discreteActionsOut[0] = 2;
        }
        else
        {
            discreteActionsOut[0] = 0;
        }
        if (Input.GetKey("d"))
        {
            discreteActionsOut[1] = 1;
        }else if (Input.GetKey("a"))
        {
            discreteActionsOut[1] = 2;
        }
        else
        {
            discreteActionsOut[1] = 0;
        }
        if (Input.GetKey(KeyCode.Space)){
            discreteActionsOut[2] = 1;
        }
        else
        {
            discreteActionsOut[2] = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            continuousActionOut[0] = 1;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            continuousActionOut[0] = -1;
        }
        else
        {
            continuousActionOut[0] = 0;
        }
    }
}

