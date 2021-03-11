using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class RootTank : Agent
{
    public bool EnableMove=false;
    public bool EnableSetMine=false;
    public float speedTank = 1.0f;
	public bool aliving=true;
    private float a=100.0f;
    public void forwardTank(float delta){
        if(EnableMove){
            transform.position +=a*transform.forward * speedTank * delta;
        }
    }
    public void backwardTank(float delta){
        if(EnableMove){
            transform.position -= a*transform.forward * speedTank * delta;
        }
    }
    public void rightTank(float delta){
        if(EnableMove){
            transform.position += a*transform.right * speedTank * delta;
        }
    }
    public void leftTank(float delta){
        if(EnableMove){
            transform.position -= a*transform.right * speedTank * delta;
        }
    }
    public void DestroyTank(){
        GameObject refObj;
        refObj = GameObject.Find("Exposion");
        effectStart es = refObj.GetComponent<effectStart>();
        es.startEffect();
        // このスクリプトがついているオブジェクトを破壊する（thisは省略が可能）
        Destroy(gameObject.transform.Find("tank").gameObject);
        //Destroy(gameObject.transform.Find("bottom").gameObject);
        GameObject scriptholder = GameObject.Find("ScriptHolder");
        if (scriptholder != null)
        {
            StageMaker stagemaker = scriptholder.GetComponent<StageMaker>();
            stagemaker.dropout_tank();
            aliving = false;
        }
    }
}
