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
    [SerializeField,Tooltip("メニューのX座標")]
    private float _shopX = default;
    [Tooltip("ずらすX座標")]
    private const float CONST_SHOP_X = 350;
    [Tooltip("ショップが開かれているか")]
    private bool _isShop = false;
    [SerializeField, Tooltip("ショップのボタンを押されて一回目のみショップを閉じないようにする")]
    private bool _isShopFirst = default;


    #endregion

    #region プロパティ  
    public bool IsShop {
        get => _isShop;
        set => _isShop = value;
    }

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
    /// ショップを開く
    /// </summary>
    public void ShopOpen() {
        //ショップが開かれていないなら
        if (!IsShop) {
            //x座標だけを変更する
            _shopX = _shopMenu.transform.position.x - CONST_SHOP_X;
            //ショップフラグをON
            IsShop = true;
            //メニューを移動させる
            _shopMenu.transform.position = new Vector2(_shopX, _shopMenu.transform.position.y);
        }
    }

    /// <summary>
    /// ショップを閉じる
    /// </summary>
    public void ShopClose() {
        //ショップが開かれているなら
        if (IsShop && _isShopFirst) {
            //x座標だけを変更する
            _shopX = _shopMenu.transform.position.x + CONST_SHOP_X;
            //ショップフラグをOFF
            IsShop = false;
            //メニューを移動させる
            _shopMenu.transform.position = new Vector2(_shopX, _shopMenu.transform.position.y);
            //一回目のみショップを閉じないようにする
            _isShopFirst = false;
        } else if (!_isShopFirst) {
            //一回目のみショップを閉じないようにする
            _isShopFirst = true;
        }
    }


    /// <summary>
    /// タワーボタンが押されたとき
    /// </summary>
    public void TowerButton() {
        //タワーを生成

    }

    #endregion
}
