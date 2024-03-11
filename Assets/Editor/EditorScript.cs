using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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

    private Tilemap _tileMap = default;
    private Tile[] _tiles =new Tile[3];
    private Tile _tile = default;
    private List<Tile> _tilemapssss = new List<Tile>();

    public SerializedObject serializedObject;


    [MenuItem("Window/MyWindow")]
    public static void ShowWindow()
    {
        EditorScript window = GetWindow<EditorScript>();
        //Window名を変える
        window.titleContent = new GUIContent("TileWindow");
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
    }

    private void OnGUI()
    {
        /*
        var current = Event.current;
        var type = current.type;
        var isMouseDragOrDown = type == EventType.MouseDrag || type == EventType.MouseDown;
        var isLeftButton = current.button == 0;

        if (isMouseDragOrDown && isLeftButton)
        {
            current.Use();
        }
        */
        //　ラベルを太字で追加
        GUILayout.Label("MapSize（縦×横）", EditorStyles.boldLabel);
        //Mapサイズを設定
        _vartical = EditorGUILayout.IntField("縦", _vartical);
        _horizontal = EditorGUILayout.IntField("横", _horizontal);
        //入力された通りの配列を生成
        _map = new int[_horizontal, _vartical];
        //生成された配列の通りのMapサイズにする

        _tileMap = (Tilemap)EditorGUILayout.ObjectField("Tilemap", _tileMap, typeof(Tilemap), true);
        _tile = (Tile)EditorGUILayout.ObjectField("Tile", _tile, typeof(Tile), true);
        _tiles[0] = (Tile)EditorGUILayout.ObjectField("NormalTile→" + 0, _tiles[0], typeof(Tile), true) ;
        _tiles[1] = (Tile)EditorGUILayout.ObjectField("RoadTile", _tiles[1], typeof(Tile), true);
        _tiles[2] = (Tile)EditorGUILayout.ObjectField("TowerSetTile", _tiles[2], typeof(Tile), true);

        //EditorGUILayout.PropertyField(serializedObject.FindProperty($"{nameof(_tilemapssss)}"));

        //serializedObject.ApplyModifiedProperties();

    }

    /// <summary>
    /// エディターが開かれているときのみ実行される
    /// </summary>
    private void Update()
    {
    }
}
