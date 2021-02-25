using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLrotate : MonoBehaviour
{
    float forceAdjustNum = 10.0f;
    public void rotateByFloat(float input = 1)
    {
        float nowAngle = transform.eulerAngles.y;
        nowAngle += input * forceAdjustNum;
        transform.eulerAngles = Vector3.up * nowAngle;
    }
}   
