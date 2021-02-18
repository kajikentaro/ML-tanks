using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectStart : MonoBehaviour
{

    public AudioClip sound1;
    AudioSource audioSource;
    void Start(){
        audioSource=GetComponent<AudioSource>();
    }
    public void startEffect(){
        audioSource=GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        GetComponent<ParticleSystem>().Play();
    }
}
