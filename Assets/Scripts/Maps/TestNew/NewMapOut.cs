// ---------------------------------------------------------  
// NewMapOut.cs  
//   
// 作成日:  3/12
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class NewMapOut : MonoBehaviour
{

    #region 変数  
    [SerializeField, Tooltip("マップ配列を保持しているスクリプト")]
    private MapData _mapData = default;

    [SerializeField, Tooltip("マップ配列に格納したいタイルマップ")]
    private Tilemap _tileMap = default;
    [SerializeField, Tooltip("使用するタイルを格納")]
    private List<Tile> _tileType = new List<Tile>();

    [Tooltip("タイルマップの横サイズ")]
    private int _horizontal = default;
    [Tooltip("タイルマップの縦サイズ")]
    private int _vartical = default;
    [Tooltip("タイルマップのX座標で一番小さい値")]
    private int _horizontalMin = default;
    [Tooltip("タイルマップのY座標で一番小さい値")]
    private int _varticalMin = default;
    [Tooltip("配列が0から始まるため、引く数字")]
    private const int CONST_MINAS_ONE = -1;

    [Tooltip("曲がり角の座標")]
    private List<Vector2Int> _curvePos = new List<Vector2Int>();

    [Tooltip("ルート探索スクリプト")]
    private NewMapRoute _newMapRoute = default;

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
        Output();
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {

     }


    /// <summary>
    /// ボタンが押されたらタイルマップを配列に落としこむ
    /// </summary>
    public void Output() {
        //左下を原点にする
        BoundsInt bounds = _tileMap.cellBounds;
        //タイルマップの一番左と一番下の座標を格納
        _horizontalMin = bounds.min.x;  
        _varticalMin = bounds.min.y; 
        //0からどのくらい離れているか
        int xDistance = -_horizontalMin;
        int yDistance = -_varticalMin;
        //タイルマップのサイズを調べる
        _horizontal = xDistance + bounds.max.x;
        _vartical = yDistance + bounds.max.y;
        Debug.LogError("a:"+_vartical+""+ _horizontal);
        //タイルマップの情報のサイズに配列を変更
        _mapData.ArraySizeChange(_vartical, _horizontal);

        //タイルマップのすべてのタイルの枚数繰り返す
        foreach (Vector3Int pos in _tileMap.cellBounds.allPositionsWithin) {
            // タイルが無かったら
            if (!_tileMap.HasTile(pos)) {
                //処理を中断
                return;
            }
            //タイルの種類分繰り返す
            int index = default;
            foreach (Tile tile in _tileType) {
                // スプライトが一致しているか判定
                if (_tileMap.GetTile(pos) == tile) {
                    Debug.LogWarning("" + (bounds.max.y) +":"+ (pos.x + xDistance));
                    // 特定のスプライトと一致している場合は配列のそのタイルに対応した数字を格納
                    _mapData.MapDataArray[bounds.max.y-1 - pos.y, pos.x + xDistance] = index;
                    break;
                }
                index++;
            }
        }

        // 配列を出力するテスト
        print("Field------------------------------------------");
        for (int y = 0; y < _vartical; y++) {
            string outPutString = "";
            for (int x = 0; x < _horizontal; x++) {
                outPutString += _mapData.MapDataArray[y, x];
            }
            print(outPutString);
        }
        print("Field------------------------------------------");

        //ルートを探索する
        _newMapRoute = new NewMapRoute(_mapData.MapDataArray.GetLength(0), _mapData.MapDataArray.GetLength(1),_mapData);
        //配列からglobalに変換
    }
    #endregion
}
