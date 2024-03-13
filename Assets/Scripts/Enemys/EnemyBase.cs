// ---------------------------------------------------------  
// EnemyBase.cs  
// 敵の基底クラス
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("曲がり角の座標を所持しているスクリプト")]
    private MapOutput _mapOutput = default;
    [Tooltip("移動時に使う曲がり角の番号")]
    private int _moveNumber = default;
    [Tooltip("スタート位置に移動させる際に使用するフラグ")]
    private bool _isFirst = true;
    [Tooltip("敵の移動スピード")]
    protected float _speed = 0;
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
     void Update ()
     {
     }

    protected void Move() {
        //一回のみ実行
        if (_isFirst) {
            //1つ目のポジションに移動
            transform.position = _mapOutput.CurvePos[0];
            _isFirst = false;
        }
        //もし曲がり角の数より多く曲がろうとしていたら
        if (_moveNumber >= _mapOutput.CurvePos.Length) {
            //処理を中断
            return;
        }
        //次の曲がり角まで移動
        transform.position = Vector3.MoveTowards(transform.position, _mapOutput.CurvePos[_moveNumber], _speed * Time.deltaTime);
        //曲がり角についたら
        if (transform.position == _mapOutput.CurvePos[_moveNumber]) {
            //次の曲がり角に変更
            _moveNumber++;
        }
    }

    #endregion
}
