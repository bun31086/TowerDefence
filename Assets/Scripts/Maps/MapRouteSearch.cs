// ---------------------------------------------------------  
// MapRouteSearch.cs  
// 敵の経路探索スクリプト
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapRouteSearch : MonoBehaviour {

    #region 変数  

    [Tooltip("一個前に探索していた配列座標")]
    private Vector2Int _beforePosition = default;
    [Tooltip("曲がり角の配列座標格納リスト")]
    private List<int[]> _curvePosition = new List<int[]>();
    [SerializeField, Tooltip("マップ配列スクリプト")]
    private MapData _mapData = default;
    [SerializeField, Tooltip("タイルマップオブジェクト")]
    private Tilemap _tilemap = default;
    [Tooltip("探索開始地点")]
    private Vector2Int _searchedPos = default;
    [Tooltip("配列で道を示す数字")]
    private int _roadInt = 1;
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


    //進む方向
    private Vector2Int[] _fourDorection =
    {
            new Vector2Int(-1, 0) ,       //上
            new Vector2Int(0, 1),        //右
            new Vector2Int(1, 0),       //下
            new Vector2Int(0, -1)        //左
    };

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake() {
    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start() {
        //タイルマップのワールド座標を取得
        //_tilemap.GetCellCenterWorld(new Vector3Int(1, 2, 0));

        RouteSearch(new Vector2Int(0, 13));
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update() {
    }

    /// <summary>
    /// ルート探索
    /// </summary>
    /// <param name="startPos">開始地点</param>
    private void RouteSearch(Vector2Int startPos) {
        //指定された位置から探索開始する
        _searchedPos = startPos;
        //インデックス初期化
        _index = 0;
        //今いるマスの周り４方向を調べる(上右下左の順で探索)
        foreach (Vector2Int vector2Int in _fourDorection) {
            //元いた地点を調べてるなら
            if (new Vector2Int(_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y) == _beforePosition) {
                continue;
            }
            //もし調べてる地点が配列範囲外なら
            if (_searchedPos.x + vector2Int.x < 0 || _searchedPos.x + vector2Int.x > 9 || _searchedPos.y + vector2Int.y < 0 || _searchedPos.y + vector2Int.y > 17) {
                continue;
            }
            //もし道があるなら
            if (_mapData.MapDataArray[_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y] == _roadInt) {
                //タイル数をカウントする
                _tileCount++;
                //曲がり角か
                if (vector2Int != _searchedPos - _beforePosition) {
                    //曲がり角の座標を格納
                    _curveCount++;
                    //_curvePoints.Add()
                    //    _tilemap.GetCellCenterWorld(new Vector3Int(1, 2, 0));
                    //_varticalMax - pos.y + CONST_MINAS_ONE, pos.x - _horizontalMin
                }

                //元居た地点を格納
                _beforePosition = _searchedPos;
                //次のタイルの周りを探索
                RouteSearch(new Vector2Int(_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y));
            }
            //もし曲がり角なら配列に格納
            //インクリメント
            _index++;
        }



        //曲がり角を取得し、配列に格納
        //その間を敵のスピードで進む
        //スタート地点を設定


    }


    private void RouteSearchSecond(Vector2Int startPos) {
        //指定された位置から探索開始する
        _searchedPos = startPos;
        //


        //今いるマスの周り４方向を調べる(上右下左の順で探索)
        foreach (Vector2Int vector2Int in _fourDorection) {
            //元いた地点を調べてるなら
            if (new Vector2Int(_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y) == _beforePosition) {
                Debug.Log("ONAJI");
                continue;
            }
            //もし調べてる地点が配列範囲外なら
            if (_searchedPos.x + vector2Int.x < 0 || _searchedPos.x + vector2Int.x > _mapData.MapDataArray.GetLength(0) || _searchedPos.y + vector2Int.y < 0 || _searchedPos.y + vector2Int.y > 17) {
                Debug.Log("CONT");
                continue;
            }
            //もし道があるなら
            if (_mapData.MapDataArray[_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y] == _roadInt) {
                Debug.Log("ROAD");
                //タイル数をカウントする
                _tileCount++;
                Debug.LogError(vector2Int);
                Debug.LogError(_searchedPos - _beforePosition);
                //曲がり角か
                if (vector2Int != _searchedPos - _beforePosition) {
                    //曲がり角の座標を格納
                    _curveCount++;
                    //_curvePoints.Add()
                    //    _tilemap.GetCellCenterWorld(new Vector3Int(1, 2, 0));
                    //_varticalMax - pos.y + CONST_MINAS_ONE, pos.x - _horizontalMin
                }

                //元居た地点を格納
                _beforePosition = _searchedPos;
                //次のタイルの周りを探索
                RouteSearch(new Vector2Int(_searchedPos.x + vector2Int.x, _searchedPos.y + vector2Int.y));
            }
            //もし曲がり角なら配列に格納
            print("NOT");
        }
    }
    #endregion
}
