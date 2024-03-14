// ---------------------------------------------------------  
// NewMapOut.cs  
// タイルマップを配列に格納
// 作成日:  3/12
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapOutput : MonoBehaviour
{

    #region 変数  
    [SerializeField, Tooltip("マップ配列に格納したいタイルマップ")]
    private Tilemap _tileMap = default;
    [SerializeField, Tooltip("使用するタイルを格納")]
    private List<Tile> _tileType = new List<Tile>();
    [Tooltip("タイルマップの横サイズ")]
    private int _horizontal = default;
    [Tooltip("タイルマップの縦サイズ")]
    private int _vartical = default;
    [Tooltip("タイルマップのX座標で一番小さい値")]
    private int _horizontalMin = default;    
    [Tooltip("タイルマップのX座標で一番大きい値")]
    private int _varticalMax = default;
    [Tooltip("タイルマップのY座標で一番小さい値")]
    private int _varticalMin = default;
    [Tooltip("配列が0から始まるため、引く数字")]
    private const int CONST_MINAS_ONE = -1;
    [Tooltip("ルート探索スクリプト")]
    private MapRouteSearch _mapRouteSearch = default;
    [Tooltip("曲がり角の数")]
    private int _curveCount = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start ()
     {
        Output();
     }

    /// <summary>
    /// ボタンが押されたらタイルマップを配列に落としこむ
    /// </summary>
    private void Output() {
        //左下を原点にする
        BoundsInt bounds = _tileMap.cellBounds;
        //タイルマップの一番左と一番下の座標を格納
        _horizontalMin = bounds.min.x;
        _varticalMin = bounds.min.y;
        _varticalMax = bounds.max.y + CONST_MINAS_ONE;
        //0からどのくらい離れているか
        int xDistance = -_horizontalMin;
        int yDistance = -_varticalMin;
        //タイルマップのサイズを調べる
        _horizontal = xDistance + bounds.max.x;
        _vartical = yDistance + bounds.max.y;
        //タイルマップの情報のサイズの配列を生成
        MapData.Instance.MapDataArray = new int[_vartical, _horizontal];


        //タイルマップのすべてのタイルの枚数繰り返す
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
                    MapData.Instance.MapDataArray[bounds.max.y - 1 - pos.y, pos.x + xDistance] = index;
                    break;
                }
                index++;
            }
        }

        // 配列を出力するテスト
        print("Field------------------------------------------");
        for (int y = 0; y < _vartical; y++) {
            string outPutString = "";
            for (int x = 0; x < _horizontal; x++) {
                outPutString += MapData.Instance.MapDataArray[y, x];
            }
            print(outPutString);
        }
        print("Field------------------------------------------");

        //ルートを探索する(配列内)
        _mapRouteSearch = new MapRouteSearch(MapData.Instance.MapDataArray.GetLength(0), MapData.Instance.MapDataArray.GetLength(1), MapData.Instance);
        //曲がり角の数を取得する
        _curveCount = _mapRouteSearch.CurvePosition.Count;
        //曲がり角の数と同じ要素数の配列を生成
        CurvePosition.Instance.CurvePos = new Vector3[_curveCount];
        //生成した配列にタイルマップの座標をワールド座標で格納
        for (int index = 0; index < _curveCount; index++) {
            CurvePosition.Instance.CurvePos[index] = _tileMap.GetCellCenterWorld(new Vector3Int(_mapRouteSearch.CurvePosition[index][1] + _horizontalMin, -_mapRouteSearch.CurvePosition[index][0] + _varticalMax));
        }
    }
    #endregion
}
