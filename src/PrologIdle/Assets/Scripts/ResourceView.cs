using System.Globalization;
using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _value;

    private Resource _resource;

    public Resource Resource
    {
        get => _resource;
        set
        {
            _resource = value;
            _label.text = ResourceIndex.GetLabel(value.Id) + ":";
        }
    }

    private void Update()
    {
        _value.text = Resource.Value.ToString("F2", CultureInfo.InvariantCulture);
    }
}