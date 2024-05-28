// ---------------------------------------------------------  
// BulletBase.cs  
// 弾のベース処理
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
public class BulletBase : MonoBehaviour
{

    #region 変数  

    [Tooltip("狙っている敵")]
    protected GameObject _target = default;
    [Tooltip("ターゲットの位置")]
    protected Transform _targetTransform = default;
    [Tooltip("自分のtransform")]
    protected Transform _bulletTransform = default;
    [Tooltip("弾の移動スピード")]
    protected float _bulletSpeed = default;
    [Tooltip("弾の攻撃力")]
    protected int _bulletPower = default;
    [SerializeField, Tooltip("弾のデータ")]
    private BulletData _bulletData = default;



    #endregion

    #region メソッド  


    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start() {
        //弾のスピードを設定
        _bulletSpeed = _bulletData.BulletSpeed;
        //弾の攻撃力を設定
        _bulletPower = _bulletData.BulletPower;
    }

    /// <summary>
    /// 生成時にターゲットを渡される
    /// </summary>
    /// <param name="target">狙っている敵</param>
    public void CreatedBullet(GameObject target) {
        //ターゲットが無ければ
        if (target == null) {
            //自分も消す
            this.gameObject.SetActive(false);
            //処理を中断
            return;
        }
        //ターゲットを取得
        _target = target;
        _targetTransform = _target.transform;
        _bulletTransform = this.transform;
    }

    /// <summary>
    /// カメラの外に行ったら非アクティブ
    /// </summary>
    private void OnBecameInvisible() {
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }
    #endregion
}
