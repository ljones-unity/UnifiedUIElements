using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIElementsAddressablePostProcessor : AssetPostprocessor
{
    private const string UxmlExtension = ".uxml";
    private const string UIElementsAddressableGroupName = "UI Documents";

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        AddressableAssetGroup group = GetOrCreateAddressableAssetGroup();
        foreach (string assetPath in importedAssets)
        {
            if (assetPath.EndsWith(UxmlExtension))
            {
                string guid = AssetDatabase.AssetPathToGUID(assetPath);
                AddAssetToAdressableGroup(guid, group);
            }
        }
        for (int i = 0; i < movedAssets.Length; i++)
        {
            string newPath = movedAssets[i];
            string guid = AssetDatabase.AssetPathToGUID(newPath);
            UpdateAddressablePath(guid, group, newPath);
        }
    }

    private static void AddAssetToAdressableGroup(string guid, AddressableAssetGroup group)
    {
        group.Settings.CreateOrMoveEntry(guid, group);
    }

    private static void UpdateAddressablePath(string guid, AddressableAssetGroup group, string newPath)
    {
        AddressableAssetEntry entry = group.Settings.FindAssetEntry(guid);
        if (entry == null)
        {
            AddAssetToAdressableGroup(guid, group);
            return;
        }
        entry.SetAddress(newPath);
    }

    private static AddressableAssetGroup GetOrCreateAddressableAssetGroup()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        AddressableAssetGroup group = settings.FindGroup(UIElementsAddressableGroupName);
        if (group == null)
        {
            // For now we just copy the default schema. Ideally if we were to make this more reusable we
            // could include a pre-made schema
            group = settings.CreateGroup(UIElementsAddressableGroupName, false, false, false, settings.DefaultGroup.Schemas);
        }
        return group;
    }
}
