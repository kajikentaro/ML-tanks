using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLTank: RootTank
{
    Rigidbody rBody;
    public Transform target;
    public GameObject tankTop;
    public GameObject shotShell;
    rotate tankTop_script;
    ShotShell shotShell_script;
    public float launch_frequency_persec=0.2f;
    float last_launch_time=0;
    bool launch_flag=true;
	void Start(){
		aliving = true;
        tankTop_script = tankTop.GetComponent<rotate>();
        shotShell_script = shotShell.GetComponent<ShotShell>();
        rBody=GetComponent<Rigidbody>();
	}
    void Update(){
        if((Time.time-last_launch_time)>1/launch_frequency_persec){
            launch_flag=true;
        }
    }
	//public float rotate_speed = 3.0f;
	//Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shell")
        {
            gameset(-1.0f);
        }
    }
    public override void Initialize()
    {
    }
    public override void OnEpisodeBegin()
    {
        int w=20;
        rBody.velocity=Vector3.zero;
        transform.localPosition=new Vector3(w*(Random.value-0.5f),0.3f,w*(Random.value-0.5f));
        target.localPosition=new Vector3(w*(Random.value-0.5f),0.5f,w*(Random.value-0.5f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.localPosition);
        sensor.AddObservation(tankTop.transform.rotation);
        sensor.AddObservation(target.transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if(EnableMove){
            if (actionBuffers.DiscreteActions[0] == 1)
            {
                transform.position += transform.forward * speedTank * Time.deltaTime;
            }
            if (actionBuffers.DiscreteActions[0] == 2)
            {
                transform.position -= transform.forward * speedTank * Time.deltaTime;
            }
            if(actionBuffers.DiscreteActions[1] == 1)
            {
                transform.position += transform.right * speedTank * Time.deltaTime;
            }
            if(actionBuffers.DiscreteActions[1] == 2)
            {
                transform.position -= transform.right * speedTank * Time.deltaTime;
            }
        }
        if(actionBuffers.DiscreteActions[2] == 1&&launch_flag)
        {
            shotShell_script.shotShell();
            last_launch_time=Time.time;
            launch_flag=false;
        }
        tankTop_script.rotateByFloat(actionBuffers.ContinuousActions[0]);
    }
    public void gameset(float reward){
        SetReward(reward);
        EndEpisode();
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
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
            continuousActionsOut[0] = -1;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            continuousActionsOut[0] = 1;
        }
        else
        {
            continuousActionsOut[0] = 0;
        }
    }
}

