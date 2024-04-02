// ---------------------------------------------------------  
// ButtonManager.cs  
// ボタン管理
// 作成日:  3/21
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("ポーズメニュー"), Header("ポーズメニューオブジェクト")]
    private GameObject _poseObject = default;
    [SerializeField, Tooltip("ステージのUI"), Header("ステージのUIオブジェクト")]
    private GameObject _stageUIObject = default;
    [Tooltip("何倍速か")]
    private int _scaleInt = default;

    /// <summary>
    /// 現在の倍速段階
    /// </summary>
    private enum ScaleType {
    One,
    Two,
    Four,
    }
    private ScaleType _scaleType = default;

    #endregion
    
    #region メソッド  
    
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        //倍速を初期化
        _scaleType = ScaleType.One;
     }
  

    /// <summary>
    /// 倍速ボタン
    /// </summary>
    public void ScaleChange() {
        //現在の倍速スピードが
        switch (_scaleType) {
            //普通だったら
            case ScaleType.One:
                //スピード変更
                _scaleInt = 2;
                //状態変更
                _scaleType = ScaleType.Two;
                break;
            //2倍だったら
            case ScaleType.Two:
                //スピード変更
                _scaleInt = 4;
                //状態変更
                _scaleType = ScaleType.Four;
                break;
            //4倍だったら
            case ScaleType.Four:
                //スピード変更
                _scaleInt = 1;
                //状態変更
                _scaleType = ScaleType.One;
                break;
        }
        //速度変更する
        Time.timeScale = _scaleInt;
    }

    /// <summary>
    /// ポーズボタンが押されたとき
    /// </summary>
    public void PoseMenu() {
        //時間を停止
        Time.timeScale = 0;
        //ポーズオブジェを表示
        _poseObject.SetActive(true);
        //ほかのボタンを非表示
        _stageUIObject.SetActive(false);
    }

    /// <summary>
    /// ゲームに戻るボタンが押されたとき
    /// </summary>
    public void ReturnGame() {
        //時間を戻す
        Time.timeScale = _scaleInt;
        //ポーズオブジェを非表示
        _poseObject.SetActive(false);
        //ほかのボタンを表示
        _stageUIObject.SetActive(true);
    }

    /// <summary>
    /// タイトルに戻るボタンが押されたとき
    /// </summary>
    public void BackTitle() {
        //タイトルシーンに移動
        SceneManager.LoadScene("TitleScene");
    }


    /// <summary>
    /// ゲーム開始ボタン
    /// </summary>
    public void GameStart() {
        //ゲームシーンに移動
        SceneManager.LoadScene("StageScene");
    }

    #endregion
}
