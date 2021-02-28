using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float v=0.1f;
    public int t=0;
    public int T=500;
    void Update()
    {
        Vector3 pos=transform.forward;
        pos*=v;
        transform.position+=pos;
        if(t%T==0){
            transform.Rotate(0.0f,90f,0.0f);
        }
        t+=1;
    }
}
