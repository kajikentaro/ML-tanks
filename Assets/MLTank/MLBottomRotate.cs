﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLBottomRotate : MonoBehaviour
{
    

	public Vector3 latestPos;
    void Start () {
		transform.rotation = Quaternion.identity;
	}
    // Update is called once per frame
    void Update()
    {
		Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
    	latestPos = transform.position;  //前回のPositionの更新
		float rotate_speed = 5f;

		float step = rotate_speed*Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward,diff,step,10.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
    }
}