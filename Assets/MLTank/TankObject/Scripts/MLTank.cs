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
    public GameObject target;
    void FixedUpdate(){
        base.FixedUpdate();
        if(received_attack&&!learningMode){
            script_holder.GetComponent<StageMaker>().dead_enemy();
        }
    }
	public override void Initialize(){
        base.Initialize();
        rBody=GetComponent<Rigidbody>();
        tankTop_script = tankTop.GetComponent<rotate>();
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
        sensor.AddObservation(target.transform.localPosition.x);
        sensor.AddObservation(target.transform.localPosition.z);
    }
    public void action_control(ActionBuffers actionBuffers){
        Vector3 Speed=Vector3.zero;
        if(!BanAction){
            if (actionBuffers.DiscreteActions[0] == 1)
            {
                Speed.z=1.0f;
            }
            if (actionBuffers.DiscreteActions[0] == 2)
            {
                Speed.z=-1.0f;
            }
            if(actionBuffers.DiscreteActions[1] == 1)
            {
                Speed.x=1.0f;
            }
            if(actionBuffers.DiscreteActions[1] == 2)
            {
                Speed.x=-1.0f;
            }
            decide_speed(Speed);
            if(actionBuffers.DiscreteActions[2] == 1)
            {
                shotShell();
            }
            tankTop_script.rotateByFloat(actionBuffers.ContinuousActions[0]);
            tankTop_script.rotateByFloat(-actionBuffers.ContinuousActions[1]);
        }
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
    private void OnCollisionEnter(Collision other)
    {
        //public GameObject top = transform.Find("top").gameObject;
        //public GameObject bottom = transform.Find("bottom").gameObject;
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば（条件）
        if (other.gameObject.tag == "Shell"&&!learningMode)
        {
            script_holder.GetComponent<effectStart>().startEffect(this.transform.localPosition);
            script_holder.GetComponent<StageMaker>().dead_enemy();
            DestroyTank();
        }
    }

}

