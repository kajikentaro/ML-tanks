using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMoveKaji : MonoBehaviour
{
    float move_dir_time;
    int rand;
    Rigidbody rBody;
    // Start is called before the first frame update
    void Start()
    {
        move_dir_time = Time.time;
        rand = Random.Range(0,4);
        rBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -2)
        {
            transform.position = new Vector3(0, 1.1f, 2);
        }
        //Debug.Log(Time.time);
        float elapsed_time = Time.time - move_dir_time;
        if(elapsed_time > 1)
        {
            move_dir_time = Time.time;
            rand = Random.Range(0,4);
        }
        Vector3[] dirs = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        rBody.velocity = dirs[rand] * 3f;
    }
}
