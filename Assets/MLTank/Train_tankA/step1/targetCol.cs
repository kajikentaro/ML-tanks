using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetCol : MonoBehaviour
{

    public  GameObject ml;
    MLtankA_1 ml_script;
    void Start(){
        ml_script = ml.GetComponent<MLtankA_1>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shell")
        {
            ml_script.gameset(1.0f);
        }
    }
}
