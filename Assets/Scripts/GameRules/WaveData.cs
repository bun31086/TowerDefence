// ---------------------------------------------------------  
// WaveData.cs  
// ウェーブごとに出現する敵を管理
// 作成日:  3/20
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "MyScriptable/CreateWaveData")]
public class WaveData : ScriptableObject
{

    #region 変数  

    [SerializeField, Tooltip("Enemy"), Header("登場する敵")]
    private List<GameObject> _waveEnemy = new List<GameObject>();

    #endregion

    #region プロパティ  
    public List<GameObject> WaveEnemy {
        get => _waveEnemy;
    }

    #endregion

}
