#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(URSSManager))]
public class URSSManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        URSSManager urssManager = (URSSManager)target;
        if (GUILayout.Button("Fill Seats"))
        {
            urssManager.FillSeats();
        }
    }
}
#endif