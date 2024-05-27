// ---------------------------------------------------------  
// MapRouteSearch.cs  
// 敵の経路探索
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System.Collections.Generic;
/// <summary>
/// 敵の経路探索
/// </summary>
public class MapRouteSearch {
    #region 変数  

    // マップ配列スクリプト
    private MapData _mapData = default;
    // 一個前に探索していた座標
    private int[] _searchPositionBefore = new int[2];
    // 現在の座標
    private int[] _searchPosition = new int[2];
    // 現在探索している座標
    private int[] _searchPositionAfter = new int[2];
    // 道のタイル数
    private int _tileCount = 0;
    // 曲がり角の座標格納配列
    private List<int[]> _curvePosition = new List<int[]>();
    // マップ配列のy軸の要素数
    private int _yLength = default;
    // マップ配列のx軸の要素数
    private int _xLength = default;
    // マスの周りの数
    private const int AROUND_TILE = 4;
    // 配列番号のEnum
    private enum INDEXS {
        // 配列のX座標の配列番号
        INDEX_X = 0,
        // 配列のY座標の配列番号
        INDEX_Y = 1
    }
    private INDEXS _indexs = default;
    // 探索4方向
    private int[,] _fourDirections =
    {
            { -1,  0},       // 上
            {  0,  1},       // 右
            {  1,  0},       // 下
            {  0, -1}        // 左
    };

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="yLength">マップ配列のy軸の要素数</param>
    /// <param name="xLength">マップ配列のx軸の要素数</param>
    /// <param name="mapData">マップ配列スクリプト</param>
    public MapRouteSearch(int yLength, int xLength, MapData mapData) {
        _yLength = yLength;
        _xLength = xLength;
        _mapData = mapData;
        // ルート探索開始
        StartSearch();
    }

    #endregion
    #region プロパティ

    /// <summary>
    /// 曲がり角の座標格納配列
    /// </summary>
    public List<int[]> CurvePosition {
        get => _curvePosition;
        set => _curvePosition = value;
    }

    #endregion
    #region メソッド  

    /// <summary>
    /// 探索開始位置を探す
    /// </summary>
    private void StartSearch() {
        int indexX = 0;
        int indexY = 0;
        // 探索開始位置を探す
        foreach (MapType startPos in _mapData.MapDataArray) {
            // 探索している座標がスタートのとき
            if (startPos == MapType.Start) {
                // 探索位置の座標を格納
                _searchPosition[(int)INDEXS.INDEX_Y] = indexY;
                _searchPosition[(int)INDEXS.INDEX_X] = indexX;
                break;
            }
            // X座標をインクリメント
            indexX++;
            // X座標が配列のXの要素数より大きいとき
            if (indexX >= _xLength) {
                // Y座標をインクリメント
                indexY++;
                // X座標を初期化
                indexX = 0;
            }
        }
        // ルート探索開始(引数はタイルの枚数カウント)
        SearchRoute(0);
    }


    /// <summary>
    /// ルート探索をする
    /// </summary>
    private void SearchRoute(int tileCount) {
        //タイルの枚数を格納
        _tileCount = tileCount;
        // 今いるマスの周り４方向を調べる(上右下左の順で探索)
        for (int y = 0; y < AROUND_TILE; y++) {
            // 調べている地点を格納
            _searchPositionAfter[(int)INDEXS.INDEX_Y] = _searchPosition[(int)INDEXS.INDEX_Y] + _fourDirections[y, (int)INDEXS.INDEX_Y];
            _searchPositionAfter[(int)INDEXS.INDEX_X] = _searchPosition[(int)INDEXS.INDEX_X] + _fourDirections[y, (int)INDEXS.INDEX_X];

            // 元いた地点を調べているとき
            if (_searchPositionAfter[(int)INDEXS.INDEX_Y] == _searchPositionBefore[(int)INDEXS.INDEX_Y] &&
                _searchPositionAfter[(int)INDEXS.INDEX_X] == _searchPositionBefore[(int)INDEXS.INDEX_X]) {
                // 次の方向を調べる
                continue;
            }
            // 配列範囲外を調べているとき
            if ((_searchPositionAfter[(int)INDEXS.INDEX_Y] < 0) || (_searchPositionAfter[(int)INDEXS.INDEX_Y] > _yLength) ||
                (_searchPositionAfter[(int)INDEXS.INDEX_X] < 0) || (_searchPositionAfter[(int)INDEXS.INDEX_X] > _xLength)) {
                // 次の方向を調べる
                continue;
            }
            // 道を調べているとき
            if (_mapData.MapDataArray[_searchPositionAfter[(int)INDEXS.INDEX_Y], _searchPositionAfter[(int)INDEXS.INDEX_X]] == MapType.Road) {
                // タイル数をカウントする
                _tileCount++;
                // 曲がり角のとき
                if ((_fourDirections[y, (int)INDEXS.INDEX_Y] != (_searchPosition[(int)INDEXS.INDEX_Y] - _searchPositionBefore[(int)INDEXS.INDEX_Y])) &&
                    (_fourDirections[y, (int)INDEXS.INDEX_X] != (_searchPosition[(int)INDEXS.INDEX_X] - _searchPositionBefore[(int)INDEXS.INDEX_X]))) {
                    // 曲がり角の座標を格納
                    CurvePosition.Add(new int[] { _searchPosition[(int)INDEXS.INDEX_Y], _searchPosition[(int)INDEXS.INDEX_X] });
                }
                // 元居た地点を格納
                _searchPositionBefore[(int)INDEXS.INDEX_Y] = _searchPosition[(int)INDEXS.INDEX_Y];
                _searchPositionBefore[(int)INDEXS.INDEX_X] = _searchPosition[(int)INDEXS.INDEX_X];
                // 探索するタイルの座標を変更する
                _searchPosition[(int)INDEXS.INDEX_Y] = _searchPositionAfter[(int)INDEXS.INDEX_Y];
                _searchPosition[(int)INDEXS.INDEX_X] = _searchPositionAfter[(int)INDEXS.INDEX_X];
                // 次のタイルの周りを探索
                SearchRoute(_tileCount);
                return;
            }
            // 探索している座標がゴールのとき
            else if (_mapData.MapDataArray[_searchPositionAfter[(int)INDEXS.INDEX_Y], _searchPositionAfter[(int)INDEXS.INDEX_X]] == MapType.Goal) {
                // ゴールのポジションを格納
                CurvePosition.Add(new int[] { _searchPositionAfter[(int)INDEXS.INDEX_Y], _searchPositionAfter[(int)INDEXS.INDEX_X] });
                return;
            }
        }
    }

    #endregion
}
