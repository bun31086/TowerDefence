// ---------------------------------------------------------  
// ShopMenu.cs  
// ショップを表示、タワー購入、アップグレード
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    private const float CONST_SHOP_X = 1000;
    [Tooltip("ショップが開かれているか")]
    private bool _isShop = false;
    [Tooltip("ショップのボタンを押されて一回目のみショップを閉じないようにする")]
    private bool _isShopFirst = default;
    [SerializeField, Tooltip("タワーを生成時に親にするゲームオブジェクト"),Header("タワーをまとめるフォルダー")]
    private GameObject _towerFolder = default;
    [Tooltip("選択されているタワー")]
    private GameObject _selectedTower = default;
    [Tooltip("タワーを設置する座標")]
    private Vector2 _towerPos = default;
    [Tooltip("タイルの種類を番号で示したもの")]
    private MapDataEnum _tileData = default;
    [Tooltip("カーソルと重なるタイルの配列座標(列)")]
    private int _tileCoordinateCol = default;
    [Tooltip("カーソルと重なるタイルの配列座標(行)")]
    private int _tileCoordinateRow = default;
    [SerializeField, Tooltip("タイル名テキスト")]
    private Text _tileName = default; 
    [SerializeField, Tooltip("タイル説明テキスト")]
    private Text _tileExplanation = default;
    [SerializeField,Tooltip("タワー名テキスト")]
    private Text _towerNameText = default;
    [SerializeField, Tooltip("タワー説明テキスト")]
    private Text _towerExplanationText = default;
    [SerializeField, Tooltip("タワー金額テキスト")]
    private Text _towerMoneyText = default;
    [SerializeField,Tooltip("購入ボタンやテキスト")]
    private GameObject _buyObjects = default;
    [SerializeField, Tooltip("タワーボタン")]
    private GameObject _towerObjects = default;
    [SerializeField, Tooltip("プレイヤーのHPオブジェクト"), Header("プレイヤーのステータスオブジェクト")]
    private GameObject _playerStatusObject = default;
    [Tooltip("選択しているタワーの金額")]
    private int _towerMoney = default;
    [Tooltip("選択しているタワーの名前")]
    private string _towerName = default;
    [Tooltip("選択しているタワーの説明")]
    private string _towerExplanation = default;
    [SerializeField, Tooltip("タワーの説明等スクリプタブル"), Header("タワースクリプタブルオブジェクト")]
    private TowerData[] _towerData = default;
    [Tooltip("プレイヤーステータス")]
    private PlayerStatus _playerStatus = default;

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
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        _shopTransform = _shopMenu.transform;
        _playerStatus = _playerStatusObject.GetComponent<PlayerStatus>();
     }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
     {
     }

    /// <summary>
    /// タイルナンバーを受け取る
    /// </summary>
    /// <param name="tileCoordinate">タイルの配列座標</param>
    /// <param name="towerPos">タワーの設置座標</param>
    public void DataTell(int tileCoordinateCol,int tileCoordinateRow,Vector2 towerPos) {
        //カーソルと重なるタイルの配列座標を取得
        _tileCoordinateCol = tileCoordinateCol;
        _tileCoordinateRow = tileCoordinateRow;
        //現在のタイルの種類を取得
        _tileData = MapData.Instance.MapDataArray[_tileCoordinateCol, _tileCoordinateRow];
        //タワーを設置する位置を取得
        _towerPos = towerPos;
    }

    /// <summary>
    /// ショップを開く
    /// </summary>
    public void ShopOpen() {
        //ショップが開かれていないなら
        if (!IsShop) {
            //タイルの種類に合わせた説明画面を表示
            TextChange();
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
    /// 説明の文字を変更する
    /// </summary>
    private void TextChange() {
        //タイルの種類に合わせた説明画面を表示
        switch (_tileData) {
            case MapDataEnum.Platform:     //設置可能場所
                _tileName.text = "プラットフォーム";
                _tileExplanation.text = "タワー設置可能エリア";
                //タワー選択ボタンを表示
                _towerObjects.SetActive(true);
                break;
            case MapDataEnum.Road:        //道
                _tileName.text = "道";
                _tileExplanation.text = "敵が通る経路";
                //タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                //タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                break;
            case MapDataEnum.Start:    //スタート
                //タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                //タワー選択ボタンを未表示
                _towerObjects.SetActive(false);

                break;
            case MapDataEnum.Goal:    //ゴール
                //タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                //タワー選択ボタンを未表示
                _towerObjects.SetActive(false);

                break;
            case MapDataEnum.Tower:      //タワー
                _tileName.text = "タワー";
                _tileExplanation.text = "味方のタワー";
                //タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                //タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                break;
        }

    }


    /// <summary>
    /// タワーボタンが押されたとき
    /// </summary>
    public void TowerButton(GameObject tower) {
        //押されているボタンに対応するタワーを格納
        _selectedTower = tower;
        //アタッチされているスクリプタブルオブジェクトの数、繰り返す
        foreach (TowerData towerScriptable in _towerData) {
            //選択されているタワーとスクリプタブルオブジェクトのタワーが同じだったら
            if (_selectedTower == towerScriptable.TowerObject) {
                //そのタワーの金額を取得
                _towerMoney = towerScriptable.TowerMoney;
                //そのタワーの名前を取得
                _towerName = towerScriptable.TowerName;
                //そのタワーの説明を取得
                _towerExplanation = towerScriptable.TowerExplanation;
                //Foreachを終了する
                break;
            }
        }
        //タワー説明、確定ボタンを表示
        _towerMoneyText.text = _towerMoney + "円";
        _towerNameText.text = _towerName;
        _towerExplanationText.text = _towerExplanation;
        //タワー購入ボタンなどを表示
        _buyObjects.SetActive(true);
    }

    /// <summary>
    /// タワー購入ボタンを押されたとき
    /// </summary>
    public void DecideToBuy() {
        //もし金額が足りているなら
        if (_playerStatus.PlayerMoney >= _towerMoney) {
            //タワーを生成
            Instantiate(_selectedTower, _towerPos, Quaternion.identity, _towerFolder.transform);
            //配列を変更
            MapData.Instance.MapDataArray[_tileCoordinateCol, _tileCoordinateRow] = MapDataEnum.Tower;
            //タワーの金額分減らす
            _playerStatusObject.GetComponent<IMoneyAdd>().MoneyGet(-_towerMoney);
            //メニューを閉じる
            ShopClose();
            DataTell(_tileCoordinateCol, _tileCoordinateRow, _towerPos);
            TextChange();
        }
        //足りていないなら
        else {
            //足りていないと表示する
            Debug.LogError("足りない");
        }
    }

    #endregion
}
