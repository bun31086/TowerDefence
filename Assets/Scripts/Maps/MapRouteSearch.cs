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

public class MapRouteSearch : MonoBehaviour
{

    #region 変数  

    [Tooltip("一個前に探索していた配列座標")]
    private int[] _beforePosition = new int[2];
    [Tooltip("曲がり角の配列座標格納リスト")]
    private List<int[]> _curvePosition = new List<int[]>();
    [SerializeField, Tooltip("マップ配列スクリプト")]
    private MapData _mapData = default;
    [SerializeField,Tooltip("タイルマップオブジェクト")]
    private Tilemap _tilemap = default;

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
        //タイルマップのワールド座標を取得
        _tilemap.GetCellCenterWorld(new Vector3Int(1, 2, 0));
    }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
     }

    /// <summary>
    /// ルート探索
    /// </summary>
    private void RouteSearch() {
        //曲がり角を取得し、配列に格納
        //その間を敵のスピードで進む
        //スタート地点を設定
    
    }

    #endregion
}
