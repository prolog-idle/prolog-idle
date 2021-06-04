using System;
using UnityEngine;

public class ProductionView : MonoBehaviour
{
    [SerializeField] private RectTransform _resourceList;
    [SerializeField] private ResourceView _resourceTemplate;

    [SerializeField] private RectTransform _actionList;
    [SerializeField] private ActionButton _actionTemplate;

    private void Start()
    {
        _resourceTemplate.gameObject.SetActive(false);
        _actionTemplate.gameObject.SetActive(false);

        var state = GameState.Instance;
        AddResource("People", () => state.People);
        AddResource("Fruits", () => state.Fruits);
        AddResource("Stones", () => state.Stones);
        AddResource("Sticks", () => state.Sticks);
        AddResource("Knapped Stones", () => state.KnappedStones);
        AddResource("Spears", () => state.Spears);
        
        AddAction("Knap stone", () =>
        {
            if (state.Stones < 1)
            {
                return;
            }

            state.Stones--;
            state.KnappedStones++;
        });
        
        AddAction("Sharpen stick", () =>
        {
            if (state.KnappedStones <= 0 || state.Sticks < 1)
            {
                return;
            }

            state.KnappedStones -= 0.5f;
            state.Sticks--;
            state.Spears++;
        });
    }

    private void AddResource(string name, Func<double> getter)
    {
        var view = Instantiate(_resourceTemplate, _resourceList);
        view.gameObject.SetActive(true);
        view.SetLabel(name + ":");
        view.Getter = getter;
    }

    private void AddAction(string name, Action action)
    {
        var view = Instantiate(_actionTemplate, _actionList);
        view.gameObject.SetActive(true);
        view.SetLabel(name);
        view.OnClick(action);
    }
}