using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DetailSceneScript : MonoBehaviour
{
    static public bool loaded_prehub = false;
    public int pre_stage_number = 1;//inspectorで変更できてしまうので注意。
    void Start()
    {
        //SceneManager.sceneLoaded += pass_value_toStage;
        StartCoroutine("test");
    }
    IEnumerator test()
    {
        //StartCoroutine("LoadScene");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainStage");
        Debug.Log("0 " + asyncLoad.progress);
        asyncLoad.allowSceneActivation = false;
        Debug.Log("1 " + asyncLoad.progress);
        while (true)
        {
            if(asyncLoad.progress >= 0.90f && loaded_prehub)
            {
                Debug.Log("3 " + asyncLoad.progress);
                asyncLoad.allowSceneActivation = true;
                break;
            }
            else
            {
                Debug.Log("2 " + asyncLoad.progress);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    //非同期にステージをロードしておく。早く終わった場合でも三秒間は画面推移をしない。
    IEnumerator loadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainStage");
        asyncLoad.allowSceneActivation = false;
        while (true)
        {
            yield return null;
            // 読み込み完了したら
            if (asyncLoad.progress >= 0.9f)
            {
                // シーン読み込み
                asyncLoad.allowSceneActivation = true;
                break;
            }
        }
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
