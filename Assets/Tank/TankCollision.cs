using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCollision : MonoBehaviour
{
    GameObject refObj;
	public int aliving = 1;
    
	private void OnCollisionEnter(Collision other)
    {	
		//public GameObject top = transform.Find("top").gameObject;
		//public GameObject bottom = transform.Find("bottom").gameObject;
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば（条件）
        if (other.gameObject.tag == "Shell")
        {	
			refObj=GameObject.Find("Exposion");
			effectStart es=refObj.GetComponent<effectStart>();
			es.startEffect();
            // このスクリプトがついているオブジェクトを破壊する（thisは省略が可能）
            Destroy(other.gameObject);	
            Destroy(gameObject.transform.Find("tank").gameObject);
			//Destroy(gameObject.transform.Find("bottom").gameObject);
			aliving = 0;

            GameObject scriptholder = GameObject.Find("ScriptHolder");
            if(scriptholder != null)
            {
                StageMaker stagemaker = scriptholder.GetComponent<StageMaker>();
                stagemaker.dropout_tank();
            }
        }
    }
}
