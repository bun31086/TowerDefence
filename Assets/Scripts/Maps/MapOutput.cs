// ---------------------------------------------------------  
// MapOutput.cs  
// マップ配列にTileMapの状態を出力する
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapOutput : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("マップデータを保持している配列")]
    private MapData _mapData = default;

    [SerializeField, Tooltip("マップ出力をしたいタイルマップ")]
    private Tilemap _tileMap = default;
    [SerializeField, Tooltip("使用するタイルを格納")]
    private List<Tile> _tileType = new List<Tile>();

    [Tooltip("タイルマップの横サイズ")]
    private int _horizontal = default;
    [Tooltip("タイルマップの縦サイズ")]
    private int _vartical = default;
    [Tooltip("タイルマップのX座標で一番小さい値")]
    private int _horizontalMin = default;
    [Tooltip("タイルマップのY座標で一番大きい値")]
    private int _varticalMax = default;
    [Tooltip("配列が0から始まるため、引く数字")]
    private const int CONST_MINAS_ONE = -1;

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
  
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
     }


    /// <summary>
    /// ボタンが押されたら実行する処理
    /// </summary>
    public void Output() {
        BoundsInt bounds = _tileMap.cellBounds;
        //タイルマップの一番左と一番下の座標を格納
        _horizontalMin = bounds.min.x;
        _varticalMax = bounds.max.y;
        //タイルマップのサイズを調べる
        _horizontal = bounds.max.x - _horizontalMin;
        _vartical = _varticalMax - bounds.min.y;
        //タイルマップの情報のサイズに配列を変更
        _mapData.ArraySizeChange(_horizontal, _vartical);

        //配列を変更
        foreach (Vector3Int pos in _tileMap.cellBounds.allPositionsWithin) {
            // タイルが無かったら
            if (!_tileMap.HasTile(pos)) {
                //処理を中断
                return;
            }
            //タイルの種類分繰り返す
            int index = default;
            foreach (Tile tile in _tileType) {
                // スプライトが一致しているか判定
                if (_tileMap.GetTile(pos) == tile) {
                    // 特定のスプライトと一致している場合は配列のそのタイルに対応した数字を格納
                    _mapData.MapDataArray[_varticalMax - pos.y + CONST_MINAS_ONE, pos.x - _horizontalMin] = index;
                    break;
                }
                index++;
            }

        }
        //配列を表示
        // 配列を出力するテスト
        print("Field------------------------------------------");
        for (int y = 0; y < _vartical; y++) {
            string outPutString = "";
            for (int x = 0; x < _horizontal; x++) {
                outPutString += _mapData.MapDataArray[y, x];
            }
            print(outPutString);
        }
        print("Field------------------------------------------");
    }
    #endregion
}
