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

    [SerializeField, Tooltip("カーソルビュー")]
    private CursorView _playerView = default;
    [SerializeField, Tooltip("カーソルモデル")]
    private CursorModel _playerModel = default;
    [SerializeField]
    private ShopMenu _shopMenu = default;

    #endregion

    #region メソッド 


    //if()
    //処理を中断



    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start() {
        #region View側
        //カーソル位置計算
        _playerView.TouchScreenPosition
            //メニューが開かれていないとき
            .Where(_ => _shopMenu.IsShop == false)
            .Subscribe(touchPoint => {
                //座標計算
                _playerModel.SearchPos(touchPoint);
            })
            .AddTo(this);
        //カーソルサイズ計算
        _playerView.CursorSize
            .Subscribe(_ => {
                //大きさ計算
                _playerModel.ScaleChange();
            })
            .AddTo(this);
        #endregion
        #region Model側
        //カーソル位置移動
        _playerModel.CursorPosition
            .Subscribe(cursorPos => {
                //位置変更
                _playerView.PositionChange(cursorPos);
            })
            .AddTo(this);
        //カーソルサイズ変更
        _playerModel.CursorSize
            .Subscribe(cursorSize => {
                //サイズ変更
                _playerView.ScaleChage(cursorSize);
            })
            .AddTo(this);
        #endregion
    }


    #endregion
}
