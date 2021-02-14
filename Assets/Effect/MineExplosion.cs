using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomMy : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rBody;
    void Start()
    {
        this.rBody = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 0.05f);
    }
}
