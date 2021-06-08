using System.Linq;
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
        if (!HasEnoughResources())
        {
            return;
        }

        var resources = GameState.Instance.Resources;
        foreach (var ingredient in Action.Ingredients)
        {
            var resource = resources[ingredient.Id];
            resource.Value -= ingredient.Value;
        }

        resources[Action.Product.Id].Value += Action.Product.Value;
    }

    private bool HasEnoughResources()
    {
        var resources = GameState.Instance.Resources;
        return Action.Ingredients.All(ingredient => ingredient.Value <= resources[ingredient.Id].Value);
    }
}