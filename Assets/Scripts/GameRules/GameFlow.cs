// ---------------------------------------------------------  
// GameFlow.cs  
// ゲームの流れ管理
// 作成日:  3/20
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameFlow : MonoBehaviour
{

    #region 変数  

    [Tooltip("WAVE数")]
    private int _waveCount = default;
    [Tooltip("召喚している敵リスト")]
    private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField, Tooltip("Waveを格納"), Header("Waveを１から順番にアタッチ")]
    private WaveData[] _waveData = default;
    [SerializeField, Tooltip("敵を生成時に親にするゲームオブジェクト"), Header("敵をまとめるフォルダー")]
    private GameObject _enemyFolder = default;
    [Tooltip("敵を生成する座標")]
    private Vector2 _enemyPos = default;
    [Tooltip("1Waveの中で生成された敵リスト")]
    private List<GameObject> _nowWaveEnemy =default;
    [Tooltip("敵生成インターバル")]
    private int _createInterval = 1;
    [Tooltip("配列を0スタートするために使用")]
    private int _offset = default;
    [Tooltip("待ち時間")]
    private WaitForSeconds _spawnWait = default;
    [Tooltip("コルーチンを一度のみ再生")]
    private bool _isCoroutine = false;
    [Tooltip("WAVEの敵をすべて生成したらTrue")]
    private bool _isCreated = false;
    [SerializeField,Tooltip("プレイヤーステータス")]
    private PlayerStatus _playerStatus = default;
    [SerializeField, Tooltip("ランダム生成に使う敵の種類"),Header("敵の種類一覧")]
    private GameObject[] _enemyTypes = default;

    /// <summary>
    /// ゲームの状態
    /// </summary>
    private enum GameState {
        Start,          //ゲーム開始
        Preparation,    //準備
        Buttle,         //戦闘
        Result,         //リザルト
    }

    private GameState _gameState = default;

    #endregion
  
    #region プロパティ  
  
    #endregion
  
    #region メソッド  
  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Awake()
     {
     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        /* シーン移動したらスタート
         * スタート時は準備時間無制限にする
         * その間にチュートリアルする
         * スタートボタンを押したらWAVE開始。
         * WAVEの初めにそのWAVEの敵を一斉に生成する
         * 生成された敵をリストに入れて、すべてなくなったら準備時間へ（制限時間あり）
         * 制限時間が終わったらつぎのWAVE
         * WAVEの敵はスクリプタブルで選べる
         * えらばれてないWAVEはランダムで生成する
         */
        _gameState = GameState.Start;
        //WAVE数初期化
        _waveCount = 0;
        //生成場所を設定
        _enemyPos = new Vector2(100, 100);
        //インターバルを設定
        _spawnWait = new WaitForSeconds(_createInterval);
        //オフセット設定
        _offset = -1;
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
        switch (_gameState) {
            case GameState.Start:
                //チュートリアル画面を表示

                //スタートボタンを押すとPreparationへ

                _gameState = GameState.Preparation;

                break;
            case GameState.Preparation:
                //WAVE数を１追加
                _waveCount++;
                //リスト初期化
                _nowWaveEnemy = new List<GameObject>();
                //もし用意されたウェーブデータよりウェーブ数が多くなったら
                if (_waveCount > _waveData.Length) {
                    //敵を自動ランダム生成
                    for (int x = 0; x < _waveCount * 2; x++) {
                        //ランダムで敵を選出
                        int number = Random.Range(0, _enemyTypes.Length);
                        //敵を生成
                        GameObject spawnedEnemy = Instantiate(_enemyTypes[number], _enemyPos, Quaternion.identity,_enemyFolder.transform);
                        //生成した敵をリストに入れる
                        _nowWaveEnemy.Add(spawnedEnemy);
                        //非アクティブ化
                        spawnedEnemy.SetActive(false);
                    }
                }
                //まだウェーブデータがあるなら
                else {
                    //そのウェーブの敵の数、繰り返す
                    foreach (GameObject enemy in _waveData[_waveCount + _offset].WaveEnemy) {
                        //敵を生成
                        GameObject spawnedEnemy = Instantiate(enemy, _enemyPos, Quaternion.identity, _enemyFolder.transform);
                        //生成した敵をリストに入れる
                        _nowWaveEnemy.Add(spawnedEnemy);
                        //非アクティブ化
                        spawnedEnemy.SetActive(false);
                    }
                }
                //フラグ変更
                _isCoroutine = false;
                _isCreated = false;
                //制限時間すぎたらButtleへ
                _gameState = GameState.Buttle;
                break;
            case GameState.Buttle:
                //はじめのみ
                if (!_isCoroutine) {
                    //敵を生成する
                    StartCoroutine(nameof(EnemySpawn));
                    //フラグ変更
                    _isCoroutine = true;
                }
                //敵が全て生成されていないとき
                if (!_isCreated) {
                    //処理を中断
                    return;
                }
                //現在のWAVEの敵の数、繰り返す
                foreach (GameObject enemy in _nowWaveEnemy) {
                    //アクティブの敵がいれば
                    if (enemy.activeSelf) {
                        //処理を中断
                        return;
                    }
                }
                //もしプレイヤーのHPが０になったら
                if (_playerStatus.PlayerHP == 0) {
                    //Resultへ
                    _gameState = GameState.Result;
                    //処理を中断
                    return;
                }
                //全ての敵が倒されたときPreparationへ
                _gameState = GameState.Preparation;
                break;
            case GameState.Result:
                Debug.LogError("リザルト");
                break;
        }
     }

    /// <summary>
    /// 敵を生成
    /// </summary>
    private IEnumerator EnemySpawn() {
        //そのウェーブの敵の数、繰り返す
        foreach (GameObject enemy in _nowWaveEnemy) {
            //敵を生成
            enemy.SetActive(true);
            //クールタイム待つ
            yield return _spawnWait;
        }
        //全て生成しおわったらフラグを変更
        _isCreated = true;
        //全ての敵を生成後、コルーチン停止
        StopCoroutine(nameof(EnemySpawn));
    }

    #endregion
}
