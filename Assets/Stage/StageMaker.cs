using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class StageMaker : MonoBehaviour
{
    GameObject[] block_objs;
    float block_height = 1.0f;
    float block_width = 1.6f;
    float block_depth = 1.0f;
    private int stage_number = 1;
    private int next_stage_number;

    public Text loading_message;
    public GameObject tankPrefab;
    public Text startGameCounter;
    public GameObject Panel;
    private void pass_value_to_MainStage(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var stageMaker = GameObject.Find("ScriptHolder").GetComponent<StageMaker>();
        stageMaker.stage_number = next_stage_number;
        SceneManager.sceneLoaded -= pass_value_to_MainStage;
    }
    void restartStage()
    {
        next_stage_number = stage_number;
        SceneManager.sceneLoaded += pass_value_to_MainStage;
        SceneManager.LoadScene("MainStage");
    }
    void nextStage()
    {
        next_stage_number = stage_number + 1;
        SceneManager.sceneLoaded += pass_value_to_MainStage;
        SceneManager.LoadScene("MainStage");
    }
    async void countDown(int n)
    {
        string[] showText = { "Start!", "1", "2", "3" };
        startGameCounter.text = showText[n];
        if (n != 0)
        {
            await Task.Delay(1000);
            countDown(n - 1);
        }
        else
        {
            await Task.Delay(1000);
            bgm.Play();
            Panel.SetActive(false);
            RootTank.BanAction=false;
        }
    }
    bool pausing= false;
    public void dead_enemy()
    {
        stageLoad sl = GetComponent<stageLoad>();
        sl.enemy_num--;
        if(sl.enemy_num == 0)
        {
            startGameCounter.text = "Game Clear";
            Panel.SetActive(true);
            Invoke("nextStage", 3);
        }
    }
    public void dead_me()
    {
        startGameCounter.text = "Game Over";
        Panel.SetActive(true);
        Invoke("restartStage", 3);
    }
    void pause_game()
    {
        if(pausing == true)
        {
            //start
            Time.timeScale = 1;
            Panel.SetActive(false);
            pausing = false;
        }
        else
        {
            //pause
            Time.timeScale = 0;
            startGameCounter.text = "pause";
            Panel.SetActive(true);
            pausing = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown("p")) pause_game();
    }
    public AudioSource bgm;
    public AudioSource countdown_music;
    public GameObject loading_menu;
    public GameObject countdown_panel;
    void Start()
    {
        RootTank.BanAction=true;
        loading_message.text = stage_number + ""; 
        //char[,] blocks = LoadStage(stage_number);
        //drawBlock(blocks);
        StartCoroutine(load_stage_async());
        StartCoroutine(wait4sec());
    }
    /* wait3sec()とload_stage_async()の両方が終わった場合のみカウントダウンを開始する*/
    private int checkDone_counter = 0;
    void checkDoneTwoMethods()
    {
        checkDone_counter++;
        if(checkDone_counter == 2)
        {
            countdown_music.Play();
            countDown(3);
            loading_menu.SetActive(false);
            countdown_panel.SetActive(true);
        }
    }
    IEnumerator wait4sec()
    {
        yield return new WaitForSeconds(4);
        checkDoneTwoMethods();
    }
    IEnumerator load_stage_async()
    {
        stageLoad sl = GetComponent<stageLoad>();
        yield return StartCoroutine(sl.LoadStage(stage_number, false));
        checkDoneTwoMethods();
    }
}
