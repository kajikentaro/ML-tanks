using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll : MonoBehaviour
{   
	public Vector3 latestPos;
    
    public float speed = 3.0f;
	//別のオブジェクトから参照する
	GameObject refObj;
	//public float rotate_speed = 3.0f;
	// Update is called once per frame
	void Update () {
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
			//Exposionオブジェクトを参照
			//startEffect()はeffectStartスクリプトのメソッド
			//この場合は戦車が左に進んだら爆発する
			refObj=GameObject.Find("Exposion");
			effectStart es=refObj.GetComponent<effectStart>();
			es.startEffect();
		}
	}
}
