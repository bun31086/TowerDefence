// ---------------------------------------------------------  
// ITowerTell.cs  
// タワーの情報を伝える
// 作成日:  3/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public interface ITowerInformationTell
{

    #region 変数  

    public int TowerMoney {
        get;
    }
    public string TowerName {
        get;
    }
    public string TowerExplanation {
        get;
    }

    #endregion

    #region プロパティ  



    #endregion

}
