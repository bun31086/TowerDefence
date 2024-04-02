// ---------------------------------------------------------  
// WavePresenter.cs  
// WAVE仲介
// 作成日:  3/21
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class WavePresenter : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("View1"), Header("WaveView1")]
    private WaveView _waveView1 = default;
    [SerializeField, Tooltip("View2"), Header("WaveView2")]
    private WaveView _waveView2 = default;
    [SerializeField,Tooltip("Model"),Header("WaveModel")]
    private GameFlow _gameFlow = default;

    #endregion
  
    #region プロパティ  
  
    #endregion
  
    #region メソッド  
  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Awake()
     {
        //ウェーブ数が変化したとき
        _gameFlow.WaveCount
            .Subscribe(waveCount => {
                //画面のウェーブ数表示も変える
                _waveView1.CountChange(waveCount);
                _waveView2.CountChange(waveCount);
            }).AddTo(this);

     }
    
    #endregion
}
