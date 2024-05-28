// ---------------------------------------------------------  
// BulletData.cs  
// 弾のデータ
// 作成日:  5/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/CreateBulletData")]
public class BulletData : ScriptableObject {

    #region 変数  

    [SerializeField, Tooltip("弾の名前"), Header("弾の名前")]
    private string _bulletName = default;
    [SerializeField, Tooltip("弾の説明"), Header("弾の説明")]
    private string _bulletExplanation = default;
    [SerializeField, Tooltip("弾の威力"), Header("弾の威力")]
    private int _bulletPower = default;
    [SerializeField, Tooltip("弾の速度"), Header("弾の速度")]
    private int _bulletSpeed = default;



    #endregion

    #region プロパティ  
    /// <summary>
    /// 弾の名前
    /// </summary>
    public string BulletName {
        get => _bulletName;
    }
    /// <summary>
    /// 弾の説明
    /// </summary>
    public string BulletExplanation {
        get => _bulletExplanation;
    }
    /// <summary>
    /// 弾の威力
    /// </summary>
    public int BulletPower {
        get => _bulletPower;
    }
    /// <summary>
    /// 弾の速度
    /// </summary>
    public int BulletSpeed {
        get => _bulletSpeed;
    }

    #endregion

}
