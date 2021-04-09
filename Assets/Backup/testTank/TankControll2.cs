using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll2 : MonoBehaviour
{   
	public Vector3 latestPos;
    
    public float speed = 3.0f;
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
}
