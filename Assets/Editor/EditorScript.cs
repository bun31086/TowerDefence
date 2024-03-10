using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class EditorScript : EditorWindow
{
    [Tooltip("横")]
    private int _horizontal = 0;
    [Tooltip("縦")]
    private int _vartical = 0;
    [Tooltip("Map管理配列")]
    private int[,] _map = default;


    [MenuItem("Window/MyWindow")]
    public static void ShowWindow()
    {
        EditorScript window = GetWindow<EditorScript>();
        //Window名を変える
        window.titleContent = new GUIContent("TileWindow");
    }

    private void OnGUI()
    {
        var current = Event.current;
        var type = current.type;
        var isMouseDragOrDown = type == EventType.MouseDrag || type == EventType.MouseDown;
        var isLeftButton = current.button == 0;

        if (isMouseDragOrDown && isLeftButton)
        {
            current.Use();
            Debug.LogError("a");
        }
        //　ラベルを太字で追加
        GUILayout.Label("MapSize（縦×横）", EditorStyles.boldLabel);
        //Mapサイズを設定
        _vartical = EditorGUILayout.IntField("縦", _vartical);
        _horizontal = EditorGUILayout.IntField("横", _horizontal);
        //入力された通りの配列を生成
        _map = new int[_horizontal, _vartical];
        //生成された配列の通りのMapサイズにする
        Debug.LogError("b");

    }

    /// <summary>
    /// エディターが開かれているときのみ実行される
    /// </summary>
    private void Update()
    {
    }
}
