using System.Globalization;
using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _value;

    private ResourceAmount _resourceAmount;

    public ResourceAmount ResourceAmount
    {
        get => _resourceAmount;
        set
        {
            _resourceAmount = value;
            _label.text = _resourceAmount.Resource.Name + ":";
        }
    }

    private void Update()
    {
        _value.text = ResourceAmount.Amount.ToString("F2", CultureInfo.InvariantCulture);
    }
}