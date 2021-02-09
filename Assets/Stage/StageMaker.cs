using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageMaker : MonoBehaviour
{
    GameObject[] block_objs;
    float block_height = 1.0f;
    float block_width = 1.6f;
    float block_depth = 1.0f;
    void roadResourse(int n)//プレハブ化してあるn個のブロックリソースをとってくるだけ。
    {
        block_objs = new GameObject[n];
        for(int i = 0; i < n; i++)
        {
            block_objs[i] = (GameObject)Resources.Load("Block" + (i+1));
        }
    }
    int[,] roadStage(int stage_number)//csvからテキスト情報を読み込み、int2次元配列を返す
    {
        string map_file_path = Application.dataPath + "/Stage/StageData/stage1.csv";
        int[,] blocks;
        using (var fs = new StreamReader(map_file_path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            string[] param = fs.ReadLine().Split(' ');
            int h = int.Parse(param[0]);
            int w = int.Parse(param[1]);
            blocks = new int[h, w];
            //while (fs.Peek() != -1)
            for (int i = 0; i < h; i++)
            {
                string line = fs.ReadLine();
                for (int j = 0; j < w; j++)
                {
                    blocks[i, j] = line[j] - '0';
                }
            }
        }
        return blocks;
    }
    void drawBlock(int[,] blocks)//取得した配列を元にブロックをステージに描写するメソッド
    {
        roadResourse(2);//現在作成済みのブロックの数は2! 要変更
        int h = blocks.GetLength(0);
        int w = blocks.GetLength(1);
        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < w; j++)
            {
                if (blocks[i,j] == 0) continue;
                Vector3 block_position = new Vector3(block_width * (j + 0.5f), block_depth / 2.0f, block_height * (h - i - 1 + 0.5f));
                Instantiate(block_objs[blocks[i,j] - 1], block_position, Quaternion.identity);
            }
        }
    }
    void newGame(int stage_number)
    {
        int[,] blocks = roadStage(stage_number);
        drawBlock(blocks);
        //ステージが完成する。
        //
        //なんかの処理
    }
    void Start()//  ？「全てはここからはじまる」
    {
        newGame(1);//第一ステージを始めるよ。
    }
    void Update()
    {
        
    }
}
