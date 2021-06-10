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
        var resources = GameState.Instance.Resources;
        if (resources.Purchase(Action.Consume.Select(i => (ResourceAmount) (GameDatabase.Instance.FindResource(i.Id), i.Amount))))
        {
            foreach (var ingredient in Action.Produce)
            {
                resources.Add((GameDatabase.Instance.FindResource(ingredient.Id), ingredient.Amount));
            }
        }
    }

    private void Update()
    {
        _button.interactable = GameState.Instance.Resources.IsAffordable(Action.Consume.Select(i => (ResourceAmount) (GameDatabase.Instance.FindResource(i.Id), i.Amount)));
    }
}