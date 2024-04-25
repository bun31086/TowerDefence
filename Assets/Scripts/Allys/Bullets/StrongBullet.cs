// ---------------------------------------------------------  
// StrongBullet.cs  
// 攻撃力が高い弾の処理
// 作成日:  3/14
// 作成者:  竹村綾人
// ---------------------------------------------------------  

public class StrongBullet : BulletBase
{
    #region メソッド  
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     private void Start ()
     {
        //弾のスピードを設定
        _bulletSpeed = 8;
        //弾の攻撃力を設定
        _bulletPower = 20;
    }

    #endregion
}
