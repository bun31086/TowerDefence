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
public class MapRouteSearch 
{
    #region 変数  

    // 一個前に探索していた配列座標
    private int[] _beforePosition = new int[2];
    // マップ配列スクリプト
    private MapData _mapData = default;
    // 探索開始地点
    private int[] _searchedPos = new int[2];
    // 道のタイル数
    private int _tileCount = 0;
    // 曲がり角の座標格納配列
    private List<int[]> _curvePosition = new List<int[]>();
    // マップ配列のy軸の要素数
    private int _yLength = default;
    // マップ配列のx軸の要素数
    private int _xLength = default;
    // 配列のY座標の配列番号
    private const int INDEX_Y = 0;
    // 配列のX座標の配列番号
    private const int INDEX_X = 1;
    // 調べている座標
    private int[] _searchPos = new int[2];
    // 進む方向
    private int[,] _fourDirection =
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
    public MapRouteSearch(int yLength, int xLength, MapData mapData) 
    {
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
    public List<int[]> CurvePosition 
    {
        get => _curvePosition;
        set => _curvePosition = value;
    }

    #endregion
    #region メソッド  

    /// <summary>
    /// 探索開始位置を探す
    /// </summary>
    public void StartSearch() 
    {
        int indexX = 0;
        int indexY = 0;
        // 探索開始位置を探す
        foreach (MapDataEnum startPos in _mapData.MapDataArray) 
        {
            // 探索開始位置だったら
            if (startPos == MapDataEnum.Start) 
            {
                // 探索位置の座標を格納
                _searchedPos[INDEX_Y] = indexY;
                _searchedPos[INDEX_X] = indexX;
                break;
            }
            indexX++;
            if (indexX >= _xLength) 
            {
                indexY++;
                indexX = 0;
            }
        }
        // ルート探索開始(引数はタイルの枚数カウント)
        SearchRoute(0);
    }


    /// <summary>
    /// ルート探索をする
    /// </summary>
    private void SearchRoute(int tileCount) 
    {
        _tileCount = tileCount;
        // 今いるマスの周り４方向を調べる(上右下左の順で探索)
        for (int y = 0; y < 4; y++) 
        {
            // 調べている地点を格納
            _searchPos[INDEX_Y] = _searchedPos[INDEX_Y] + _fourDirection[y, INDEX_Y];
            _searchPos[INDEX_X] = _searchedPos[INDEX_X] + _fourDirection[y, INDEX_X];

            // 元いた地点を調べているなら
            if (_searchPos[INDEX_Y] == _beforePosition[INDEX_Y] && _searchPos[INDEX_X] == _beforePosition[INDEX_X]) 
            {
                // 次の方向を調べる
                continue;
            }
            // 配列範囲外を調べているなら
            if (_searchPos[INDEX_Y] < 0 || _searchPos[INDEX_Y] > _yLength || _searchPos[INDEX_X] < 0 || _searchPos[INDEX_X] > _xLength) 
            {
                // 次の方向を調べる
                continue;
            }
            // 道を調べているなら
            if (_mapData.MapDataArray[_searchPos[INDEX_Y], _searchPos[INDEX_X]] == MapDataEnum.Road) 
            {
                // タイル数をカウントする
                _tileCount++;
                // 曲がり角か
                if ((_fourDirection[y, INDEX_Y] != _searchedPos[INDEX_Y] - _beforePosition[INDEX_Y]) && (_fourDirection[y, INDEX_X] != _searchedPos[INDEX_X] - _beforePosition[INDEX_X])) 
                {
                    // 曲がり角の座標を格納
                    CurvePosition.Add(new int[] { _searchedPos[INDEX_Y], _searchedPos[INDEX_X] });
                }

                // 元居た地点を格納
                _beforePosition[INDEX_Y] = _searchedPos[INDEX_Y];
                _beforePosition[INDEX_X] = _searchedPos[INDEX_X];
                // 探索するタイルの座標を変更する
                _searchedPos[INDEX_Y] = _searchPos[INDEX_Y];
                _searchedPos[INDEX_X] = _searchPos[INDEX_X];
                // 次のタイルの周りを探索
                SearchRoute(_tileCount);
                return;
            }
            // もしゴールなら
            else if (_mapData.MapDataArray[_searchPos[INDEX_Y], _searchPos[INDEX_X]] == MapDataEnum.Goal) 
            {
                // ゴールのポジションを格納
                CurvePosition.Add(new int[] { _searchPos[INDEX_Y], _searchPos[INDEX_X] });
                return;
            }
        }
    }
    #endregion
}
