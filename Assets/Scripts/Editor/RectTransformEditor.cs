using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RectTransform), true)]
[CanEditMultipleObjects]
public class RectTransformEditor : Editor
{
    private Editor _defaultEditor;
    private RectTransform _transform;

    private void OnEnable()
    {
        _transform = target as RectTransform;
        _defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.RectTransformEditor, UnityEditor"));
    }

    private void OnDisable()
    {
        if (!_defaultEditor)
            return;
       
        MethodInfo disableMethod = _defaultEditor.GetType()
            .GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (disableMethod != null)
            disableMethod.Invoke(_defaultEditor, null);
        DestroyImmediate(_defaultEditor);
    }

    public override void OnInspectorGUI()
    {
        _defaultEditor.OnInspectorGUI();
        GUILayout.Space(10f);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Copy"))
        {
            UnityEditorInternal.ComponentUtility.CopyComponent(_transform);
        }

        if (GUILayout.Button("Paste"))
        {
            UnityEditorInternal.ComponentUtility.PasteComponentValues(_transform);
        }
        
        EditorGUILayout.EndHorizontal();
    }
}