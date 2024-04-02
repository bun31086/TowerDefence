// ---------------------------------------------------------  
// CursorView.cs  
// カーソルの変更
// 作成日:  3/15
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;

public class CursorView : MonoBehaviour {

    #region 変数  

    [Tooltip("transformコンポーネント")]
    private Transform _transform = default;
    [Tooltip("マウスが押されたスクリーン座標")]
    private readonly Vector2ReactiveProperty _touchScreenPosition = new Vector2ReactiveProperty();
    [Tooltip("カーソルの大きさ変更通知")]
    private readonly Vector2ReactiveProperty _cursorSize = new Vector2ReactiveProperty();

    #endregion

    #region プロパティ
    public IReadOnlyReactiveProperty<Vector2> TouchScreenPosition => _touchScreenPosition;
    public IReadOnlyReactiveProperty<Vector2> CursorSize => _cursorSize;

    #endregion

    #region メソッド 


    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start() {
        _transform = this.transform;
        //カーソルの大きさを変更
        _cursorSize.Value = this.transform.localScale;
    }



    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update() {
        //もしUI関連が押されたら
        if (EventSystem.current.IsPointerOverGameObject()) {
            //処理を中断
            return;
        }
        //もしマウスの右クリックが押されたら
        if (Input.GetMouseButtonDown(0)) {
            //スクリーン座標を格納する
            _touchScreenPosition.Value = Input.mousePosition;
        }
    }

    /// <summary>
    /// オブジェクトの座標変更
    /// </summary>
    /// <param name="cursorPos">座標</param>
    public void PositionChange(Vector2 cursorPos) {
        if (_transform != null) {
            //カーソルのポジションを変更
            _transform.position = cursorPos;
        }
    }

    /// <summary>
    /// オブジェクトのサイズ変更
    /// </summary>
    /// <param name="cursorSize">サイズ</param>
    public void ScaleChage(Vector2 cursorSize) {
        if (_transform != null) {
            //サイズ変更
            _transform.localScale = cursorSize;
        }
    }

    #endregion
}
