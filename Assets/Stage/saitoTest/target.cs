using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class target : MonoBehaviour
{
    public GameObject tank; 
    public bool hitRay=false;
    public float speedTarget=1.0f;
    float multix=0;
    float multiz=1;
    public bool received_attack;
    void FixedUpdate(){
        int w=12;
        float x=this.transform.localPosition.x;
        float z=this.transform.localPosition.z;
        if(x==w&&z>w){
            multix=-1;
            multiz=0;
            this.transform.localPosition=new Vector3(w,1.0f,w);
        }
        else if(z==w&&x<-w){
            multix=0;
            multiz=-1;
            this.transform.localPosition=new Vector3(-w,1.0f,w);
        }
        else if(x==-w&&z<-w){
            multix=1;
            multiz=0;
            this.transform.localPosition=new Vector3(-w,1.0f,-w);
        }
        else if(z==-w&&x>w){
            multix=0;
            multiz=1;
            this.transform.localPosition=new Vector3(w,1.0f,-w);
        }
        this.transform.localPosition+=new Vector3(Random.value*multix,0.0f,Random.value*multiz)*speedTarget;
        if(hitRay){
            hitRay=false;
            GetComponent<Renderer>().material.color=Color.red;
        }
        else {
            GetComponent<Renderer>().material.color=Color.blue;
        }
    }
}
