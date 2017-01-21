#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Controller), true)]
public class ControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Controller ctrllr = (Controller)target;
        if (GUILayout.Button("DebugWave"))
        {
            ctrllr.hasWaved = false;
            ctrllr.Wave();
        }
    }
}
#endif
