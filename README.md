# ML-tanks
Wi○のパクリ戦車ゲーム。ML-Agentsの強化学習ライブラリを利用し、敵キャラを生成。

# Unityでのゲーム実装
- 戦車、ステージ、弾丸のモデリング
- 弾丸跳ね返り、戦車の移動の設定
- エフェクトの設定（この時点では仮設定のため低クオリティ）  
![explode](https://user-images.githubusercontent.com/62131533/149570689-0a0b9e10-9685-4327-b697-48ef788e441c.gif)

# MLagentsでの強化学習（動かない戦車）
- 弾丸当てればプラス報酬、指定時間経過でマイナス報酬
- 砲台からはrayと呼ばれるセンサーが前方に放射されており、その範囲に対象が入れば微小報酬を与える  
![ss](https://user-images.githubusercontent.com/62131533/149570844-c4cb7b16-f535-4d21-a0af-e4f4ee90224d.gif)

# MLagentsでの強化学習（動く戦車）
- 動く戦車は弾丸を避けながら対象を撃破するように学習させる
- 以下は弾を避けながら対象を撃破するAgent
- 実際はここからある程度距離を保ちながら撃破する必要がある。  
![avoidShell](https://user-images.githubusercontent.com/62131533/149571986-bb18aa1a-cea4-4740-ab76-7fd6a6daa4b0.gif)
