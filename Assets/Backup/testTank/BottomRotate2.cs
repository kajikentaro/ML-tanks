using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomRotate2 : MonoBehaviour
{
    

    public float speed = 3.0f;
   //public float rotate_speed = 3.0f;
	public Vector3 latestPos;
    void Start () {
		transform.rotation = Quaternion.identity;
	}
    // Update is called once per frame
    void Update()
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

    	//ベクトルの大きさが0.01以上の時に向きを変える処理をする
    	if (diff.magnitude > 0.01f)
    	{
        	transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
    	}
    }
}