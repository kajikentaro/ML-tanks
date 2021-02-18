using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion_effect;
    public GameObject mine_object;
    public GameObject object_ditector;

    void Start()
    {
        //test method
        Invoke("explosion", 3);
    }

    void explosion()
    {
        Invoke("disappear", 1);
        Invoke("clear", 5);
        explosion_effect.SetActive(true);
        object_ditector.SetActive(true);
        mine_object.SetActive(false);
    }
    void disappear()
    {
        object_ditector.SetActive(false);
    }
    void clear()
    {
        Destroy(this.gameObject);
    }
}
