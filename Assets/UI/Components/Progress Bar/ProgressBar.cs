using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProgressBar : VisualElement
{
    private UxmlReference uxml = new UxmlReference("Assets/UI/Components/Progress Bar/ProgressBar.uxml");

    public ProgressBar()
    {
        uxml.Clone(this);
    }

    public new class UxmlFactory : UxmlFactory<ProgressBar, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }
}
