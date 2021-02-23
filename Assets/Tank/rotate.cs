using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
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
            transform.LookAt(new Vector3(lookPoint.x,0.5f,lookPoint.z));
        }
    }
    /* 
    private void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint( transform.position );
        var direction = Input.mousePosition - screenPos;
        var angle = GetAim( Vector3.zero, direction );
        transform.SetLocalEulerAnglesY( -angle + 90 );
    }

    public float GetAim( Vector2 from, Vector2 to )
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
    */
}
