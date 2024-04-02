// ---------------------------------------------------------  
// CreatedPool.cs  
// 作り終わったプール名を格納
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System.Collections.Generic;

public class CreatedPool : SingletonMonoBehaviour<CreatedPool> {

    #region 変数  

    /// <summary>
    /// 生成した弾プールリスト
    /// </summary>
    private List<string> _poolList = new List<string>();


    #endregion

    #region プロパティ 
    
    /// <summary>
    /// 生成した弾プールリスト
    /// </summary>
    public List<string> PoolList {
        get => _poolList;
        set => _poolList = value;
    }

    #endregion
}
