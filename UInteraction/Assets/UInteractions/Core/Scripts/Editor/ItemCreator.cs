using UnityEngine;
using UnityEditor;

public class ItemCreator
{
    [MenuItem("Assets/Create/Item")]
    public static Item Create()
    {
        Item asset = ScriptableObject.CreateInstance<Item>();

        string path = EditorUtility.SaveFilePanelInProject("Save New Item", "Item", "asset", "Save it!");

        if(path != null)
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
        }
        return asset;
    }
}
