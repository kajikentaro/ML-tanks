using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLBottomRotate : MonoBehaviour
{
    

	public Vector3 latestPos;
	public float rotate_speed = 5.0f;
    void Start () {
		transform.rotation = Quaternion.identity;
	}
    // Update is called once per frame
    void FixedUpdate()
    {
		//if(transform.rotation.x!=0){
			//transform.rotation=Quaternion.Euler(0,transform.rotation.y,0);
		//}
		Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
    	latestPos = transform.position;  //前回のPositionの更新
		if(diff.magnitude >= 0.0000001f)
        {

		float step = rotate_speed*Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward,diff,step,10.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}