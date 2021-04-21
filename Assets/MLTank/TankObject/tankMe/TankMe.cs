using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class TankMe : RootTank
{
    //public float rotate_speed = 3.0f;
    //Update is called once per frame
    rotate rotate_script;
    void Start(){
        rotate_script = this.tankTop.GetComponent<rotate>();
    }
    void FixedUpdate()
    {
        if (!BanAction)
        {
            base.FixedUpdate();
            Vector3 Speed=Vector3.zero;
            //平行移動
            if (Input.GetKey("w"))
            {
                Speed.z=1.0f;
            }
            if (Input.GetKey("s"))
            {
                Speed.z=-1.0f;
            }
            if (Input.GetKey("d"))
            {
                Speed.x=1.0f;
            }
            if (Input.GetKey("a"))
            {
                Speed.x=-1.0f;
            }
            decide_speed(Speed);
            if (Input.GetMouseButtonDown(0)){
                shotShell();
            }
        rotate_script.rotateByMouse();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //public GameObject top = transform.Find("top").gameObject;
        //public GameObject bottom = transform.Find("bottom").gameObject;
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば（条件）
        if (other.gameObject.tag == "Shell")
        {
            Destroy(other.gameObject);
            DestroyTank();
        }
    }
}
