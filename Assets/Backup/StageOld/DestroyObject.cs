using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // このメソッドはぶつかった瞬間に呼び出される
    private void OnCollisionEnter(Collision other)
    {
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば（条件）
        if (other.gameObject.tag == "Shell")
        {
            // このスクリプトがついているオブジェクトを破壊する（thisは省略が可能）
            Destroy(this.gameObject);

            // ぶつかってきたオブジェクトを破壊する
            Destroy(other.gameObject);
        }
    }
}