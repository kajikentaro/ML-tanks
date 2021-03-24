using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class stageLoad : MonoBehaviour
{
    static public bool canMove = false;
    GameObject BreakableBlock;
    GameObject unBreakableBlock;
    GameObject tankMe;
    GameObject[] EnemyTank;
    float block_height = 1.0f;
    float block_width = 1.0f;
    float block_depth = 1.0f;
    public int stage_number = 1;

    char[,] LoadStage(int stage_number)//csvからテキスト情報を読み込み、int2次元配列を返す
    {
        string map_file_path = Application.dataPath + "/Stage/StageData/test" + stage_number + ".csv";
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
    void drawBlock(char[,] blocks)//取得した配列を元にブロックをステージに描写するメソッド
    {
        int h = blocks.GetLength(0);
        int w = blocks.GetLength(1);
        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < w; j++)
            {
                float x=block_height*(i-h/2+0.5f);
                float z=block_width*(j-w/2+0.5f);
                if (blocks[i,j] == '0') continue;
                if(blocks[i,j] == '1'){
                    Vector3 block_position = new Vector3(x, -block_depth * 4.0f, z);
                    Instantiate(BreakableBlock, block_position, Quaternion.identity);
                }
                if(blocks[i,j] == '2'){
                    Vector3 block_position = new Vector3(x, -block_depth * 4.0f,z);
                    Instantiate(unBreakableBlock, block_position, Quaternion.identity);
                    
                }
                if(blocks[i,j] == 'A'){
                    Vector3 tank_position = new Vector3(x, block_depth / 2.0f,z);
                    Instantiate(tankMe, tank_position , Quaternion.identity);
                }
                if(blocks[i,j] == 'a'){
                    Vector3 tank_position = new Vector3(x, block_depth / 2.0f, z);
                    Instantiate(EnemyTank[0], tank_position , Quaternion.identity);
                }
            }
        }
    }
    void Start(){
    BreakableBlock=Resources.Load("stageObject/Block1") as GameObject;
    unBreakableBlock=Resources.Load("stageObject/Block2") as GameObject;
    tankMe=Resources.Load("stageObject/tankMetest") as GameObject;
    EnemyTank= new GameObject[1];
    EnemyTank[0]=Resources.Load("stageObject/tankAtest") as GameObject;
    char[,] blocks = LoadStage(1);
    drawBlock(blocks);
    }
}