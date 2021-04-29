using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject footPrintPrefab;
    float pre_time;
    void Start(){
        pre_time=Time.time;
    }
    void Update()
    {
        if (Time.time-pre_time>0.1f)
        {
            pre_time=Time.time;
            Vector3 a=this.transform.position;
            a.y=0.01f;
            Instantiate (footPrintPrefab, a, transform.rotation);
        }
    }
    
}
