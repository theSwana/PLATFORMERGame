using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ImportedAnimations))]
public class ImportedAnimationsInspector : Editor
{
    private ImportedAnimations myTarget
    {
        get
        {
            return (ImportedAnimations)target;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginDisabledGroup(Selection.gameObjects.Length == 0);
        if (GUILayout.Button("Add Animations"))
        {
            Debug.Log(AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.gameObjects[0]), typeof(AnimationClip)));
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                if ((AnimationClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.gameObjects[i]), typeof(AnimationClip)) == null) return;

                 myTarget.animations.Add( (AnimationClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.gameObjects[i]), typeof(AnimationClip)));
            }
        }
        EditorGUI.EndDisabledGroup();
    }
    
}
