// ---------------------------------------------------------  
// TowerData.cs  
// タワーのデータ
// 作成日:  3/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/CreateTowerData")]
public class TowerData : ScriptableObject
{

    #region 変数  

    [SerializeField, Tooltip("タワー"), Header("タワーオブジェクト")]
    private GameObject _towerObject = default;
    [SerializeField, Tooltip("タワーの弾"), Header("タワーの弾")]
    private GameObject _towerBullet = default;
    [SerializeField, Tooltip("タワーの値段"), Header("タワーの値段")]
    private int _towerMoney = default;
    [SerializeField, Tooltip("タワー名"), Header("タワーの名前")]
    private string _towerName = default;
    [SerializeField, Tooltip("タワー説明"), Header("タワーの説明")]
    private string _towerExplanation = default;
    [SerializeField, Tooltip("タワー索敵範囲"), Header("タワーの索敵範囲")]
    private float _searchRange = default;
    [SerializeField,Tooltip("弾を撃つ時間間隔"),Header("連射スピード")]
    private float _shootTime = default;


    #endregion

    #region プロパティ  
    public GameObject TowerObject {
        get => _towerObject;
    }
    public GameObject TowerBullet {
        get => _towerBullet;
    }
    public int TowerMoney {
        get => _towerMoney;
    }
    public string TowerName {
        get => _towerName;
    }
    public string TowerExplanation {
        get => _towerExplanation;
    }
    public float SearchRange {
        get => _searchRange;
    }
    public float ShootTime {
        get => _shootTime;
    }

    #endregion

}
