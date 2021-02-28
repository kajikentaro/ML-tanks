using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLBottomRotate : MonoBehaviour
{
    

	public Vector3 latestPos;
	public bool EnableMove=false;
    void Start () {
		transform.rotation = Quaternion.identity;
	}
    // Update is called once per frame
    void Update()
    {
		Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
    	latestPos = transform.position;  //前回のPositionの更新
		if(EnableMove&&diff.magnitude >= 0.0000001f)
        {
		float rotate_speed = 25f;

		float step = rotate_speed*Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward,diff,step,10.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}