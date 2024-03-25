// ---------------------------------------------------------  
// PlayerUI.cs  
// プレイヤーステータスのUI
// 作成日:  3/21
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("所持金テキスト"), Header("所持金のテキスト")]
    private Text _moneyText = default;
    [SerializeField,Tooltip("HPテキスト"),Header("HPのテキスト")]
    private Text _hpText = default;

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
  
     }
  

    /// <summary>
    /// 金額表示を変更
    /// </summary>
    /// <param name="money">現在の金額</param>
    public void MoneyChange(int money) {
        //テキスト変更
        _moneyText.text = money.ToString();
    }

    /// <summary>
    /// HPの表示を変更
    /// </summary>
    /// <param name="hp">現在のHP</param>
    public void HPChange(int hp) {
        //テキスト変更
        _hpText.text = hp.ToString();
    }

    #endregion
}
