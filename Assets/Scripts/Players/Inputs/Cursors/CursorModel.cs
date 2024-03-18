// ---------------------------------------------------------  
// CursorModel.cs  
// カーソル計算スクリプト
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.Tilemaps;
using UniRx;


public class CursorModel : MonoBehaviour {

    #region 変数

    [Tooltip("ワールド座標変換時に使用")]
    private Camera _camera = default;
    [Tooltip("カーソルサイズ")]
    private Vector2ReactiveProperty _cursorSize = new Vector2ReactiveProperty();
    [Tooltip("カーソルの移動先座標")]
    private Vector3ReactiveProperty _cursorPosition = new Vector3ReactiveProperty();
    [SerializeField, Tooltip("TileMapコンポーネント"), Header("TileMapコンポーネント")]
    private Tilemap _tile = default;
    [Tooltip("タイルTransform")]
    private Transform _tileTransform = default;
    [SerializeField,Tooltip("カーソルと重なっているタイル")]
    private int _tileNumber = default;


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


    #endregion

    #region プロパティ  
    public IReadOnlyReactiveProperty<Vector3> CursorPosition => _cursorPosition;
    public IReadOnlyReactiveProperty<Vector2> CursorSize => _cursorSize;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start() {
        _camera = Camera.main;
        _tileTransform = _tile.transform;
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
        //左下を原点にする
        BoundsInt bounds = _tile.cellBounds;
        //タイルマップの一番左と一番下の座標を格納
        _horizontalMin = bounds.min.x;
        _varticalMax = bounds.max.y + CONST_MINAS_ONE;
        //0からどのくらい離れているか
        int xOffset = -_horizontalMin;
        //カーソルと重なるタイルの配列座標を調べる
        _tileNumber = MapData.Instance.MapDataArray[bounds.max.y - 1 - tilemapPosition.y, tilemapPosition.x + xOffset];
        print("Tile:" + _tileNumber);
    }

    #endregion
}
