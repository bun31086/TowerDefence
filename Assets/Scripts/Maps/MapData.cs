// ---------------------------------------------------------  
// MapData.cs  
// マップのデータを格納する
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;

public class MapData : SingletonMonoBehaviour<MapData>
{

    #region 変数  

    //Map格納配列
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
    /// 送られてきた情報のサイズにマップを変更する
    /// </summary>
    public void ArraySizeChange(int vartical,int horizontal ) {
        _mapDataArray = new int[vartical, horizontal];
    }

    #endregion
}
