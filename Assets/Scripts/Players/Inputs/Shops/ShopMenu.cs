// ---------------------------------------------------------  
// ShopMenu.cs  
// ショップを表示、タワー購入、アップグレード
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopMenu : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("ショップメニュー"),Header("ショップメニュー画面")]
    private GameObject _shopMenu = default;
    [Tooltip("ショップメニューTransform")]
    private Transform _shopTransform = default;
    [Tooltip("メニューの座標")]
    private Vector2 _shopPos = default;
    [Tooltip("ずらすX座標")]
    private const float CONST_SHOP_X = 350;
    [Tooltip("ショップが開かれているか")]
    private bool _isShop = false;
    [Tooltip("ショップのボタンを押されて一回目のみショップを閉じないようにする")]
    private bool _isShopFirst = default;
    [SerializeField, Tooltip("タワーを生成時に親にするゲームオブジェクト"),Header("タワーをまとめるフォルダー")]
    private GameObject _towerFolder = default;
    [Tooltip("選択されているタワー")]
    private GameObject _selectedTower = default;
    [Tooltip("タワーを設置する座標")]
    private Vector3 _towerPos = default;
    [SerializeField,Tooltip("カーソルに合わさっているタイルを取得")]
    private CursorModel _cursorModel = default;

    #endregion

    #region プロパティ  
    public bool IsShop {
        get => _isShop;
        set => _isShop = value;
    }
    public bool IsShopFirst {
        get => _isShopFirst;
        set => _isShopFirst = value;
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
        _shopTransform = _shopMenu.transform;
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
            //現在のタイルの種類を取得
            int tileNumber = _cursorModel.TileNumber;
            //タイルの種類に合わせた説明画面を表示

            //x座標だけを変更する
            _shopPos.x = _shopTransform.position.x - CONST_SHOP_X;
            _shopPos.y = _shopTransform.position.y;
            //ショップフラグをON
            IsShop = true;
            //メニューを移動させる
            _shopTransform.position = _shopPos;
        }
    }

    /// <summary>
    /// ショップを閉じる
    /// </summary>
    public void ShopClose() {
        //ショップが開かれているなら
        if (IsShop && IsShopFirst) {
            //x座標だけを変更する
            _shopPos.x = _shopTransform.position.x + CONST_SHOP_X;
            _shopPos.y = _shopTransform.position.y;
            //ショップフラグをOFF
            IsShop = false;
            //メニューを移動させる
            _shopTransform.position = _shopPos;
            //一回目のみショップを閉じないようにする
            IsShopFirst = false;
        } else if (!IsShopFirst) {
            //一回目のみショップを閉じないようにする
            IsShopFirst = true;
        }
    }


    /// <summary>
    /// タワーボタンが押されたとき
    /// </summary>
    public void TowerButton(GameObject tower) {
        //押されているボタンに対応するタワーを格納
        _selectedTower = tower;
        //タワー説明、確定ボタンを表示
        
    }

    /// <summary>
    /// タワー購入ボタンを押されたとき
    /// </summary>
    public void DecideToBuy() {
        //もし金額が足りているなら

        //設置する位置を取得
        //タワーを生成
        Instantiate(_selectedTower, _towerPos, Quaternion.identity, _towerFolder.transform);
        //タワーの金額分減らす

        //足りていないなら

        //足りていないと表示する
    }

    #endregion
}
