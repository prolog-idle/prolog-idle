using UnityEngine;
using static ResourceIndex;

public class ProductionView : MonoBehaviour
{
    [SerializeField] private RectTransform _actionList;
    [SerializeField] private ProductionActionButton _actionTemplate;
    [SerializeField] private ResourceListView _resourceList;
    

    private void Start()
    {
        Ensure(Fruit, Stick, Stone, KnappedStone, Spear);

        var state = GameState.Instance;
        foreach (var resource in state.Resources.Resources)
        {
            _resourceList.Add(resource);
        }
        
        AddAction(
            "Knap Stone",
            (KnappedStone, 1),
            (Stone, 1)
        );
        AddAction(
            "Sharpen Stick",
            (Spear, 1),
            (KnappedStone, 0.5), (Stick, 1)
        );
    }

    private void Ensure(params ResourceId[] ids)
    {
        foreach (var id in ids)
        {
            GameState.Instance.Resources.Ensure(id);
        }
    }

    private void AddAction(string name, Resource product, params Resource[] ingredients)
    {
        var view = Instantiate(_actionTemplate, _actionList);
        view.Action = new ProductionAction(name, product, ingredients);
    }
}