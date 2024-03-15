// ---------------------------------------------------------  
// TowerSet.cs  
// タワー設置スクリプト
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class TowerSet : MonoBehaviour
{

    #region 変数

    //[Tooltip("マウスが押されたポイント")]
    //Vector2 _touchWorldPosition = default;
    [Tooltip("サイズを合わせる")]
    private GameObject _tileMap = default;

    private Vector2 _vec = default;

    [SerializeField,Tooltip("TileMapコンポーネント")]
    private Tilemap _tile = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {
        /*
         * タイルを押すとタワー選択画面を表示
         * タワーを選択し、決定を押すと、設置できる
         * 
         * 
         * 
         * 
         */

     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        _tileMap = _tile.gameObject;
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
        //サイズ合わせ
        _vec = new Vector2(_tileMap.transform.localScale.x * _tileMap.transform.root.localScale.x, _tileMap.transform.localScale.y * _tileMap.transform.root.localScale.y);
        this.transform.localScale = _vec;

        //もしマウスの右クリックが押されたら
        if (Input.GetMouseButtonDown(0)) {
            //座標を調べる
            Vector2 touchScreenPosition = Input.mousePosition;
            //スクリーン座標からワールド座標に変換
            touchScreenPosition = Camera.main.ScreenToWorldPoint(touchScreenPosition);
            //タイルマップ座標に変換
            Vector3Int ve3i = _tile.WorldToCell(touchScreenPosition);
            Debug.LogError("TILE:" + ve3i);
            Debug.LogError("WORLD:" + _tile.GetCellCenterWorld(new Vector3Int(ve3i.x, ve3i.y, 0)));
            //四捨五入する
            //_touchWorldPosition = new Vector2((float)Math.Round(_touchWorldPosition.x, 0, MidpointRounding.AwayFromZero), (float)Math.Round(_touchWorldPosition.y, 0, MidpointRounding.AwayFromZero));        
            //座標に選択オブジェクトを移動
            this.transform.position = _tile.GetCellCenterWorld(new Vector3Int(ve3i.x, ve3i.y, 0));
            

            //メニューを表示
        }
     }
  
    #endregion
}
