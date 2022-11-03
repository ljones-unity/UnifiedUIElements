using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestEditorWindow : EditorWindow
{
    private UxmlReference uxml = new UxmlReference("Assets/UI/Forms/Cross Domain Test/CrossDomainTest.uxml");

    [MenuItem("Test/Editor Window")]
    public static void Open()
    {
        TestEditorWindow window = GetWindow<TestEditorWindow>();
        window.Init();
        window.Show();
    }

    private void Init()
    {
        uxml.Clone(rootVisualElement);
    }
}
