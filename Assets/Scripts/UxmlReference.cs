using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class UxmlReference
{
    private readonly string assetPath;

    public UxmlReference(string assetPath)
    {
        this.assetPath = assetPath;
    }

    public void Clone(VisualElement toReplace)
    {
        VisualTreeAsset asset = LoadAsset();
        asset.CloneTree(toReplace);
    }

    public VisualElement Instantiate()
    {
        VisualTreeAsset asset = LoadAsset();
        return asset.Instantiate();
    }

    public VisualTreeAsset LoadAsset()
    {
        AsyncOperationHandle<VisualTreeAsset> handle = Addressables.LoadAssetAsync<VisualTreeAsset>(assetPath);
        handle.WaitForCompletion();
        return handle.Result;
    }
}
