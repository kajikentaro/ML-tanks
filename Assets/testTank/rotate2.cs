using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate2 : MonoBehaviour
{
    Plane plane = new Plane();
    float distance = 0;
    // Update is called once per frame
    void Update()
    {   
        var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        plane.SetNormalAndPosition(Vector3.up,transform.localPosition);
        if(plane.Raycast(ray,out distance)){
            var lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);
        }
    }
}
