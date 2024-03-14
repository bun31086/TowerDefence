// ---------------------------------------------------------  
// PlayerStatus.cs  
// プレイヤーのHPを管理する
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour,IDamageable
{

    #region 変数  

    [SerializeField,Tooltip("プレイヤーの機数")]
    private int _playerHP = 5;

    #endregion

    #region プロパティ  

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
