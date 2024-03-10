using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ClickCheck : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        //�}�E�X�̃N���b�N���������珈��
        if (Event.current == null || Event.current.type != EventType.MouseUp)
        {
            return;
        }

        //�������̃C�x���g����}�E�X�̈ʒu�擾
        Vector3 mousePosition = Event.current.mousePosition;

        //�V�[����̍��W�ɕϊ�
        mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
        mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);

        Debug.Log("���W : " + mousePosition.x.ToString("F2") + ", " + mousePosition.y.ToString("F2"));
    }
#endif

}
