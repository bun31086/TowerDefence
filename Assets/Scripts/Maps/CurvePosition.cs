// ---------------------------------------------------------  
// CurvePosition.cs  
// 曲がり角を格納
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 曲がり角を格納
/// </summary>
public class CurvePosition : SingletonMonoBehaviour<CurvePosition> 
{
    #region 変数  

    [Tooltip("曲がり角格納配列")]
    private Vector3[] _curvePos = default;

    #endregion
    #region プロパティ  
    /// <summary>
    /// 曲がり角格納配列
    /// </summary>
    public Vector3[] CurvePos 
    {
        get => _curvePos;
        set => _curvePos = value;
    }

    #endregion
}
