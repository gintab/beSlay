using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UImanager : Editor
{
    public override void OnInspectorGUI()
    {
        // SerializeFieldを更新した場合の処理
        if (GUI.changed)
        {
            // SceneViewのUIを更新
            SceneView.RepaintAll();
        }
    }
}
