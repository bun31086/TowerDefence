// ---------------------------------------------------------  
// TowerBase.cs  
// タワーの基底クラス
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("撃つ弾の種類")] 
    private GameObject _bullet = default;
    [Tooltip("撃つ弾のTransform")]
    private Transform _bulletTransorm = default;
    [Tooltip("一番近い敵")]
    private GameObject _nearestEnemy = default;
    [Tooltip("一番近い敵との距離")]
    private float _distance = 0;
    [Tooltip("タワーのTransform")]
    protected Transform _transform = default;
    [Tooltip("タワーの回転スピード")]
    private const int CONST_ROTATE_SPEED = 3;
    [Tooltip("新しく生成された弾オブジェクト")]
    private GameObject _createdBullet = default;
    [SerializeField,Tooltip("前の弾を撃ってから経過した時間")]
    private float _nowTime = default;
    [Tooltip("弾を撃つ時間間隔")]
    protected float _shootTime = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  


    protected void Update() {
        //一番近い敵を向く
        TowerMove();
        //時間を数える
        _nowTime += Time.deltaTime;
        //ターゲットがいなかったら
        if (_nearestEnemy == null) {
            //時間を初期化
            _nowTime = 0.0f;
            //処理を中断
            return;
        }
        //弾を撃てる時間になったら
        if (_nowTime >= _shootTime) {
            //時間を初期化
            _nowTime = 0.0f;
            //弾を生成
            InstBullet(_transform.position, _transform.rotation);
        }
    }

    /// <summary>
    /// 一番近い敵を向く
    /// </summary>
    private void TowerMove() {
        //全ての敵を配列に格納
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //敵がいなかったら
        if (enemys.Length == 0) {
            //ターゲットを空にする
            _nearestEnemy = null;
            //処理を中断
            return;
        }
        //距離を初期化
        _distance = 100;
        //一番近い敵を探す
        foreach (GameObject enemy in enemys) {
            //タワーと敵の距離を求める
            float distance = Vector2.Distance(transform.position, enemy.transform.position); 
            //現段階で一番近かったら
            if (distance < _distance)
            {
                //最短距離を更新
                _distance = distance;
                //探索中の敵を格納
                _nearestEnemy = enemy;
            }
        }
        //一番近い敵を向く
        Vector3 diff = (_nearestEnemy.transform.position - this.transform.position).normalized;
        Quaternion quaternionTest = new Quaternion(0,0, Quaternion.FromToRotation(Vector3.up, diff).z, Quaternion.FromToRotation(Vector3.up, diff).w);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternionTest, CONST_ROTATE_SPEED);
    }



    /// <summary>
    /// 弾を生成
    /// </summary>
    /// <param name="pos">生成位置</param>
    /// <param name="rotation">生成時の回転</param>
    private void InstBullet(Vector3 pos, Quaternion rotation) {
        //現在の弾の名前を取得
        string objectName = _bullet.gameObject.name + "Pool";
        //True→同じ名前のプールがすでに生成されている
        bool isCreated = false;
        //生成されているプール分繰り返す
        foreach (string poolName in CreatedPool.Instance.PoolList) {
            //もしまだその弾のプールが生成されているなら
            if (objectName == poolName) {
                //生成済みフラグを立てる
                isCreated = true;
            }
        }
        //もし生成済みじゃなかったら
        if (!isCreated) {
            //その弾のプールを生成する
            _bulletTransorm = new GameObject(objectName).transform;
            //生成済みリストに追加
            CreatedPool.Instance.PoolList.Add(objectName);
        }
        //生成されているなら
        else {
            //そのプールに変更
            _bulletTransorm = GameObject.Find(objectName).transform;
        }
        //非アクティブオブジェクトをbulletsの中から探索
        foreach (Transform bulletTransform in _bulletTransorm) {
            if (!bulletTransform.gameObject.activeSelf) {
                //非アクティブなオブジェクトの位置と回転を設定
                bulletTransform.SetPositionAndRotation(pos, rotation);
                //アクティブにする
                bulletTransform.gameObject.SetActive(true);
                //ターゲットを伝える
                bulletTransform.gameObject.GetComponent<BulletBase>().CreatedBullet(_nearestEnemy);
                return;
            }
        }
        //非アクティブオブジェクトがない場合新規生成
        //生成時に_bulletTransormの子オブジェクトにする
        _createdBullet = Instantiate(_bullet, pos, rotation, _bulletTransorm);
        //ターゲットを伝える
        _createdBullet.gameObject.GetComponent<BulletBase>().CreatedBullet(_nearestEnemy);
    }
    #endregion
}
