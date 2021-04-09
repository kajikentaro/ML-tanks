using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCollision2 : MonoBehaviour
{
    GameObject refObj;
	public int aliving = 1;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shell")
        {
            if(aliving != 0)Destroy(other.gameObject);
            destroy_obj();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Mine")
        {
			if(aliving != 0) destroy_obj();
        }
    }
	public void destroy_obj()
    {
			refObj=GameObject.Find("Exposion");
			effectStart es=refObj.GetComponent<effectStart>();
			es.startEffect();
            // このスクリプトがついているオブジェクトを破壊する（thisは省略が可能）
            Destroy(gameObject.transform.Find("top").gameObject);
			Destroy(gameObject.transform.Find("bottom").gameObject);
			aliving = 0;
    }
}
