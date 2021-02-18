﻿using System.Collections;
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
        string map_file_path = Application.dataPath + "/Stage/StageData/stage1.csv";
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
                    Vector3 block_position = new Vector3(block_width * (j + 0.5f), block_depth / 2.0f, block_height * (h - i - 1 + 0.5f));
                    Instantiate(block_objs[blocks[i,j] - '1'], block_position, Quaternion.identity);
                    
                }
                if(blocks[i,j] == 'a'){
                    // タンクのプレハブを実体化（インスタンス化）する。
                    Vector3 tank_position = new Vector3(block_width * (j + 0.5f), block_depth / 2.0f, block_height * (h - i - 1 + 0.5f));
                    Instantiate(tankPrefab, tank_position , Quaternion.identity);
                }
            }
        }
    }
    public Text startGameCounter;
    public GameObject Panel;
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
        }
    }
    void finish()
    {
        
    }
    int stage_number;
    void newGame(int stage_number)
    {
        this.stage_number = stage_number;
        char[,] blocks = LoadStage(stage_number);
        drawBlock(blocks);
        countDown(3);

        
    }
    void Start()
    {
        newGame(1);//第一ステージを始めるよ。
    }
    void Update()
    {
        
    }
}
