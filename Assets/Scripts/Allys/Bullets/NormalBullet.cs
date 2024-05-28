// ---------------------------------------------------------  
// NormalBullet.cs  
// 通常弾の動作処理
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;

public class NormalBullet : BulletBase
{
    #region メソッド  

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update() {

        Move();
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


    #endregion
}
