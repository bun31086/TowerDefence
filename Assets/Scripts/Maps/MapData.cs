// ---------------------------------------------------------  
// MapData.cs  
// マップのデータを格納する
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  
/// <summary>
/// Map格納配列
/// </summary>
public class MapData : SingletonMonoBehaviour<MapData>
{
    #region 変数  

    /// <summary>
    /// タイルの種類Enum
    /// </summary>
    private MapDataEnum[,] _mapDataArray = default;

    #endregion
    #region プロパティ
    /// <summary>
    /// タイルの種類Enum
    /// </summary>
    public MapDataEnum[,] MapDataArray 
    {
        get => _mapDataArray;
        set => _mapDataArray = value;
    }

    #endregion
}
