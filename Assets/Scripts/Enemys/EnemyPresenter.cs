// ---------------------------------------------------------  
// EnemyPresenter.cs  
// 敵のHPの仲介
// 作成日:  5/16
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;
public class EnemyPresenter : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("敵の基底クラス")]
    private EnemyBase _enemyBase = default;
    [SerializeField,Tooltip("敵のHPバークラス")]
    private EnemyBar _enemyBar = default;

    #endregion
    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start() 
    {
        _enemyBase.EnemyHP
            .Subscribe(hp => 
            {
                _enemyBar.EnemyHPBar(hp);
            }
            ).AddTo(this);
    }
    
    #endregion
}
