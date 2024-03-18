// ---------------------------------------------------------  
// NormalTower.cs  
// 通常のタワー
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  

public class NormalTower : TowerBase
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
    void Start() {
        _transform = this.transform;
        //３秒おきに弾を撃つ
        _shootTime = 1;
    }
    ///// <summary>  
    ///// 更新処理  
    ///// </summary>  
    //void Update ()
    //{
    //}

    #endregion
}
