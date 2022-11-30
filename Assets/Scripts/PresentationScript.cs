using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    public UIDocument MainUIDocument;
    public List<float> ProgressValues;

    public VisualTreeAsset ProgressBarTemplate;

    private InGameUIController controller;

    private void Awake()
    {
        controller = new();
        VisualElement root = MainUIDocument.rootVisualElement.Q<VisualElement>("content-root");
        controller.Init(root, ProgressBarTemplate, ProgressValues);
    }
}

public class InGameUIController
{
    private TextField textBox;
    private List<ProgressBarController> progressBars = new();

    public void Init(VisualElement viewRoot, VisualTreeAsset progressBarTemplate, IEnumerable<float> progresses)
    {
        textBox = viewRoot.Q<TextField>();
        foreach (float progress in progresses)
        {
            TemplateContainer progressBarUI = progressBarTemplate.Instantiate();
            viewRoot.Add(progressBarUI);
            ProgressBarController controller = new();
            controller.Init(progressBarUI);
            progressBars.Add(controller);
        }
    }
}

public class ProgressBarController
{
    public void Init(VisualElement viewRoot)
    {
    }
}

public class InGameUIElement : VisualElement
{
    private const string UxmlPath = "Assets/UI/Forms/InGameUIElement.uxml";

    public InGameUIElement()
    {
        var handle = Addressables.LoadAssetAsync<VisualTreeAsset>(UxmlPath);
        handle.WaitForCompletion();
        VisualTreeAsset uxml = handle.Result;
        uxml.CloneTree(this);
    }

    public void SetProgressValues(IEnumerable<float> progresses)
    {
        Clear();
        foreach (float progress in progresses)
        {
            ProgressBarElement elem = new ProgressBarElement();
            elem.SetProgress(progress);
            Add(elem);
        }
    }
}

public class ProgressBarElement : VisualElement 
{
    public void SetProgress(float f) { }
}
