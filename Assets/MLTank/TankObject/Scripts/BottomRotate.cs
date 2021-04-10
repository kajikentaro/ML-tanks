using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomRotate : MonoBehaviour
{
    

    public float speed = 3.0f;
   //public float rotate_speed = 3.0f;
	public Vector3 latestPos;
    void Start () {
		transform.rotation = Quaternion.identity;
	}
    // Update is called once per frame
    void FixedUpdate()
    {
        //平行移動
        /* 
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
		}*/
		//回転移動
		Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
    	latestPos = transform.position;  //前回のPositionの更新
		float rotate_speed = 5f;

		float step = rotate_speed*Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward,diff,step,10.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
    }
}