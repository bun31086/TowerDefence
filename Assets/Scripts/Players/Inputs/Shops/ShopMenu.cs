// ---------------------------------------------------------  
// ShopMenu.cs  
// ショップを表示、タワー購入、アップグレード
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class ShopMenu : MonoBehaviour {

    #region 変数  

    [SerializeField, Tooltip("タワーの説明等スクリプタブル"), Header("タワースクリプタブルオブジェクト")]
    private TowerData[] _towerData = default;
    [SerializeField, Tooltip("ショップメニュー")]
    private GameObject _shopMenu = default;
    [Tooltip("ショップメニューTransform")]
    private Transform _shopTransform = default;
    [Tooltip("メニューの座標")]
    private Vector2 _shopPos = default;
    [Tooltip("ショップが開かれているか")]
    private BoolReactiveProperty _isShop = new BoolReactiveProperty();
    [Tooltip("ショップのボタンを押されて一回目のみショップを閉じないようにする")]
    private bool _isShopFirst = default;
    [SerializeField, Tooltip("タワーを生成時に親にするゲームオブジェクト")]
    private GameObject _towerFolder = default;
    [Tooltip("選択されているタワー")]
    private GameObject _selectedTower = default;
    [Tooltip("タワーを設置する座標")]
    private Vector2 _towerPos = default;
    [Tooltip("タイルの種類を番号で示したもの")]
    private MapType _tileData = default;
    [Tooltip("カーソルと重なるタイルの配列座標(列)")]
    private int _tileCoordinateCol = default;
    [Tooltip("カーソルと重なるタイルの配列座標(行)")]
    private int _tileCoordinateRow = default;
    [SerializeField, Tooltip("タイル名テキスト")]
    private Text _tileName = default;
    [SerializeField, Tooltip("タイル説明テキスト")]
    private Text _tileExplanation = default;
    [SerializeField, Tooltip("タワー名テキスト")]
    private Text _towerNameText = default;
    [SerializeField, Tooltip("タワー説明テキスト")]
    private Text _towerExplanationText = default;
    [SerializeField, Tooltip("タワー金額テキスト")]
    private Text _towerMoneyText = default;
    [SerializeField, Tooltip("購入ボタンやテキスト")]
    private GameObject _buyObjects = default;
    [SerializeField, Tooltip("選択ボタンフォルダー")]
    private GameObject _selectButtonFolder = default;
    [SerializeField, Tooltip("タワーボタン")]
    private GameObject _towerObjects = default;
    [SerializeField, Tooltip("バレットボタン")]
    private GameObject _bulletObjects = default;
    [SerializeField, Tooltip("購入ボタン")]
    private GameObject _buyButton = default;
    [SerializeField, Tooltip("プレイヤーのHPオブジェクト")]
    private GameObject _playerStatusObject = default;
    [Tooltip("選択しているタワーの金額")]
    private int _towerMoney = default;
    [Tooltip("選択しているタワーの名前")]
    private string _towerName = default;
    [Tooltip("選択しているタワーの説明")]
    private string _towerExplanation = default;
    [Tooltip("選択しているタワーの射撃範囲")]
    private FloatReactiveProperty _towerRange = new FloatReactiveProperty();
    [Tooltip("プレイヤーステータス")]
    private PlayerStatus _playerStatus = default;
    [SerializeField, Tooltip("タワーの選択ボタン")]
    private Image _towerButtonImage = default;
    [SerializeField, Tooltip("バレットの選択ボタン")]
    private Image _bulletButtonImage = default;
    [SerializeField, Tooltip("タワーのステータスフォルダー")]
    private GameObject _towerStatesFolder = default;
    [SerializeField, Tooltip("バレットのステータスフォルダー")]
    private GameObject _bulletStatesFolder = default;
    [SerializeField, Tooltip("バレット名テキスト")]
    private Text _bulletNameText = default;
    [SerializeField, Tooltip("連射速度テキスト")]
    private Text _rateOfFireText = default;
    [SerializeField, Tooltip("攻撃範囲テキスト")]
    private Text _attackRangeText = default;
    [SerializeField, Tooltip("威力テキスト")]
    private Text _powerText = default;
    [SerializeField, Tooltip("弾速テキスト")]
    private Text _bulletSpeedText = default;    
    [SerializeField, Tooltip("通常弾データ")]
    private BulletData _normalBulletData = default;    
    [SerializeField, Tooltip("高威力データ")]
    private BulletData _highPowerBulletData = default;

    #region 定数

    [Tooltip("ずらすX座標")]
    private const float CONST_SHOP_X = 1000;
    private const string TOWER_NAME = "Tower";
    private const string BULLET_NAME = "Bullet";
    private const string NORMAL_BULLET_NAME = "NormalBullet";
    private const string STRONG_BULLET_NAME = "StrongBullet";

    #endregion

    #endregion

    #region プロパティ  
    public bool IsShopFirst {
        get => _isShopFirst;
        set => _isShopFirst = value;
    }
    public IReadOnlyReactiveProperty<float> TowerRange => _towerRange;

    public ReactiveProperty<bool> IsShop => _isShop;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start() {
        _shopTransform = _shopMenu.transform;
        _playerStatus = _playerStatusObject.GetComponent<PlayerStatus>();
        _tileData = MapType.Wood;

    }

    /// <summary>
    /// タイルナンバーを受け取る
    /// </summary>
    /// <param name="tileCoordinate">タイルの配列座標</param>
    /// <param name="towerPos">タワーの設置座標</param>
    public void DataTell(int tileCoordinateCol, int tileCoordinateRow, Vector2 towerPos) {
        // カーソルと重なるタイルの配列座標を取得
        _tileCoordinateCol = tileCoordinateCol;
        _tileCoordinateRow = tileCoordinateRow;
        // 現在のタイルの種類を取得
        _tileData = MapData.Instance.MapDataArray[_tileCoordinateCol, _tileCoordinateRow];
        // タワーを設置する位置を取得
        _towerPos = towerPos;
    }

    /// <summary>
    /// ショップを開く
    /// </summary>
    public void ShopOpen() {
        // ショップが開かれていないなら
        if (!IsShop.Value) {
            // タイルの種類に合わせた説明画面を表示
            TextChange();
            // x座標だけを変更する
            _shopPos.x = _shopTransform.position.x - CONST_SHOP_X;
            _shopPos.y = _shopTransform.position.y;
            // ショップフラグをON
            IsShop.Value = true;
            // メニューを移動させる
            _shopTransform.position = _shopPos;
        }
    }

    /// <summary>
    /// ショップを閉じる
    /// </summary>
    public void ShopClose() {
        // ショップが開かれているなら
        if (IsShop.Value && IsShopFirst) {
            // x座標だけを変更する
            _shopPos.x = _shopTransform.position.x + CONST_SHOP_X;
            _shopPos.y = _shopTransform.position.y;
            // ショップフラグをOFF
            IsShop.Value = false;
            // メニューを移動させる
            _shopTransform.position = _shopPos;
            // 一回目のみショップを閉じないようにする
            IsShopFirst = false;
            // タワー購入ボタンなどを表示
            _buyObjects.SetActive(false);
            // タワーのステータスを非表示
            _towerStatesFolder.SetActive(false);
            // バレットのステータスを非表示
            _bulletStatesFolder.SetActive(false);
        } else if (!IsShopFirst) {
            // 一回目のみショップを閉じないようにする
            IsShopFirst = true;
        }
    }

    /// <summary>
    /// 説明の文字を変更する
    /// </summary>
    private void TextChange() {
        // タイルの種類に合わせた説明画面を表示
        switch (_tileData) {
            case MapType.Platform:     // 設置可能場所
                _tileName.text = "プラットフォーム";
                _tileExplanation.text = "タワー設置可能エリア";
                // タワー選択ボタンを表示
                _towerObjects.SetActive(true);
                // 購入ボタンを表示
                _buyButton.SetActive(true);
                // 選択ボタンを非表示
                _selectButtonFolder.SetActive(true);
                break;
            case MapType.Road:        // 道
                _tileName.text = "道";
                _tileExplanation.text = "敵が通る経路";
                // タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                // タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                // 選択ボタンを非表示
                _selectButtonFolder.SetActive(false);
                break;
            case MapType.Start:    // スタート
                // タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                // タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                // 選択ボタンを非表示
                _selectButtonFolder.SetActive(false);
                break;
            case MapType.Goal:    // ゴール
                // タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                // タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                // 選択ボタンを非表示
                _selectButtonFolder.SetActive(false);
                break;
            case MapType.Tower:      // タワー
                _tileName.text = "タワー";
                _tileExplanation.text = "味方のタワー";
                // タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                // タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                // 選択ボタンを非表示
                _selectButtonFolder.SetActive(false);
                break;
            case MapType.Wood:      // 木
                _tileName.text = "木";
                _tileExplanation.text = "木";
                // タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                // タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                // 選択ボタンを非表示
                _selectButtonFolder.SetActive(false);
                break;
            case MapType.Stone:     // 石
                _tileName.text = "石";
                _tileExplanation.text = "石";
                // タワー選択ボタンを未表示
                _towerObjects.SetActive(false);
                // タワー購入ボタンなどを未表示
                _buyObjects.SetActive(false);
                // 選択ボタンを非表示
                _selectButtonFolder.SetActive(false);
                break;
        }

    }


    /// <summary>
    /// タワーボタンが押されたとき
    /// </summary>
    public void TowerButton(GameObject tower) {
        // 押されているボタンに対応するタワーを格納
        _selectedTower = tower;
        // アタッチされているスクリプタブルオブジェクトの数、繰り返す
        foreach (TowerData towerScriptable in _towerData) {
            // 選択されているタワーとスクリプタブルオブジェクトのタワーが同じだったら
            if (_selectedTower == towerScriptable.TowerObject) {
                // そのタワーの金額を取得
                _towerMoney = towerScriptable.TowerMoney;
                // そのタワーの名前を取得
                _towerName = towerScriptable.TowerName;
                // そのタワーの説明を取得
                _towerExplanation = towerScriptable.TowerExplanation;
                // そのタワーの射撃範囲を取得
                _towerRange.Value = towerScriptable.SearchRange;
                // そのタワーのバレット名を取得
                _bulletNameText.text = towerScriptable.TowerBullet.name;
                // そのタワーの連射速度を取得
                _rateOfFireText.text = towerScriptable.ShootTime.ToString();
                // そのタワーの攻撃範囲を取得
                _attackRangeText.text = towerScriptable.SearchRange.ToString();
                // Foreachを終了する
                break;
            }
        }
        // タワーのステータスを表示
        _towerStatesFolder.SetActive(true);
        // バレットのステータスを非表示
        _bulletStatesFolder.SetActive(false);
        // タワー説明、確定ボタンを表示
        _towerMoneyText.text = _towerMoney + "コイン";
        _towerNameText.text = _towerName;
        _towerExplanationText.text = _towerExplanation;
        // タワー購入ボタンなどを表示
        _buyObjects.SetActive(true);
        // 購入ボタン表示
        _buyButton.SetActive(true);
    }

    /// <summary>
    /// バレットボタンが押されたとき
    /// </summary>
    public void BulletButton(GameObject bullet) {
        // バレット名
        string bulletName = default;
        // バレット説明
        string bulletExplanation = default;
        // バレット威力
        int bulletPower = default;
        // バレット弾速
        int bulletSpeed = default;
        // 弾の種類によって変える
        switch (bullet.name) {
            case NORMAL_BULLET_NAME:
                bulletName = _normalBulletData.BulletName;
                bulletExplanation = _normalBulletData.BulletExplanation;
                bulletPower = _normalBulletData.BulletPower;
                bulletSpeed = _normalBulletData.BulletSpeed;
                break;
            case STRONG_BULLET_NAME:
                bulletName = _highPowerBulletData.BulletName;
                bulletExplanation = _highPowerBulletData.BulletExplanation;
                bulletPower = _highPowerBulletData.BulletPower;
                bulletSpeed = _highPowerBulletData.BulletSpeed;
                break;
        }
        // そのバレットの威力を取得
        _powerText.text = bulletPower.ToString();
        // そのバレットの弾速を取得
        _bulletSpeedText.text = bulletSpeed.ToString();
        // タワーのステータスを非表示
        _towerStatesFolder.SetActive(false);
        // バレットのステータスを表示
        _bulletStatesFolder.SetActive(true);
        // そのバレットの名前を取得
        _towerName = bulletName;
        // そのバレットの説明を取得
        _towerExplanation = bulletExplanation;
        // それぞれ変更する
        _towerNameText.text = _towerName;
        _towerExplanationText.text = _towerExplanation;
        // タワー購入ボタンなどを表示
        _buyObjects.SetActive(true);
        // 購入ボタン非表示
        _buyButton.SetActive(false);
    }

    /// <summary>
    /// タワー購入ボタンを押されたとき
    /// </summary>
    public void DecideToBuy() {
        // もし金額が足りているなら
        if (_playerStatus.PlayerMoney.Value >= _towerMoney) {
            // タワーを生成
            Instantiate(_selectedTower, _towerPos, Quaternion.identity, _towerFolder.transform);
            // 配列を変更
            MapData.Instance.MapDataArray[_tileCoordinateCol, _tileCoordinateRow] = MapType.Tower;
            // タワーの金額分減らす
            _playerStatusObject.GetComponent<IMoneyChange>().MoneyChange(-_towerMoney);
            // メニューを閉じる
            ShopClose();
            DataTell(_tileCoordinateCol, _tileCoordinateRow, _towerPos);
            TextChange();
        }
    }

    /// <summary>
    /// タワーとバレットどちらを表示するか選択
    /// </summary>
    public void SelectButton(string type) {
        // Enumによって、変更
        switch (type) {
            case TOWER_NAME:
                // タワーボタンの色を暗くする
                _towerButtonImage.color = Color.gray;
                // バレットボタンの色を明るくする
                _bulletButtonImage.color = Color.white;
                // タワーのボタンを表示
                _towerObjects.SetActive(true);
                // 弾のボタンを非表示
                _bulletObjects.SetActive(false);
                break;
            case BULLET_NAME:
                // タワーボタンの色を明るくする
                _towerButtonImage.color = Color.white;
                // バレットボタンの色を暗くする
                _bulletButtonImage.color = Color.gray;
                // タワーのボタンを非表示
                _towerObjects.SetActive(false);
                // 弾のボタンを表示
                _bulletObjects.SetActive(true);
                break;
        }
        // タワー説明、確定ボタンを表示
        _towerMoneyText.text = default;
        _towerNameText.text = default;
        _towerExplanationText.text = default;
        // タワー購入ボタンなどを表示
        _buyObjects.SetActive(false);
        // タワーのステータスを非表示
        _towerStatesFolder.SetActive(false);
        // バレットのステータスを非表示
        _bulletStatesFolder.SetActive(false);
    }

    #endregion
}
