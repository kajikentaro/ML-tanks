using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotShell : MonoBehaviour
{
    public GameObject shellPrefab;
    public float shotSpeed;

    //public AudioClip shotSound;
    // Update is called once per frame
    public void shotShell()
    {
        // 砲弾のプレハブを実体化（インスタンス化）する。
        GameObject shell = Instantiate(shellPrefab, transform.position,transform.rotation );
        // 砲弾に付いているRigidbodyコンポーネントにアクセスする。
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        // 前に飛ばす
        // Vector3 backward = new Vector3(0,0,-1);
        shellRb.velocity= transform.forward * shotSpeed;
        // 発射した砲弾を３秒後に破壊する。
        // （重要な考え方）不要になった砲弾はメモリー上から削除すること。
        //Destroy(shell, 3.0f);
        // 砲弾の発射音を出す。
        //AudioSource.PlayClipAtPoint(shotSound, transform.position);
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