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
using UniRx;


public class TowerSet : MonoBehaviour {

    #region 変数

    [Tooltip("ワールド座標変換時に使用")]
    private Camera _camera = default;
    [Tooltip("カーソルサイズ")]
    private Vector2ReactiveProperty _cursorSize = new Vector2ReactiveProperty();
    [Tooltip("カーソルの移動先座標")]
    private Vector3ReactiveProperty _cursorPosition = new Vector3ReactiveProperty();
    [SerializeField, Tooltip("TileMapコンポーネント")]
    private Tilemap _tile = default;
    [Tooltip("TileTransform")]
    private Transform _tileTransform = default;

    #endregion

    #region プロパティ  
    public IReadOnlyReactiveProperty<Vector3> CursorPosition => _cursorPosition;
    public IReadOnlyReactiveProperty<Vector2> CursorSize => _cursorSize;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake() {
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
    void Start() {
        _camera = Camera.main;
        _tileTransform = _tile.transform;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update() {
        //もしメニューが開かれていたら
        //if()
        //処理を中断

    }

    /// <summary>
    /// カーソルのサイズ計算
    /// </summary>
    /// <param name="size">カーソルの元の大きさ</param>

    public void ScaleChange() {
        //カーソルのサイズ計算
        _cursorSize.Value = new Vector2(_tileTransform.localScale.x * _tileTransform.root.localScale.x, _tileTransform.localScale.y * _tileTransform.root.localScale.y);
    }

    /// <summary>
    /// クリックされたスクリーン座標をタイルマップ座標に変更
    /// </summary>
    /// <param name="touchScreenPosition">クリックされたスクリーン座標</param>
    public void SearchPos(Vector2 touchScreenPosition) {
        //スクリーン座標からワールド座標に変換
        touchScreenPosition = _camera.ScreenToWorldPoint(touchScreenPosition);
        //タイルマップ座標に変換
        Vector3Int tilemapPosition = _tile.WorldToCell(touchScreenPosition);
        //オブジェクトの座標をタイルマップ座標に変更
        _cursorPosition.Value = _tile.GetCellCenterWorld(new Vector3Int(tilemapPosition.x, tilemapPosition.y, 0));
    }

    #endregion
}
