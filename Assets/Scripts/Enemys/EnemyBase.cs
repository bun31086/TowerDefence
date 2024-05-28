// ---------------------------------------------------------  
// EnemyBase.cs  
// 敵の基底クラス
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;
using System.Collections;

public class EnemyBase : MonoBehaviour, IDamageable,IMaxHPChange {

    #region 変数  

    [Tooltip("移動時に使う曲がり角の番号")]
    private int _moveNumber = default;
    [SerializeField,Tooltip("敵のHP")]
    protected ReactiveProperty<float> _hp = new ReactiveProperty<float>();
    [Tooltip("プレイヤーのHPオブジェクト"), Header("プレイヤーのステータスオブジェクト")]
    private GameObject _playerStatus = default;
    [SerializeField, Tooltip("敵のデータ"), Header("敵のスクリプタブルオブジェクト")]
    private EnemyData _enemyData = default;
    [SerializeField, Tooltip("敵のスプライト"), Header("敵のスプライトレンダラー")]
    private SpriteRenderer _enemySprite = default;
    [Tooltip("ダメージ時待ち時間")]
    private WaitForSeconds _waitForSecond = new WaitForSeconds(0.05f);

    #endregion
    #region プロパティ
    /// <summary>
    /// 敵のHP
    /// </summary>
    public IReadOnlyReactiveProperty<float> EnemyHP => _hp;
    #endregion
    #region メソッド  

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake() {

        //プレイヤーのステータスを取得
        _playerStatus = GameObject.Find("PlayerStatus");
        //HPを取得
        _hp.Value = _enemyData.Hp;
        //1つ目のポジションに移動
        transform.position = CurvePosition.Instance.CurvePos[0];

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    protected void Update() {
        Move();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move() {

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
        _hp.Value -= damage;
        //もしHPが０より下になったら
        if (_hp.Value <= 0) {
            //金額を増やす
            _playerStatus.GetComponent<IMoneyChange>().MoneyChange(_enemyData.Money);
            //消す
            this.gameObject.SetActive(false);
        } else {
            //敵を赤くする
            StartCoroutine(DamageRed());
        }
    }

    /// <summary>
    /// 最大HPを変更
    /// </summary>
    /// <param name="wave">現在WAVE数</param>
    public void MaxHPChange(int wave) {
        _hp.Value *= (1 + (wave * 0.1f));
    }

    /// <summary>
    /// 被ダメージに敵を赤くする処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageRed() {
        _enemySprite.color = Color.red;
        yield return _waitForSecond;
        _enemySprite.color = Color.white;
        yield return null;
    }

    #endregion
}
