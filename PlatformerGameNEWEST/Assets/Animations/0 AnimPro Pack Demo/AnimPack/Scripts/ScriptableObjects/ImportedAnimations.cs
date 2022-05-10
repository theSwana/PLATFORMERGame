using System.IO;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ImportedAnimations : ScriptableObject {

    public List<AnimationClip> animations = new List<AnimationClip>();

    public AnimationClip[] GetAnimations
    {
        get
        {
            return animations.ToArray();
        }
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Imported Animations", priority = 100)]
    public static void Create()
    {
        ImportedAnimations asset = CreateInstance<ImportedAnimations>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string fileName;

        fileName = "NewImportedAnimations_Data";

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + fileName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;

    }
#endif
}
