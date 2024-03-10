using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class EditorScript : EditorWindow
{
    [Tooltip("��")]
    private int _horizontal = 0;
    [Tooltip("�c")]
    private int _vartical = 0;
    [Tooltip("Map�Ǘ��z��")]
    private int[,] _map = default;


    [MenuItem("Window/MyWindow")]
    public static void ShowWindow()
    {
        EditorScript window = GetWindow<EditorScript>();
        //Window����ς���
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
        //�@���x���𑾎��Œǉ�
        GUILayout.Label("MapSize�i�c�~���j", EditorStyles.boldLabel);
        //Map�T�C�Y��ݒ�
        _vartical = EditorGUILayout.IntField("�c", _vartical);
        _horizontal = EditorGUILayout.IntField("��", _horizontal);
        //���͂��ꂽ�ʂ�̔z��𐶐�
        _map = new int[_horizontal, _vartical];
        //�������ꂽ�z��̒ʂ��Map�T�C�Y�ɂ���
        Debug.LogError("b");

    }

    /// <summary>
    /// �G�f�B�^�[���J����Ă���Ƃ��̂ݎ��s�����
    /// </summary>
    private void Update()
    {
    }
}
