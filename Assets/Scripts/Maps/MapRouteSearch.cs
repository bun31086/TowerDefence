// ---------------------------------------------------------  
// MapRouteSearch.cs  
// 敵の経路探索
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using System;

public class MapRouteSearch
{

    #region 変数  

    //一個前に探索していた配列座標
    private int[] _beforePosition = new int[2];
    //マップ配列スクリプト
    private MapData _mapData = default;
    //探索開始地点
    private int[] _searchedPos = new int[2];
    //配列で道を示す数字
    private const int CONST_ROAD_NUMBER = 1;
    //配列でスタートを示す数字
    private const int CONST_START_NUMBER = 2;
    //配列でゴールを示す数字
    private const int CONST_GOAL_NUMBER = 3;
    //道のタイル数
    private int _tileCount = 0;
    //曲がり角数
    private int _curveCount = 0;
    //曲がり角の座標格納配列
    private List<int[]> _curvePosition = new List<int[]>();
    //マップ配列のy軸の要素数
    private int _yLength = default;
    //マップ配列のx軸の要素数
    private int _xLength = default;
    //配列のY座標の配列番号
    private const int CONST_Y_NUMBER = 0;
    //配列のX座標の配列番号
    private const int CONST_X_NUMBER = 1;
    //調べている座標
    private int[] _searchPos = new int[2];
    //エラー対策用変数
    private int _error = 0;
    //進む方向
    private int[,] _fourDirection =
    {
            { -1, 0 },       //上
            { 0, 1},        //右
            { 1, 0},        //下
            { 0, -1}        //左
    };


    /// <summary>
    /// コンストラクタ
    /// </summary>
    public MapRouteSearch(int yLength,int xLength, MapData mapData) {
        //マップ配列のy軸の要素数
        _yLength = yLength;
        //マップ配列のx軸の要素数
        _xLength = xLength;
        //マップ配列スクリプト
        _mapData = mapData;
        //ルート探索開始
        StartSearch();
    }

    #endregion

    #region プロパティ  
    public List<int[]> CurvePosition {
        get => _curvePosition;
        set => _curvePosition = value;
    }

    #endregion

    #region メソッド  

    /// <summary>
    /// 探索開始位置を探す
    /// </summary>
    public void StartSearch() {
        int indexX = 0;
        int indexY = 0;
        //探索開始位置を探す
        foreach (int startPos in _mapData.MapDataArray) {
            //探索開始位置だったら
            if (startPos == CONST_START_NUMBER) {
                //探索位置の座標を格納
                _searchedPos[CONST_Y_NUMBER] = indexY;
                _searchedPos[CONST_X_NUMBER] = indexX;
                break;
            }
            indexX++;
            if (indexX >= _xLength) {
                indexY++;
                indexX = 0;
            }
        }
        //ルート探索開始(引数はタイルの枚数カウント)
        SearchRoute(0);
    }


    /// <summary>
    /// ルート探索をする
    /// </summary>
    private void SearchRoute(int tileCount) {
        _tileCount = tileCount;
        //今いるマスの周り４方向を調べる(上右下左の順で探索)
        for (int y = 0; y < 4; y++) {
            //調べている地点を格納
            _searchPos[CONST_Y_NUMBER] =  _searchedPos[CONST_Y_NUMBER] + _fourDirection[y, CONST_Y_NUMBER];
            _searchPos[CONST_X_NUMBER] =  _searchedPos[CONST_X_NUMBER] + _fourDirection[y, CONST_X_NUMBER];

            //元いた地点を調べているなら
            if (_searchPos[CONST_Y_NUMBER] == _beforePosition[CONST_Y_NUMBER] && _searchPos[CONST_X_NUMBER] == _beforePosition[CONST_X_NUMBER]) {
                //次の方向を調べる
                continue;
            }
            //配列範囲外を調べているなら
            if (_searchPos[CONST_Y_NUMBER] < 0 || _searchPos[CONST_Y_NUMBER] > _yLength || _searchPos[CONST_X_NUMBER] < 0 || _searchPos[CONST_X_NUMBER] > _xLength) {
                //次の方向を調べる
                continue;
            }
            //道を調べているなら
            if (_mapData.MapDataArray[_searchPos[CONST_Y_NUMBER], _searchPos[CONST_X_NUMBER]] == CONST_ROAD_NUMBER) {
                //タイル数をカウントする
                _tileCount++;
                //曲がり角か
                if ((_fourDirection[y,CONST_Y_NUMBER] != _searchedPos[CONST_Y_NUMBER] - _beforePosition[CONST_Y_NUMBER])&& (_fourDirection[y, CONST_X_NUMBER] != _searchedPos[CONST_X_NUMBER] - _beforePosition[CONST_X_NUMBER])) {
                    //曲がり角数をカウントする
                    _curveCount++;
                    //曲がり角の座標を格納
                    CurvePosition.Add(new int[] { _searchedPos[CONST_Y_NUMBER], _searchedPos[CONST_X_NUMBER] });
                }

                //元居た地点を格納
                _beforePosition[CONST_Y_NUMBER] = _searchedPos[CONST_Y_NUMBER];
                _beforePosition[CONST_X_NUMBER] = _searchedPos[CONST_X_NUMBER];
                //探索するタイルの座標を変更する
                _searchedPos[CONST_Y_NUMBER] = _searchPos[CONST_Y_NUMBER];
                _searchedPos[CONST_X_NUMBER] = _searchPos[CONST_X_NUMBER];

                //エラー対策用
                if (_error >= 200) {
                    return;
                }
                _error++;
                //次のタイルの周りを探索
                SearchRoute(_tileCount);
                return;
            }
            //もしゴールなら
            else if (_mapData.MapDataArray[_searchPos[CONST_Y_NUMBER], _searchPos[CONST_X_NUMBER]] == CONST_GOAL_NUMBER) {
                //ゴールのポジションを格納
                CurvePosition.Add(new int[] { _searchPos[CONST_Y_NUMBER], _searchPos[CONST_X_NUMBER] });
                return;
            }

        }
    }
    #endregion
}
