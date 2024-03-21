// ---------------------------------------------------------  
// PlayerStatusPresenter.cs  
// プレイヤーのステータスとUIをつなぐ
// 作成日:  3/21
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class PlayerStatusPresenter : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("プレイヤーのUI"), Header("プレイヤーUI")]
    private PlayerUI _playerUI = default;
    [SerializeField, Tooltip("プレイヤーのステータス"), Header("プレイヤーステータス")]
    private PlayerStatus _playerStatus = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {
        //プレイヤーのHPが変更されたとき
        _playerStatus.PlayerHP
            .Subscribe(hp => {
                //UIを変更
                _playerUI.HPChange(hp);
            }).AddTo(this);
        //プレイヤーの所持金が変更されたとき
        _playerStatus.PlayerMoney
            .Subscribe(money => {
                //UIを変更
                _playerUI.MoneyChange(money);
            }).AddTo(this);
     }
    
    #endregion
}
