// ---------------------------------------------------------  
// EnemyBar.cs  
// 敵のHPバー
// 作成日:  5/16
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
public class EnemyBar : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("スライダー")]
    private Slider _hpSlider = default;
    /// <summary>
    /// 実行一回目か
    /// </summary>
    private bool _isFirst = true;

    #endregion

    #region メソッド  

    /// <summary>
    /// スライダーの上限を設定
    /// </summary>
    private void MaxSlider(float maxHP) 
    {
        _hpSlider.maxValue = maxHP;
    }


    /// <summary>  
    /// 敵のHPバーを表示
    /// </summary>  
    public void EnemyHPBar(float hp) 
    {           
        // 一回だけ実行
        if (_isFirst) 
        {
            _isFirst = false;
            MaxSlider(hp);
        }
        _hpSlider.value = hp;
    }
  
    #endregion
}
