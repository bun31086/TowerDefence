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
    private GameObject _target = default;
    [Tooltip("ターゲットの位置")]
    private Transform _targetTransform = default;
    [Tooltip("自分のtransform")]
    private Transform _bulletTransform = default;
    [Tooltip("弾の移動スピード")]
    protected float _bulletSpeed = default;
    [Tooltip("弾の攻撃力")]
    protected int _bulletPower = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {

        Move();
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
    /// 敵を追尾する動き
    /// </summary>
    private void Move() {
        //ターゲットがいないなら
        if (_target == null) {
            //処理を中断
            return;
        }
        //ターゲットが消えたら
        if (!_target.activeSelf) {
            //自分も消す
            this.gameObject.SetActive(false);
            //処理を中断する
            return;
        }
        //ターゲットの位置を取得し移動
        _bulletTransform.position = Vector3.MoveTowards(_bulletTransform.position, _targetTransform.position, _bulletSpeed * Time.deltaTime);
        //もしターゲットと位置が重なったら
        if (_bulletTransform.position == _targetTransform.position) {
            //ターゲットにダメージを与える
            _targetTransform.GetComponent<IDamageable>().DamageHit(_bulletPower);
            //自分も消す
            this.gameObject.SetActive(false);
        }
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
