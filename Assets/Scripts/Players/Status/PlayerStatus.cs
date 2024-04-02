// ---------------------------------------------------------  
// PlayerStatus.cs  
// プレイヤーのHP,所持金を管理する
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class PlayerStatus : MonoBehaviour,IDamageable,IMoneyChange
{

    #region 変数  

    [SerializeField,Tooltip("プレイヤーの機数"), Header("プレイヤー残機")]
    private IntReactiveProperty _playerHP = new IntReactiveProperty(5);
    [SerializeField, Tooltip("プレイヤーの所持金"), Header("プレイヤー所持金")]
    private IntReactiveProperty _playerMoney = new IntReactiveProperty(100);


    #endregion

    #region プロパティ  
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;
    public IReadOnlyReactiveProperty<int> PlayerMoney => _playerMoney;

    #endregion

    #region メソッド  

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void DamageHit(int damage) {
        _playerHP.Value -= damage;
    }
    /// <summary>
    /// 金取得処理
    /// </summary>
    /// <param name="damage">金額</param>
    public void MoneyChange(int money) {
        _playerMoney.Value += money;
    }

    #endregion
}
