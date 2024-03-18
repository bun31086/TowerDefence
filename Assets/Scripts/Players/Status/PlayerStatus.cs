// ---------------------------------------------------------  
// PlayerStatus.cs  
// プレイヤーのHP,所持金を管理する
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour,IDamageable
{

    #region 変数  

    [SerializeField,Tooltip("プレイヤーの機数"), Header("プレイヤー残機")]
    private int _playerHP = 5;
    [SerializeField, Tooltip("プレイヤーの所持金"), Header("プレイヤー所持金")]
    private int _playerMoney = 100;


    #endregion

    #region プロパティ  
    public int PlayerMoney {
        get => _playerMoney;
        set => _playerMoney = value;
    }

    #endregion

    #region メソッド  

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void DamageHit(int damage) {
        _playerHP -= damage;
    }

    #endregion
}
