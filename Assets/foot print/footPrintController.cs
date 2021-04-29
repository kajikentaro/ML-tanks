using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footPrintController : MonoBehaviour 
{
	void Start () {
        StartCoroutine (Disappearing ());
	}
	
    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(5.0f);
        int step = 90;
        for (int i = 0; i < step; i++)
        {
            GetComponent<MeshRenderer> ().material.color = new Color (1, 1, 1, 1 - 1.0f * i / step);
            yield return new WaitForSeconds(0.04f);
        }
        Destroy (gameObject);
    }
}