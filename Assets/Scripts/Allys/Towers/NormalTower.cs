// ---------------------------------------------------------  
// NormalTower.cs  
// 通常のタワー
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  

public class NormalTower : TowerBase
{ 
    #region メソッド  
  
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start() {
        _transform = this.transform;
        //１秒おきに弾を撃つ
       // _shootTime = 1;
    }

    #endregion
}
