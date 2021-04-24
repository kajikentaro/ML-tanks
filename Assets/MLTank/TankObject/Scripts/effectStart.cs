using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectStart : MonoBehaviour
{

    public AudioClip sound1;
    AudioSource audioSource;
    public GameObject explodePrefab;
    void Start(){
        audioSource=GetComponent<AudioSource>();
    }
    public void startEffect(Vector3 pos){
        GameObject effect = Instantiate(explodePrefab, pos,transform.rotation ,this.transform);
        //audioSource=GetComponent<AudioSource>();
        //audioSource.PlayOneShot(sound1);
        effect.GetComponent<ParticleSystem>().Play();
    }
}
