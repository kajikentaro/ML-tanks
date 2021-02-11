using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject explosionPrefubs;
    public void attackBlock()
    {
        explosionPrefubs.SetActive(true);
        this.gameObject.SetActive(false);
        //エフェクトの処理

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            attackBlock();
        }

    }
}
