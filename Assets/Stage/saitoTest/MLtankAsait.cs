using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLtankAsait : MLTank
{
    float start_time;
    int NotHitCount;
    public GameObject targetL;  
    public override void Initialize()
    {
        base.Initialize();
        target=targetL;
        target.GetComponent<Rigidbody>().velocity=new Vector3(4.0f,0,4.0f);
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        int w=26;
        int h=26;
        tankTop.transform.rotation=Quaternion.Euler(0.0f,360*Random.value,0.0f);
        Vector3 newPosition = new Vector3(6+w*(Random.value-0.5f),0.3f,h*(Random.value-0.5f));
        //Vector3 newPosition2 = new Vector3(-16,0.3f,0);
        Vector3 newPosition2 = new Vector3(w*(Random.value-0.5f),0.3f,h*(Random.value-0.5f));
        target.transform.localPosition = newPosition;
        transform.localPosition = newPosition2;
        start_time=Time.time;
    }
    public int clear=1000;
    public float distLimit=3.0f;
    bool f=true;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        base.OnActionReceived(actionBuffers);
        //AddReward(-0.0001f);
        if(received_attack){
            //SetReward(-1.0f);
            received_attack=false;
            //Debug.Log("received_attack");
            EndEpisode();
        }
        if(hitTank){
            SetReward(-0.5f);
            hitTank=false;
            //Debug.Log("hit tank");
            EndEpisode();
        }
        if(hitTarget){
            SetReward(1.0f);
            //target.GetComponent<MLtankAsait>().received_attack=true;
            hitTarget=false;
            Debug.Log("hit target");
            EndEpisode();
        }
        if(hitShell){
            //AddReward(0.3f);
            hitShell=false;
        }
        if(notHit){
            //AddReward(-0.01f);
            notHit=false;
            Debug.Log("Not hit");
        }
        //if(Time.time - start_time >= 50){
            //SetReward(-1.0f);
            //EndEpisode();
        //}
    }
    void OnCollisionEnter(Collision collision){
        var Tag=collision.gameObject.tag;
        if(Tag=="Shell"||Tag=="block"){
            AddReward(-0.5f);
            //EndEpisode();
        }
    }
}
