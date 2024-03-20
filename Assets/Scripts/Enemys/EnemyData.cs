// ---------------------------------------------------------  
// EnemyData.cs  
// 敵のデータ
// 作成日:  3/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/CreateEnemyData")]
public class EnemyData : ScriptableObject 
{

    #region 変数  

    [SerializeField, Tooltip("敵の移動スピード"), Header("敵の移動速度")]
    private float _speed = default;
    [SerializeField, Tooltip("敵の攻撃力"), Header("敵の攻撃力")]
    private int _power = default;
    [SerializeField, Tooltip("敵の所持金"), Header("敵の金額")]
    private int _money = default;


    #endregion

    #region プロパティ  
    public float Speed {
        get => _speed;
        set => _speed = value;
    }
    public int Power {
        get => _power;
        set => _power = value;
    }
    public int Money {
        get => _money;
        set => _money = value;
    }

    #endregion

}
