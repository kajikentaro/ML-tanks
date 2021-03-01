using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotShell : MonoBehaviour
{
    public GameObject shellPrefab;
    //public float shotSpeed;
    public int maxColCount;
    public int maxShellNum;
    public int shellNum;

    //public AudioClip shotSound;
    // Update is called once per frame
    public void shotShell()
    {
        if(shellNum<maxShellNum){
            GameObject shell = Instantiate(shellPrefab, transform.position,transform.rotation );
            shellNum+=1;
            // 砲弾に付いているRigidbodyコンポーネントにアクセスする。
            shell.GetComponent<shellScript>().maxCol=maxColCount;
            shell.GetComponent<shellScript>().shotshell_gameobject=this.gameObject;
            //Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            //shellRb.velocity= transform.forward * shotSpeed*10;
            //AudioSource.PlayClipAtPoint(shotSound, transform.position);
        }
    }
    void Update()
    {
        // もしもSpaceキーを押したならば（条件）
        //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && StageMaker.canMove )
        //{
            ////shotShell();
        //}
    }
}