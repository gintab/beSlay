using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UImanager : Editor
{
    public override void OnInspectorGUI()
    {
        // SerializeField���X�V�����ꍇ�̏���
        if (GUI.changed)
        {
            // SceneView��UI���X�V
            SceneView.RepaintAll();
        }
    }
}
