using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineScript : MonoBehaviour
{
    public int ExplodeTime;
    public GameObject tank_gameobject;
    bool explodeFlag=false;
    public float startT;
    void Start()
    {
        startT=Time.time;
    }
    void Update()
    {
        if(Time.time-startT>ExplodeTime){
            explodeFlag=true;
            startT=Time.time;
        }
        else if(explodeFlag&&Time.time-startT>1){
            tank_gameobject.GetComponent<MLTank>().currentMineNum-=1;
            Destroy(this.gameObject);
        }
    }
    void OnTriggerStay(Collider collision){
        Debug.Log(collision.gameObject.tag);
        if(explodeFlag){
            if(collision.gameObject.tag=="tank"){
                collision.gameObject.GetComponent<MLTank>().gameset(-0.1f);
                if(tank_gameobject!=collision.gameObject)tank_gameobject.GetComponent<MLTank>().gameset(1.0f);
            }
            else if(collision.gameObject.tag=="target"){
                tank_gameobject.GetComponent<MLTank>().gameset(1.0f);
            }
        }
    }
}
