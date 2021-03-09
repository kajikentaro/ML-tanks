using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLTank: RootTank
{
    public Rigidbody rBody;
    public GameObject tankTop;
    public  GameObject shotShell;
    public GameObject Shells;
    public float launch_cnt=0;
    public int shotInterval=10;
    rotate tankTop_script;
    ShotShell shotShell_script;
    //public float launch_frequency_persec=0.2f;
    float last_launch_time=0;
    //bool launch_flag=true;
	void Start(){
		aliving = true;
        //Time.timeScale=1.0f;
        Debug.Log(Time.timeScale);
        tankTop_script = tankTop.GetComponent<rotate>();
        shotShell_script = shotShell.GetComponent<ShotShell>();
        rBody=GetComponent<Rigidbody>();
	}
    void Update(){
        if(!EnableMove){
            rBody.velocity=Vector3.zero;
        }
    }
    public override void OnEpisodeBegin(){
        launch_cnt=0;
        shotShell_script.shellNum=0;
        foreach(Transform child in Shells.transform){
            GameObject.Destroy(child.gameObject);
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition.x);
        sensor.AddObservation(this.transform.localPosition.z);
        sensor.AddObservation(tankTop.transform.rotation.y);
        sensor.AddObservation(shotShell_script.maxShellNum-shotShell_script.shellNum);
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
            if((Time.time-last_launch_time)>shotInterval*Time.deltaTime){
                if(shotShell_script.shellNum<shotShell_script.maxShellNum){
                    shotShell_script.shotShell();
                    last_launch_time=Time.time;
                    launch_cnt+=1;
                }
            }
        }
        tankTop_script.rotateByFloat(actionBuffers.DiscreteActions[3]-1);
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
            discreteActionsOut[2] = 2;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            discreteActionsOut[3] = -1;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            discreteActionsOut[3] = 1;
        }
        else
        {
            discreteActionsOut[3] = 0;
        }
    }
}

