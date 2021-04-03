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

    public void reset_position()
    {
        float x = Random.Range(-15f, 15f);
        float y = Random.Range(-10f, 10f);

        transform.position = new Vector3(x, 1, y);
    }
    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x) >= 15 || Mathf.Abs(transform.position.z) >= 10)
        {
            reset_position();
        }
        //Debug.Log(Time.time);
        float elapsed_time = Time.time - move_dir_time;
        if(elapsed_time > 1)
        {
            move_dir_time = Time.time;
            rand = Random.Range(0,4);
        }
        Vector3[] dirs = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        rBody.velocity = dirs[rand] * 4f;
    }
}
