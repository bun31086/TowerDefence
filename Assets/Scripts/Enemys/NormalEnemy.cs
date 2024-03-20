// ---------------------------------------------------------  
// NormalEnemy.cs  
// 通常の敵
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  

public class NormalEnemy : EnemyBase {
  
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
        _hp = 100;
        ////敵の移動スピードを設定
        //_speed = 2;
        ////敵の攻撃力を設定
        //_power = 1;
        ////敵の所持金を設定
        //_money = 10;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    //void Update() {
    //    //移動

    //}

    #endregion
}
