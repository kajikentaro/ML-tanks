using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject footPrintPrefab;
    float pre_time;
    Vector3 pre_pos;
    float sum_pos=0;
    public float interval=0.4f;
    void Start(){
        //pre_time=Time.time;
        pre_pos=this.transform.position;

    }
    void Update()
    {
        Vector3 current_pos=this.transform.position;
        sum_pos+=Vector3.Distance(current_pos, pre_pos);
        pre_pos=this.transform.position;
        if(sum_pos>interval){
            sum_pos=0;
            current_pos.y=0.01f;
            Instantiate (footPrintPrefab, current_pos, transform.rotation);
        }
        /*if (Time.time-pre_time>0.1f)
        {
            pre_time=Time.time;
            Vector3 a=this.transform.position;
            a.y=0.01f;
            Instantiate (footPrintPrefab, a, transform.rotation);
        }*/
    }
    
}
