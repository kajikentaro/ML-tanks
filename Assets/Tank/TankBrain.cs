using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBrain: MonoBehaviour
{
    GameObject refObj;
    public float speed = 3.0f;
	bool aliving;
	void Start(){
		aliving = true;
	}
	//public float rotate_speed = 3.0f;
	//Update is called once per frame
	void Update()
	{
		if (aliving == true && StageMaker.canMove)
		{
			//平行移動
			if (Input.GetKey("w"))
			{
				transform.position += transform.forward * speed * Time.deltaTime;
			}
			if (Input.GetKey("s"))
			{
				transform.position -= transform.forward * speed * Time.deltaTime;
			}
			if (Input.GetKey("d"))
			{
				transform.position += transform.right * speed * Time.deltaTime;
			}
			if (Input.GetKey("a"))
			{
				transform.position -= transform.right * speed * Time.deltaTime;
			}
		}
	}
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
            aliving = false;
			//Destroy(gameObject.transform.Find("bottom").gameObject);

            GameObject scriptholder = GameObject.Find("ScriptHolder");
            if(scriptholder != null)
            {
                StageMaker stagemaker = scriptholder.GetComponent<StageMaker>();
                stagemaker.dropout_tank();
				aliving = false;
            }
        }
    }
}
