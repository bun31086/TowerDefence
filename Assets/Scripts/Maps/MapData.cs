// ---------------------------------------------------------  
// MapData.cs  
// マップのデータを格納する
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MapData : MonoBehaviour
{

    #region 変数  

    [Tooltip("Map格納配列")]
    private int[,] _mapDataArray = default;


    #endregion

    #region プロパティ  
    public int[,] MapDataArray {
        get => _mapDataArray;
        set => _mapDataArray = value;
    }

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
  
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
     }

    /// <summary>
    /// 送られてきた情報のサイズにマップを変更する
    /// </summary>
    public void ArraySizeChange(int horizontal , int vartical) {
        _mapDataArray = new int[vartical, horizontal];
    }

    #endregion
}
