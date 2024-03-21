// ---------------------------------------------------------  
// EnemyBase.cs  
// 敵の基底クラス
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour,IDamageable
{

    #region 変数  

    [Tooltip("移動時に使う曲がり角の番号")]
    private int _moveNumber = default;
    [Tooltip("スタート位置に移動させる際に使用するフラグ")]
    private bool _isFirst = true;
    [Tooltip("敵のHP")]
    protected float _hp = default;
    //[Tooltip("敵の移動スピード")]
    //protected float _speed = default;
    //[Tooltip("敵の攻撃力")]
    //protected int _power = default;
    //[Tooltip("敵の所持金")]
    //protected int _money = default;
    [Tooltip("プレイヤーのHPオブジェクト"), Header("プレイヤーのステータスオブジェクト")]
    private GameObject _playerStatus = default;
    [SerializeField,Tooltip("敵のデータ"), Header("敵のスクリプタブルオブジェクト")]
    private EnemyData _enemyData = default;
    [SerializeField,Tooltip("敵のスプライト"),Header("敵のスプライトレンダラー")]
    private SpriteRenderer _enemySprite = default;

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
            _playerStatus = GameObject.Find("PlayerStatus");
            //HPを取得
            _hp = _enemyData.Hp;
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
        //前の座標を格納
        Vector3 beforePos = transform.position;
        //次の曲がり角まで移動
        transform.position = Vector3.MoveTowards(transform.position, CurvePosition.Instance.CurvePos[_moveNumber], _enemyData.Speed * Time.deltaTime);
        //もし右に進んだいたら
        if (beforePos.x < transform.position.x) {
            //スプライトを反転させる
            _enemySprite.flipX = true;
        }
        //左に進んでいたら
        else if (beforePos.x > transform.position.x) {
            //スプライトを戻す
            _enemySprite.flipX = false;
        }
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
        if (_playerStatus.TryGetComponent(out IDamageable damageable)) {
            //PlayerHPのインターフェースを参照し、ダメージを与える
            _playerStatus.GetComponent<IDamageable>().DamageHit(_enemyData.Power);
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
            //金額を増やす
            _playerStatus.GetComponent<IMoneyAdd>().MoneyGet(_enemyData.Money); 
            //消す
            this.gameObject.SetActive(false);
        }
    }

    #endregion
}
