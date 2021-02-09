using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll : MonoBehaviour
{   
    /* 
    public float speed = 3f;
	float moveX = 0f;
	float moveZ = 0f;
	Rigidbody rb;
 
	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
 
	void Update () {
		moveX = Input.GetAxis ("Horizontal") * speed;
		moveZ = Input.GetAxis ("Vertical") * speed;
		Vector3 direction = new Vector3(moveX , 0, moveZ);
	}
 
	void FixedUpdate(){
		rb.velocity = new Vector3(moveX, 0, moveZ);
	}
    */
    public float speed = 3.0f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("up")) {
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey ("down")) {
			transform.position -= transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey("right")) {
			transform.position += transform.right * speed * Time.deltaTime;
		}
		if (Input.GetKey ("left")) {
			transform.position -= transform.right * speed * Time.deltaTime;
		}
	}
}
