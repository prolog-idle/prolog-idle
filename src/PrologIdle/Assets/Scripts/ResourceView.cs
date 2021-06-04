using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _value;

    public void SetLabel(string label) => _label.text = label;
    
    public Func<double> Getter { get; set; }
    
    private void Update()
    {
        _value.text = Getter().ToString("F2", CultureInfo.InvariantCulture);
    }
}