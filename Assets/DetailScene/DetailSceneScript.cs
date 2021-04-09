using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DetailSceneScript : MonoBehaviour
{
    public int pre_stage_number = 1;//inspectorで変更できてしまうので注意。
    void Start()
    {
        Invoke("next", 3);
    }
    void next()
    {
        SceneManager.sceneLoaded += pass_value_toStage;
        SceneManager.LoadScene("MainStage");
    }
    private void pass_value_toStage(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var stageMaker = GameObject.Find("ScriptHolder").GetComponent<StageMaker>();
        stageMaker.stage_number = pre_stage_number + 1;
        SceneManager.sceneLoaded -= pass_value_toStage;
    }

// Update is called once per frame
void Update()
    {
        
    }
}
