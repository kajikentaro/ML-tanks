using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    Plane plane = new Plane();
    float distance = 0;
    // Update is called once per frame
    public void rotateByMouse()
    {
        var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        plane.SetNormalAndPosition(Vector3.up,transform.localPosition);
        if(plane.Raycast(ray,out distance)){
            var lookPoint = ray.GetPoint(distance);
            transform.LookAt(new Vector3(lookPoint.x,0.5f,lookPoint.z));
            Vector3 angl = transform.eulerAngles;
            transform.eulerAngles = new Vector3(0, angl.y, 0);
        }
    }
    float forceAdjustNum = 4.0f;
    public void rotateByFloat(GameObject target,bool f,float input = 1)
    {
        float nowAngle = transform.eulerAngles.y;
        nowAngle += input * forceAdjustNum;
        if(f){
		    Vector3 targetDir = target.transform.position - transform.position; 
		    float rotate_speed = 3.0f;
		    float step = rotate_speed*Time.deltaTime;
		    Vector3 newDir = Vector3.RotateTowards(transform.forward,targetDir,step,10.0F);
		    transform.rotation = Quaternion.LookRotation(newDir);
        }
        else{
            transform.eulerAngles = Vector3.up * nowAngle;
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
