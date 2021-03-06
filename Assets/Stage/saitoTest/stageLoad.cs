﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Unity.MLAgents.Policies;
using Unity.Barracuda;


public class stageLoad : MonoBehaviour
{
    public bool learningMode = true;
    GameObject BreakableBlock;
    GameObject unBreakableBlock;
    GameObject tankMe;
    public GameObject tankMe_instance;
    public NNModel tankModel;
    float block_height = 1.0f;
    float block_width = 1.0f;
    float block_depth = -2.5f;
    float tank_depth = 0.2f;
    public int stage_number = 1;
    public int enemy_num = 0;

    public IEnumerator LoadStage(int stage_number, bool learningMode)//csvからテキスト情報を読み込み、int2次元配列を返す
    {
        TextAsset textAsset = Resources.Load("StageData/stage" + stage_number) as TextAsset;
        string[] fss = textAsset.text.Split('\n');

        string[] param = fss[0].Split(' ');
        int h = int.Parse(param[0]);
        int w = int.Parse(param[1]);
        char[,] blocks = new char[h, w];

        for(int k = 1; k < fss.Length; k++)
        {
            int i = h - k;
            for (int j = 0; j < w; j++)
            {
                blocks[i, j] = fss[k][j];
            }
        }
        BreakableBlock = Resources.Load("stageObject/Block1") as GameObject;
        unBreakableBlock = Resources.Load("stageObject/Block2") as GameObject;
        GameObject Blocks = new GameObject("Blocks");
        GameObject Shells = new GameObject("Shells");
        tankMe = Resources.Load("stageObject/tankMe") as GameObject;
        tankMe_instance = Instantiate(tankMe, new Vector3(0, -100, 0), Quaternion.identity);
        MLTank.target = tankMe_instance;
        //頂点の個数と、必要なメッシュの個数を数える
        int vcnt = 0;
        int tcnt = 0;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (blocks[i, j] == '2')
                {
                    int f = 0;
                    if (i > 0 && blocks[i - 1, j] == '2') f = 1;
                    if (j > 0 && blocks[i, j - 1] == '2') f = 1;
                    if (i == 0 || j == 0) f = 1;
                    if (i > 0 && j > 0 && blocks[i - 1, j - 1] == '2') f = 1;
                    if (f == 0) vcnt++;
                    f = 0;
                    if (i > 0 && blocks[i - 1, j] == '2') f = 1;
                    if (i > 0 && j + 1 < w && blocks[i - 1, j + 1] == '2') f = 1;
                    if (i == 0 || j == w - 1) f = 1;
                    if (f == 0) vcnt++;
                    f = 0;
                    if (j > 0 && blocks[i, j - 1] == '2') f = 1;
                    if (j == 0) f = 1;
                    if (f == 0) vcnt++;
                    vcnt++;

                    if (i > 0 && blocks[i - 1, j] != '2') tcnt++;
                    if (j > 0 && blocks[i, j - 1] != '2') tcnt++;
                    if (i + 1 < h && blocks[i + 1, j] != '2') tcnt++;
                    if (j + 1 < w && blocks[i, j + 1] != '2') tcnt++;

                }
            }
        }
        //頂点とメッシュを作る
        Vector3[] vertices = new Vector3[vcnt * 2];
        int[] triangles = new int[tcnt * 2 * 3];
        int[,,] idx = new int[h, w, 4];
        /*             ^上から見てZ順に0,1,2,3
        */
        int now = 0;
        int cur = 0;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (blocks[i, j] == '2')
                {

                    int f = 0;
                    float x = block_width * (j - w / 2) + transform.position.x;
                    float z = block_height * (i - h / 2) + transform.position.z;

                    if (i > 0 && blocks[i - 1, j] == '2') f = 1;
                    if (j > 0 && blocks[i, j - 1] == '2') f = 1;
                    if (i == 0 || j == 0) f = 1;
                    if (i > 0 && j > 0 && blocks[i - 1, j - 1] == '2') f = 1;
                    if (f == 0)
                    {
                        idx[i, j, 0] = now;
                        vertices[now] = new Vector3(x, 5.0f, z);
                        vertices[now + vcnt] = new Vector3(x, -5.0f, z);
                        now++;
                    }
                    f = 0;
                    if (i > 0 && blocks[i - 1, j] == '2') f = 1;
                    if (i > 0 && j + 1 < w && blocks[i - 1, j + 1] == '2') f = 1;
                    if (i == 0 || j == w - 1) f = 1;
                    if (f == 0)
                    {
                        idx[i, j, 1] = now;
                        vertices[now] = new Vector3(x + block_width, 5.0f, z);
                        vertices[now + vcnt] = new Vector3(x + block_width, -5.0f, z);
                        if (j + 1 < w && blocks[i, j + 1] == '2')
                        {
                            idx[i, j + 1, 0] = now;
                        }
                        now++;
                    }
                    f = 0;
                    if (j > 0 && blocks[i, j - 1] == '2') f = 1;
                    if (j == 0) f = 1;
                    if (f == 0)
                    {
                        idx[i, j, 2] = now;
                        vertices[now] = new Vector3(x, 5.0f, z + block_height);
                        vertices[now + vcnt] = new Vector3(x, -5.0f, z + block_height);
                        if (i + 1 < h && j > 0 && blocks[i + 1, j - 1] == '2')
                        {
                            idx[i + 1, j - 1, 1] = now;
                        }
                        if (i + 1 < h && blocks[i + 1, j] == '2')
                        {
                            idx[i + 1, j, 0] = now;
                        }
                        now++;
                    }
                    idx[i, j, 3] = now;
                    vertices[now] = new Vector3(x + block_width, 5.0f, z + block_height);
                    vertices[now + vcnt] = new Vector3(x + block_width, -5.0f, z + block_height);
                    if (j + 1 < w && blocks[i, j + 1] == '2')
                    {
                        idx[i, j + 1, 2] = now;
                    }
                    if (i + 1 < h && blocks[i + 1, j] == '2')
                    {
                        idx[i + 1, j, 1] = now;
                    }
                    if (i + 1 < h && j + 1 < w && blocks[i + 1, j + 1] == '2')
                    {
                        idx[i + 1, j + 1, 0] = now;
                    }
                    now++;

                    if (i > 0 && blocks[i - 1, j] != '2')
                    {
                        triangles[cur] = idx[i, j, 0];
                        cur++;
                        triangles[cur] = idx[i, j, 1] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 0] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 0];
                        cur++;
                        triangles[cur] = idx[i, j, 1];
                        cur++;
                        triangles[cur] = idx[i, j, 1] + vcnt;
                        cur++;
                    }
                    if (j > 0 && blocks[i, j - 1] != '2')
                    {
                        triangles[cur] = idx[i, j, 2];
                        cur++;
                        triangles[cur] = idx[i, j, 0] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 2] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 2];
                        cur++;
                        triangles[cur] = idx[i, j, 0];
                        cur++;
                        triangles[cur] = idx[i, j, 0] + vcnt;
                        cur++;
                    }
                    if (i + 1 < h && blocks[i + 1, j] != '2')
                    {
                        triangles[cur] = idx[i, j, 3];
                        cur++;
                        triangles[cur] = idx[i, j, 2] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 3] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 3];
                        cur++;
                        triangles[cur] = idx[i, j, 2];
                        cur++;
                        triangles[cur] = idx[i, j, 2] + vcnt;
                        cur++;
                    }
                    if (j + 1 < w && blocks[i, j + 1] != '2')
                    {
                        triangles[cur] = idx[i, j, 1];
                        cur++;
                        triangles[cur] = idx[i, j, 3] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 1] + vcnt;
                        cur++;
                        triangles[cur] = idx[i, j, 1];
                        cur++;
                        triangles[cur] = idx[i, j, 3];
                        cur++;
                        triangles[cur] = idx[i, j, 3] + vcnt;
                        cur++;
                    }
                }
            }
        }
        //メッシュの生成
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        //オブジェクトの生成
        GameObject UnbreakableBlocksCollider = new GameObject("UnbreakableBlocksCollider");
        UnbreakableBlocksCollider.tag = "block";
        UnbreakableBlocksCollider.AddComponent<MeshCollider>();
        var meshcollider = UnbreakableBlocksCollider.GetComponent<MeshCollider>();
        meshcollider.sharedMesh = mesh;
        Instantiate(UnbreakableBlocksCollider, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                yield return 0;
                float x = block_width * (j - w / 2 + 0.5f) + transform.position.x;
                float z = block_height * (i - h / 2 + 0.5f) + transform.position.z;
                if (blocks[i, j] == '0') continue;
                else if (blocks[i, j] == '1')
                {
                    Vector3 block_position = new Vector3(x, block_depth, z);
                    Instantiate(BreakableBlock, block_position, Quaternion.identity, Blocks.transform);
                }
                else if (blocks[i, j] == '2')
                {
                    Vector3 block_position = new Vector3(x, block_depth, z);
                    Instantiate(unBreakableBlock, block_position, Quaternion.identity, Blocks.transform);
                }
                else if (blocks[i, j] == '.')
                {
                    if (!learningMode)
                    {
                        Vector3 tank_position = new Vector3(x, tank_depth, z);
                        tankMe_instance.transform.position = tank_position;
                        tankMe_instance.GetComponent<RootTank>().Shells = Shells;
                        tankMe_instance.GetComponent<TankMe>().learningMode = learningMode;
                        tankMe_instance.GetComponent<RootTank>().script_holder = this.gameObject;
                    }
                }
                else
                {
                    GameObject EnemyTank;
                    if (!learningMode)
                    {
                        enemy_num++;
                        EnemyTank = Resources.Load("stageObject/tank" + blocks[i, j]) as GameObject;
                        Vector3 tank_position = new Vector3(x, tank_depth, z);
                        var tank_gameobject = Instantiate(EnemyTank, tank_position, Quaternion.identity);
                        tank_gameobject.GetComponent<RootTank>().Shells = Shells;
                        tank_gameobject.GetComponent<BehaviorParameters>().Model = tankModel;
                        tank_gameobject.GetComponent<RootTank>().script_holder = this.gameObject;
                        tank_gameobject.SetActive(true);//これをしないとOnEpisodeBeginがtank_gameobject.GetComponent<RootTank>().Shells=Shells;より先に発生してヌルポになる。
                    }
                }
            }
        }
    }
    void Start()
    {
        //tankMeを先にインスタンス化
        LoadStage(stage_number, learningMode);
    }
}