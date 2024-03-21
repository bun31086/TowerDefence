// ---------------------------------------------------------  
// ShootRange.cs  
// タワーの射撃範囲を表示
// 作成日:  3/21
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;

public class ShootRangeView : MonoBehaviour
{

    #region 変数  

    private Transform _transform = default;

    #endregion

    #region メソッド  

  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Awake ()
     {
        _transform = this.transform;
     }

    /// <summary>
    /// 射撃範囲表示サイズ変更
    /// </summary>
    /// <param name="size">大きさ</param>
    public void SizeChange(float size) {
        //サイズ変更
        _transform.localScale = new Vector2(size, size);
    }
    /// <summary>
    /// 射撃範囲表示表示変更
    /// </summary>
    /// <param name="isDisplay">表示するか</param>
    public void DisplaySwitch(bool isDisplay) {
        //表示変更
        this.gameObject.SetActive(isDisplay);
    }

    #endregion
}
