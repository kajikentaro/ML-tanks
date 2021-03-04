using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotShell : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject parentObj;
    //public float shotSpeed;
    public int maxColCount;
    public int maxShellNum;
    public int shellNum;

    //public AudioClip shotSound;
    // Update is called once per frame
    public void shotShell()
    {
        shellNum+=1;
        GameObject shell = Instantiate(shellPrefab, transform.position,transform.rotation ,parentObj.transform);
        // 砲弾に付いているRigidbodyコンポーネントにアクセスする。
        shell.GetComponent<shellScript>().maxCol=maxColCount;
        shell.GetComponent<shellScript>().shotshell_gameobject=this.gameObject;
        //Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        //shellRb.velocity= transform.forward * shotSpeed*10;
        //AudioSource.PlayClipAtPoint(shotSound, transform.position);
    }
}