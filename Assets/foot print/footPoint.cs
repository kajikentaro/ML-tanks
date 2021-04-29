using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject footPrintPrefab;
    float time = 0;

    void Update()
    {
        this.time += Time.deltaTime;
        if (this.time > 0.1f || true)
        {
            this.time = 0;
            Vector3 a=this.transform.position;
            a.y=0.01f;
            Instantiate (footPrintPrefab, a, transform.rotation);
        }
    }
    
}
