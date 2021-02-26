using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetCol : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject brain;
    MLTankBrain brain_script;
    void Start()
    {
        brain_script=brain.GetComponent<MLTankBrain>();
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag=="Shell"){
            brain_script.gameset(1.0f);
        }
    }
}
