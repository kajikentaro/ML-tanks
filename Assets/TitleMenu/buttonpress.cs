using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonpress : MonoBehaviour
{
    AsyncOperation async;
    void Start()
    {
        async = SceneManager.LoadSceneAsync("MainStage");
        async.allowSceneActivation = false;

    }
    public void gameStart()
    {
        async.allowSceneActivation = true;
    }
}
