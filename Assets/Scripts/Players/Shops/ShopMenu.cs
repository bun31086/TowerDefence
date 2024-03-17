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


    public bool IsShop {
        get => _isShop;
        set => _isShop = value;
    }

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
        if (IsShop) {
            //x座標だけを変更する
            _shopX = _shopMenu.transform.position.x + CONST_SHOP_X;
            //ショップフラグをOFF
            IsShop = false;
            //メニューを移動させる
            _shopMenu.transform.position = new Vector2(_shopX, _shopMenu.transform.position.y);
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
