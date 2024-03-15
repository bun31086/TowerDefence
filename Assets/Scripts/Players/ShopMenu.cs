// ---------------------------------------------------------  
// ShopMenu.cs  
// ショップを表示
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("ショップメニュー")]
    private GameObject _shopMenu = default;
    [Tooltip("ずらすX座標")]
    private const float CONST_SHOP_X = 350;
    [Tooltip("ショップが開かれているか")]
    private bool _isShop = false;

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
     /// 更新処理  
     /// </summary>  
     void Update ()
     {

     }


    /// <summary>
    /// メニューボタンが押されたとき
    /// </summary>
    public void ButtonPush() {
        //ショップの座標を定義
        float shopX = default;
        //ショップが開かれていないなら
        if (!_isShop) {
            //x座標だけを変更する
            shopX = _shopMenu.transform.position.x - CONST_SHOP_X;
            //ショップフラグをON
            _isShop = true;
        }
        //ショップが開かれているなら
        else if (_isShop) {
            //x座標だけを変更する
            shopX = _shopMenu.transform.position.x + CONST_SHOP_X;
            //ショップフラグをON
            _isShop = false;
        }
        //メニューを移動させる
        _shopMenu.transform.position = new Vector2(shopX, _shopMenu.transform.position.y);
    }

    /// <summary>
    /// タワーボタンが押されたとき
    /// </summary>
    public void TowerButton() {
        //タワーを生成

    }

    #endregion
}
