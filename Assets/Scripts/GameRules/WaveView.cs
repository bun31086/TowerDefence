// ---------------------------------------------------------  
// WaveView.cs  
// ウェーブ数のUI
// 作成日:  3/21
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;

public class WaveView : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("テキストコンポーネント"), Header("テキストコンポーネント")]
    private Text _waveText = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// ウェーブ数変更
    /// </summary>
    /// <param name="waveCount">現在ウェーブ</param>
    public void CountChange(int waveCount) {
        //ウェーブ数を表示
        _waveText.text = waveCount.ToString();
    }
  
    #endregion
}
