using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Transform), true)]
[CanEditMultipleObjects]
public class TransformEditor : Editor
{
    private Editor _defaultEditor;
    private Transform _transform;

    private void OnEnable()
    {
        _transform = target as Transform;
        _defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));
    }

    private void OnDisable()
    {
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
        
        if (GUILayout.Button("Reset"))
        {
            _transform.localPosition = Vector3.zero;
            _transform.localRotation = Quaternion.Euler(Vector3.zero);
            _transform.localScale = Vector3.one;
        }
    }
}
