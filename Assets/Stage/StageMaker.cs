using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;

public class StageMaker : MonoBehaviour
{
    GameObject[] block_objs;
    float block_height = 1.0f;
    float block_width = 1.6f;
    float block_depth = 1.0f;
    private int stage_number = 1;
    private int next_stage_number;

    public TextMeshProUGUI life_text, enemy_text, loading_message;
    public GameObject tankPrefab;
    public Text startGameCounter;
    public GameObject Panel;
    static int user_life = 3;
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
        if(next_stage_number > stage_max)
        {
            SceneManager.LoadScene("Result");
        }
        else
        {
            SceneManager.sceneLoaded += pass_value_to_MainStage;
            SceneManager.LoadScene("MainStage");
        }
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
    public AudioSource gameover_audio;
    public AudioSource gameclear_audio;

    public void dead_enemy()
    {
        stageLoad sl = GetComponent<stageLoad>();
        sl.enemy_num--;
        if(sl.enemy_num == 0)
        {
            startGameCounter.text = "You Win";
            bgm.Stop();
            gameclear_audio.Play();
            Panel.SetActive(true);
            Invoke("nextStage", 3);
        }
    }
    public void dead_me()
    {
        user_life--;
        if(user_life == 0)
        {
            //ゲームオーバーTODO
        }
        startGameCounter.text = "You lost";
        bgm.Stop();
        Panel.SetActive(true);
        gameover_audio.Play();
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
    private static int[] diff_lists;
    private static int[] enemy_nums;
    private static int stage_max = -1;
    void Start()
    {
        if(stage_max == -1)
        {
            init_diffs();
        }
        RootTank.BanAction=true;
        string life_heart = "♥";
        for(int i = 1; i < user_life; i++)
        {
            life_heart += " ♥";
        }
        life_text.text = life_heart;
        loading_message.text = "Stage " + stage_number;
        enemy_text.text = "enemy ×" + enemy_nums[stage_number - 1];

        StartCoroutine(load_stage_async());
        StartCoroutine(wait4sec());
    }
    void init_diffs()
    {
        string difficulty_txt_path = Application.dataPath + "/Stage/StageData/difficulty.txt";
        using (var fs = new StreamReader(difficulty_txt_path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            stage_max = int.Parse(fs.ReadLine());
            diff_lists = new int[stage_max];
            enemy_nums = new int[stage_max];
            for (int i = 0; i < stage_max; i++)
            {
                string[] param = fs.ReadLine().Split(' ');
                enemy_nums[i] = int.Parse(param[0]);
                diff_lists[i] = int.Parse(param[1]);
            }
        }
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
