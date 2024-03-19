// ---------------------------------------------------------  
// CursorPresenter.cs  
// カーソルのModelとViewを仲介
// 作成日:  3/15
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class CursorPresenter : MonoBehaviour {

    #region 変数  

    [SerializeField, Tooltip("カーソルビュー"),Header("カーソルオブジェクト(カーソルのView)")]
    private CursorView _cursorView = default;
    [SerializeField, Tooltip("カーソルモデル"), Header("TileMapGrid(カーソルのModel)")]
    private CursorModel _cursorModel = default;
    [SerializeField,Tooltip("ショップメニューオブジェクト"), Header("ショップメニュー画面")]
    private ShopMenu _shopMenu = default;

    #endregion

    #region メソッド 

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start() {
        #region View側
        //カーソル位置計算
        _cursorView.TouchScreenPosition
            //メニューが開かれていないとき
            .Where(_ => _shopMenu.IsShop == false)
            .Subscribe(touchPoint => {
                //座標計算
                _cursorModel.SearchPos(touchPoint);
            })
            .AddTo(this);
        //カーソルサイズ計算
        _cursorView.CursorSize
            .Subscribe(_ => {
                //大きさ計算
                _cursorModel.ScaleChange();
            })
            .AddTo(this);
        #endregion
        #region Model側
        //カーソル位置移動
        _cursorModel.CursorPosition
            //ゲーム開始時のみ処理を行わない
            .Skip(1)
            .Subscribe(cursorPos => {
                //位置変更
                _cursorView.PositionChange(cursorPos);
                //選択しているタイル,カーソルの位置を渡す
                _shopMenu.DataTell(_cursorModel.TileCoordinateCol, _cursorModel.TileCoordinateRow, cursorPos);
                //ショップを開く
                _shopMenu.ShopOpen();
                //閉じるときに押すのが一回のみ
                _shopMenu.IsShopFirst = true;
            })
            .AddTo(this);
        //カーソルサイズ変更
        _cursorModel.CursorSize
            .Subscribe(cursorSize => {
                //サイズ変更
                _cursorView.ScaleChage(cursorSize);
            })
            .AddTo(this);
        #endregion
    }


    #endregion
}
