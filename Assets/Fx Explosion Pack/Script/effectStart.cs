using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectStart : MonoBehaviour
{
    public void startEffect(){
        GetComponent<ParticleSystem>().Play();
    }
}
