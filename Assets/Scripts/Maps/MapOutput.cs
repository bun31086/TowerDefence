// ---------------------------------------------------------  
// MapOutput.cs  
// タイルマップを配列に格納
// 作成日:  3/12
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
/// <summary>
/// タイルマップを配列に格納
/// </summary>
public class MapOutput : MonoBehaviour {

    #region 変数  

    [SerializeField, Tooltip("マップ配列に格納したいタイルマップ"), Header("タイルマップ")]
    private Tilemap _tileMap = default;
    [SerializeField, Tooltip("使用するタイルを格納"), Header("使用するタイル")]
    private List<Tile> _tileType = new List<Tile>();
    [Tooltip("タイルマップの横サイズ")]
    private int _tileHorizontalSize = default;
    [Tooltip("タイルマップの縦サイズ")]
    private int _tileVarticalSize = default;
    [Tooltip("タイルマップのX座標で一番小さい値")]
    private int _tileHorizontalMin = default;
    [Tooltip("タイルマップのY座標で一番大きい値")]
    private int _tileVarticalMax = default;
    [Tooltip("タイルマップのY座標で一番小さい値")]
    private int _tileVarticalMin = default;
    [Tooltip("ルート探索スクリプト")]
    private MapRouteSearch _mapRouteSearch = default;
    [Tooltip("曲がり角の数")]
    private int _curveCount = default;

    #endregion
    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Awake() {
        Output();
    }

    /// <summary>
    /// タイルマップを配列に落としこむ
    /// </summary>
    private void Output() {
        MapData mapData = MapData.Instance;
        // 左下を原点にする
        BoundsInt bounds = _tileMap.cellBounds;
        // タイルマップの一番左と一番下の座標を格納
        _tileHorizontalMin = bounds.min.x;
        _tileVarticalMin = bounds.min.y;
        _tileVarticalMax = bounds.max.y - 1;
        // 0からどのくらい離れているか
        int xOffset = -_tileHorizontalMin;
        int yOffset = -_tileVarticalMin;
        // タイルマップのサイズを調べる
        _tileHorizontalSize = xOffset + bounds.max.x;
        _tileVarticalSize = yOffset + bounds.max.y;
        // タイルマップの情報のサイズの配列を生成
        mapData.MapDataArray = new MapType[_tileVarticalSize, _tileHorizontalSize];


        // タイルマップのすべてのタイルの枚数繰り返す
        foreach (Vector3Int tile in _tileMap.cellBounds.allPositionsWithin) {
            // タイルが無かったら
            if (!_tileMap.HasTile(tile)) {
                // 処理を飛ばす
                continue;
            }
            // タイルの種類分繰り返す
            int index = default;
            foreach (Tile tileType in _tileType) {
                // スプライトが一致しているか判定
                if (_tileMap.GetTile(tile) == tileType) {
                    // 特定のスプライトと一致している場合は配列のそのタイルに対応した数字を格納
                    mapData.MapDataArray[bounds.max.y - 1 - tile.y, tile.x + xOffset] = (MapType)index;
                    break;
                }
                index++;
            }
        }
        // ルートを探索する(配列内)
        _mapRouteSearch = new MapRouteSearch(mapData.MapDataArray.GetLength(0), mapData.MapDataArray.GetLength(1), mapData);
        // 曲がり角の数を取得する
        _curveCount = _mapRouteSearch.CurvePosition.Count;
        // 曲がり角の数と同じ要素数の配列を生成
        CurvePosition.Instance.CurvePos = new Vector3[_curveCount];
        // 曲がり角の数、繰り返す
        for (int index = 0; index < _curveCount; index++) {
            // 曲がり角の座標を格納
            CurvePosition.Instance.CurvePos[index] =
                _tileMap.GetCellCenterWorld
                (
                    new Vector3Int(_mapRouteSearch.CurvePosition[index][1] + _tileHorizontalMin,
                                   -_mapRouteSearch.CurvePosition[index][0] + _tileVarticalMax)
                );
        }
    }

    #endregion
}
