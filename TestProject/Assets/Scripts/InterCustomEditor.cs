using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractionManager))]
public class InterCustomEditor : Editor {
    InteractionManager im;

    int index = 0;
    string[] options = new string[] { "Lever", "Door"};

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Rect r = EditorGUILayout.BeginHorizontal();
        index = EditorGUILayout.Popup("Interact Type:",
             index, options, EditorStyles.popup);
        EditorGUILayout.EndHorizontal();
    }
}
