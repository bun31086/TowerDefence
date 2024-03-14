// ---------------------------------------------------------  
// EnemyBase.cs  
// 敵の基底クラス
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour,IDamageable
{

    #region 変数  

    [Tooltip("移動時に使う曲がり角の番号")]
    private int _moveNumber = default;
    [Tooltip("スタート位置に移動させる際に使用するフラグ")]
    private bool _isFirst = true;
    [Tooltip("敵の移動スピード")]
    protected float _speed = default;
    [SerializeField,Tooltip("敵のHP")]
    protected float _hp = default;
    [Tooltip("敵の攻撃力")]
    protected int _power = default;
    [SerializeField,Tooltip("プレイヤーのHPオブジェクト")]
    private GameObject _player = default;

    #endregion
  
    #region プロパティ  
  
    #endregion
  
    #region メソッド  
  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Awake()
     {
     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
  
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     protected void Update ()
     {
        Move();
     }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move() {
        //一回のみ実行
        if (_isFirst) {
            //1つ目のポジションに移動
            transform.position = CurvePosition.Instance.CurvePos[0];
            _isFirst = false;
        }
        //もし敵がゴールしていたら
        if (_moveNumber >= CurvePosition.Instance.CurvePos.Length) {
            //ゴール処理を実行する
            Goal();
            //処理を中断
            return;
        }
        //次の曲がり角まで移動
        transform.position = Vector3.MoveTowards(transform.position, CurvePosition.Instance.CurvePos[_moveNumber], _speed * Time.deltaTime);
        //曲がり角についたら
        if (transform.position == CurvePosition.Instance.CurvePos[_moveNumber]) {
            //次の曲がり角に変更
            _moveNumber++;
        }
    }

    /// <summary>
    /// ゴール処理
    /// </summary>
    private void Goal() {
        //このゲームオブジェクトを非アクティブにする
        this.gameObject.SetActive(false);
        //もしIDamageableを持っていたら
        if (_player.TryGetComponent(out IDamageable damageable)) {
            //PlayerHPのインターフェースを参照し、ダメージを与える
            _player.GetComponent<IDamageable>().DamageHit(_power);
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void DamageHit(int damage) {
        //送られてきたダメージ分、HPを減らす
        _hp -= damage;
        //もしHPが０より下になったら
        if (_hp <= 0) {
            //消す
            this.gameObject.SetActive(false);
        }
    }

    #endregion
}
