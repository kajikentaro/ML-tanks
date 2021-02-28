using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTank : MonoBehaviour
{
    public bool EnableMove=false;
    public float speedTank = 3.0f;
	public bool aliving=true;
    public float launch_frequency_persec=0.2f;
    public void forwardTank(float delta){
        transform.position += transform.forward * speedTank * delta;
    }
    public void backwardTank(float delta){
        transform.position -= transform.forward * speedTank * delta;
    }
    public void rightTank(float delta){
        transform.position += transform.right * speedTank * delta;
    }
    public void leftTank(float delta){
        transform.position -= transform.right * speedTank * delta;
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
