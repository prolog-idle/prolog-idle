using UnityEngine;

public class ProductionView : MonoBehaviour
{
    [SerializeField] private RectTransform _actionList;
    [SerializeField] private ProductionActionButton _actionTemplate;
    [SerializeField] private ResourceListView _resourceList;
    

    private void Start()
    {
        foreach (var resource in GameDatabase.Instance.Resources)
        {
            Ensure(resource);
        }

        var state = GameState.Instance;
        foreach (var resource in state.Resources.Resources.Values)
        {
            _resourceList.Add(resource);
        }

        foreach (var action in GameDatabase.Instance.ProductionActions)
        {
            AddAction(action);
        }
    }

    private void Ensure(Resource resource)
    {
        GameState.Instance.Resources.Ensure(resource.Id);
    }

    private void AddAction(ProductionAction action)
    {
        var view = Instantiate(_actionTemplate, _actionList);
        view.Action = action;
    }
}