#include <random>
#include <vector>
#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
using namespace std;

vector<vector<char> >stage_generator(int H, int W, int block_num, int enemy_num){

    vector<vector<char> >stage(H,vector<char>(W, '0'));

    for(int i = 0; i < H; i++){
        stage[i][0] = '2';
        stage[i][W-1] = '2';
    }
    for(int i = 0; i < W; i++){
        stage[0][i]='2';
        stage[H-1][i]='2';
    }

    int cnt = 0;
    while(cnt < block_num){
        int idx = rand()%((H-1)*(W-1));
        int x = idx / (W-1) + 1;
        int y = idx % (W-1) + 1;

        if(stage[x][y] == '0'){
            stage[x][y] = '2';
            cnt++;
        }
    }
    cnt = 0;
    while(cnt < enemy_num){
        int idx = rand()%((H-1)*(W-1));
        int x = idx / (W-1) + 1;
        int y = idx % (W-1) + 1;

        if(stage[x][y] == '0'){
            stage[x][y] ='A';
            cnt++;
        }
    }
    cnt = 0;
    while(cnt < 1){
        int idx = rand()%((H-1)*(W-1));
        int x = idx / (W-1) + 1;
        int y = idx % (W-1) + 1;

        if(stage[x][y] == '0'){
            stage[x][y] ='.';
            cnt++;
        }
    }
    return stage;
}

int main(){
    int H = 22, W = 32;

    string input_file = "stage_param.txt";
    string output_file = "stage_info.csv";
    
    ifstream ifs(input_file);
    ofstream ofs(output_file);
    
    int block_num, enemy_num;
    ifs >> block_num >> enemy_num;
    vector<vector<char> > stage = stage_generator(H, W, block_num, enemy_num);
    for(int i = 0; i < H; i++){
        for(int j = 0; j < W; j++){
            ofs << stage[i][j];
        }
        ofs << endl;
    }
}