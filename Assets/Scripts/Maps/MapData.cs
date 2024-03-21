// ---------------------------------------------------------  
// MapData.cs  
// マップのデータを格納する
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System;

/// <summary>
/// Map格納配列
/// </summary>
public enum MapDataEnum {
    Platform,
    Road,
    Start,
    Goal,
    Tower,
    Wood,
    Stone,
}

public class MapData : SingletonMonoBehaviour<MapData>
{
    #region 変数  

    private MapDataEnum[,] _mapDataArray = default;

    #endregion

    #region プロパティ  
    public MapDataEnum[,] MapDataArray {
        get => _mapDataArray;
        set => _mapDataArray = value;
    }

    #endregion
}
