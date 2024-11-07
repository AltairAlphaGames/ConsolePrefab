using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _stylesheet;

    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        var root = _document.rootVisualElement;
        root.styleSheets.Add(_stylesheet);

        var titleLabel = new Label("Hello");

        root.Add(titleLabel);
    }
}
