// ---------------------------------------------------------  
// MapOutputEditor.cs  
// インスペクター拡張用
// 作成日:  3/11
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MapOutput))]
public class MapOutputEditor : Editor {

    #region 変数  

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>
    /// インスペクターにボタンを表示する
    /// </summary>
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        //もしボタンが押されたら
        if (GUILayout.Button("タイルマップを出力")) {
            //MapOutputのOutput処理を実行
            MapOutput mapOutput = (MapOutput)target;
            mapOutput.Output();
        }
    }

    #endregion
}
