using UnityEngine;

public class ResourceListView : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private ResourceView _resourcePrefab;

    public void Add(ResourceAmount resourceAmount)
    {
        var view = Instantiate(_resourcePrefab, _container);
        view.ResourceAmount = resourceAmount;
    }
}