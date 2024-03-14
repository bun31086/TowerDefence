// ---------------------------------------------------------  
// SpeedEnemy.cs  
// 足が早い敵
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  

public class SpeedEnemy : EnemyBase
{
  
    #region 変数  
  
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
        //敵のHPを設定
        _hp = 80;
        //敵の移動スピードを設定
        _speed = 6;
        //敵の攻撃力を設定
        _power = 1;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    //void Update ()
    //{
    //}

    #endregion
}
