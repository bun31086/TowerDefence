// ---------------------------------------------------------  
// NewMapRoute.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class NewMapRoute
{

    #region 変数  

    [Tooltip("一個前に探索していた配列座標")]
    private Vector2Int _beforePosition = default;
    [Tooltip("曲がり角の配列座標格納リスト")]
    private List<int[]> _curvePosition = new List<int[]>();
    [Tooltip("マップ配列スクリプト")]
    private MapData _mapData = default;
    [Tooltip("探索開始地点")]
    private Vector2Int _searchedPos = default;
    [Tooltip("配列で道を示す数字")]
    private const int CONST_ROAD_NUMBER = 1;
    [Tooltip("配列でスタートを示す数字")]
    private const int CONST_START_NUMBER = 2;
    [Tooltip("配列でゴールを示す数字")]
    private const int CONST_GOAL_NUMBER = 2;
    [SerializeField, Tooltip("道のタイル数")]
    private int _tileCount = 0;
    [SerializeField, Tooltip("曲がり角数")]
    private int _curveCount = 0;
    [Tooltip("曲がり角の座標格納配列")]
    private List<Vector2Int> _curvePoints = new List<Vector2Int>();
    [Tooltip("曲がり角確認用(0:上 1:左 2:下 3:右)")]
    private int _index = default;
    [Tooltip("マップ配列のy軸の要素数")]
    private int _yLength = default;
    [Tooltip("マップ配列のx軸の要素数")]
    private int _xLength = default;



    private int _a = 0;

    //進む方向
    private Vector2Int[] _fourDorection =
    {
            new Vector2Int(-1, 0) ,       //上
            new Vector2Int(0, 1),        //右
            new Vector2Int(1, 0),       //下
            new Vector2Int(0, -1)        //左
    };

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public NewMapRoute(int yLength,int xLength, MapData mapData) {
        //マップ配列のy軸の要素数
        _yLength = yLength;
        //マップ配列のx軸の要素数
        _xLength = xLength;
        Debug.LogError("xy"+ _yLength + _xLength);

        //マップ配列スクリプト
        _mapData = mapData;
        //ルート探索開始
        StartSearch();
    }


    #endregion

    #region プロパティ  

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
                _searchedPos = new Vector2Int(indexY, indexX);
                Debug.LogError(_searchedPos);
                break;
            }
            indexX++;
            //もしxが
            if (indexX >= _xLength ) {
                indexY++;
                indexX = 0;
            }
        }
        //ルート探索開始
        SearchRoute();
    }


    /// <summary>
    /// ルート探索をする
    /// </summary>
    private void SearchRoute() {
   //     Debug.LogWarning("NowPos:" + _searchedPos);
   //     Debug.LogWarning("BeforePos:" + _beforePosition);

        //今いるマスの周り４方向を調べる(上右下左の順で探索)
        foreach (Vector2Int vector2Int in _fourDorection) {
   //         Debug.LogError("v:" + vector2Int);

            //元いた地点を調べてるなら
            if (new Vector2Int(_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y) == _beforePosition) {
      //          Debug.Log("ONAJI");
                continue;
            }
            //もし調べてる地点が配列範囲外なら
            if (_searchedPos.x + vector2Int.x < 0 || _searchedPos.x + vector2Int.x >= _yLength || _searchedPos.y + vector2Int.y < 0 || _searchedPos.y + vector2Int.y >= _xLength) {
      //          Debug.Log("OUT");
                continue;
            }
            //     Debug.LogError("x" + _searchedPos.x + vector2Int.y);
            //     Debug.LogError("y" + _searchedPos.y + vector2Int.x);
            //もし道があるなら
            if (_mapData.MapDataArray[_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y] == CONST_ROAD_NUMBER) {
                //            Debug.Log("ROAD");
                //タイル数をカウントする
                _tileCount++;
                //曲がり角か
                if (vector2Int != _searchedPos - _beforePosition) {
                    //曲がり角の座標を格納
                    _curveCount++;
                    //_curvePoints.Add(new Vector2Int(0,0));
                    //    _tilemap.GetCellCenterWorld(new Vector3Int(1, 2, 0));
                    //_varticalMax - pos.y + CONST_MINAS_ONE, pos.x - _horizontalMin
                    Debug.LogError("Curve:" + _searchedPos);
                }

                //元居た地点を格納
                _beforePosition = _searchedPos;
                //探索するタイルの座標を変更する
                _searchedPos.x += vector2Int.x;
                _searchedPos.y += vector2Int.y;
                //次のタイルの周りを探索
                if (_a > 200) {
                    Debug.LogWarning("失敗");
                    return;
                }
                _a++;
                SearchRoute();
                return;
            }
            //もしゴールなら
            else if (_mapData.MapDataArray[_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y] == CONST_GOAL_NUMBER) {
                //ゴールの座標を格納
                //_curvePoints.Add(new Vector2Int(0,0));
                //    _tilemap.GetCellCenterWorld(new Vector3Int(1, 2, 0));
                //_varticalMax - pos.y + CONST_MINAS_ONE, pos.x - _horizontalMin
                return;
            }
        }

    }


    #endregion
}
