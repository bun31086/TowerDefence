// ---------------------------------------------------------  
// ShootRangePresenter.cs  
// 射撃範囲のViewとModelをつなぐ
// 作成日:  3/21
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class ShootRangePresenter : MonoBehaviour
{

    #region 変数  

    [Tooltip("半径を直径に変更するために使用")]
    private const int CONST_TWOTIMES = 2;
    [SerializeField, Tooltip("View"), Header("射撃範囲view")]
    private ShootRangeView _shootRangeView = default;
    [SerializeField, Tooltip("Model"), Header("射撃範囲Model")]
    private ShopMenu _shopMenu = default;

    #endregion
  
    #region プロパティ  
  
    #endregion
  
    #region メソッド  
  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Start()
     {
        //タワーのボタンが押されたとき
        _shopMenu.TowerRange
            .Subscribe(size => {
                //射撃範囲サイズを変更する
                _shootRangeView.SizeChange(size * CONST_TWOTIMES);
            })
            .AddTo(this);
        //ショップボタンが押されたとき
        _shopMenu.IsShop
            .Subscribe(isDisplay => {
                //表示を変更する
                _shootRangeView.DisplaySwitch(isDisplay);
            })
            .AddTo(this);
     }
    
    #endregion
}
