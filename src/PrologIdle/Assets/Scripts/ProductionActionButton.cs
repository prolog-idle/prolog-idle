using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Button _button;

    private ProductionAction _action;

    public ProductionAction Action
    {
        get => _action;
        set
        {
            _action = value;
            _label.text = value.Name;
        }
    }

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var resources = GameState.Instance.Resources;
        if (resources.Purchase(Action.Ingredients))
        {
            resources.Add(Action.Product);
        }
    }

    private void Update()
    {
        _button.interactable = GameState.Instance.Resources.IsAffordable(Action.Ingredients);
    }
}