using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _label;

    public void SetLabel(string label) => _label.text = label;

    public void OnClick(Action action) => _button.onClick.AddListener(() => action());
}