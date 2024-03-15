// ---------------------------------------------------------  
// PlayerPresenter.cs  
// ModelとViewの仲介
// 作成日:  3/15
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System;
using UniRx;

public class PlayerPresenter : MonoBehaviour {

    #region 変数  

    [SerializeField, Tooltip("View")]
    private TowerView _playerView = default;

    [SerializeField, Tooltip("Model")]
    private TowerSet _playerModel = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start() {
        //カーソル位置計算
        _playerView.TouchScreenPosition
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
        //カーソル位置移動
        _playerModel.CursorPosition
            .Subscribe(_ => { 

            })
            .AddTo(this);
        //カーソルサイズ変更
        _playerModel.CursorSize
            .Subscribe(_ => {

            })
            .AddTo(this);
            

        //カーソルサイズ変更

    }

    #endregion
}
