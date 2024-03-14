// ---------------------------------------------------------  
// MapData.cs  
// マップのデータを格納する
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  

public class MapData : SingletonMonoBehaviour<MapData>
{

    #region 変数  

    /// <summary>
    /// Map格納配列
    /// </summary>
    private int[,] _mapDataArray = default;

    #endregion

    #region プロパティ  
    public int[,] MapDataArray {
        get => _mapDataArray;
        set => _mapDataArray = value;
    }

    #endregion
}
