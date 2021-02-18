using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll : MonoBehaviour
{   
	public Vector3 latestPos;
    
    public float speed = 3.0f;
	//別のオブジェクトから参照する
	GameObject refObj;
	public int aliving = 1;

	void Start(){
		aliving = 1;
	}
	//public float rotate_speed = 3.0f;
	// Update is called once per frame
	void Update () {
		if(aliving == 1){
			//平行移動
			if (Input.GetKey ("w")) {
				transform.position += transform.forward * speed * Time.deltaTime;
			}
			if (Input.GetKey ("s")) {
				transform.position -= transform.forward * speed * Time.deltaTime;
			}
			if (Input.GetKey("d")) {
				transform.position += transform.right * speed * Time.deltaTime;
			}
			if (Input.GetKey ("a")) {
				transform.position -= transform.right * speed * Time.deltaTime;
			}
		}
	}
	public void destroy_obj()
    {
			refObj=GameObject.Find("Exposion");
			effectStart es=refObj.GetComponent<effectStart>();
			es.startEffect();
            // このスクリプトがついているオブジェクトを破壊する（thisは省略が可能）
            Destroy(gameObject.transform.Find("top").gameObject);
			Destroy(gameObject.transform.Find("bottom").gameObject);
			aliving = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Mine")
        {
			if(aliving != 0) destroy_obj();
        }
    }
    private void OnCollisionEnter(Collision other)
    {	
		//public GameObject top = transform.Find("top").gameObject;
		//public GameObject bottom = transform.Find("bottom").gameObject;
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば（条件）
        if (other.gameObject.tag == "Shell")
        {
			destroy_obj();
            Destroy(other.gameObject);		
        }
        if(other.gameObject.tag == "Mine")
        {
			//destroy_obj();
        }
    }
}
