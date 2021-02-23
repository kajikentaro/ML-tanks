using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class StageMaker : MonoBehaviour
{
    static public bool canMove = false;
    GameObject[] block_objs;
    float block_height = 1.0f;
    float block_width = 1.6f;
    float block_depth = 1.0f;
    public int stage_number = 1;

    int remain_tanks = 0;
    void LoadResourse(int n)//プレハブ化してあるn個のブロックリソースをとってくるだけ。
    {
        block_objs = new GameObject[n];
        for(int i = 0; i < n; i++)
        {
            block_objs[i] = (GameObject)Resources.Load("Block" + (char)(i+'1'));
        }
    }
    char[,] LoadStage(int stage_number)//csvからテキスト情報を読み込み、int2次元配列を返す
    {
        string map_file_path = Application.dataPath + "/Stage/StageData/stage" + stage_number + ".csv";
        char[,] blocks;
        using (var fs = new StreamReader(map_file_path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string[] param = fs.ReadLine().Split(' ');
            int h = int.Parse(param[0]);
            int w = int.Parse(param[1]);
            blocks = new char[h,w];
            //while (fs.Peek() != -1)
            for (int i = 0; i < h; i++)
            { 
                string line = fs.ReadLine();
                for (int j = 0; j < w; j++)
                {
                    blocks[i, j] = line[j];
                }
            }
        }
        return blocks;
    }
    public GameObject tankPrefab;
    void drawBlock(char[,] blocks)//取得した配列を元にブロックをステージに描写するメソッド
    {
        LoadResourse(2);//現在作成済みのブロックの数は2! 要変更
        int h = blocks.GetLength(0);
        int w = blocks.GetLength(1);
        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < w; j++)
            {
                if (blocks[i,j] == '0') continue;
                if(blocks[i,j] == '1'){
                    Vector3 block_position = new Vector3(block_width * (j + 0.5f), -block_depth * 4.0f, block_height * (h - i - 1 + 0.5f));
                    Instantiate(block_objs[blocks[i,j] - '1'], block_position, Quaternion.identity);
                    
                }
                if(blocks[i,j] == 'a'){
                    // タンクのプレハブを実体化（インスタンス化）する。
                    Vector3 tank_position = new Vector3(block_width * (j + 0.5f), block_depth / 2.0f, block_height * (h - i - 1 + 0.5f));
                    Instantiate(tankPrefab, tank_position , Quaternion.identity);
                    remain_tanks++;
                }
            }
        }
    }
    public Text startGameCounter;
    public GameObject Panel;
    public void dropout_tank()
    {
        remain_tanks--;
        if(remain_tanks == 1)
        {
            startGameCounter.text = "Finish";
            Panel.SetActive(true);
            Invoke("nextStage", 3);
        }
    }
    private void pass_value_toDetailScene(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var stageMaker = GameObject.Find("ScriptHolder").GetComponent<DetailSceneScript>();
        stageMaker.pre_stage_number = stage_number;
        SceneManager.sceneLoaded -= pass_value_toDetailScene;
    }
    void nextStage()
    {
        SceneManager.sceneLoaded += pass_value_toDetailScene;
        SceneManager.LoadScene("DetailScene");
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
            Panel.SetActive(false);
            canMove = true;
        }
    }
    bool pausing= false;
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
    void Start()
    {
        char[,] blocks = LoadStage(stage_number);
        drawBlock(blocks);
        countDown(3);
    }
}
