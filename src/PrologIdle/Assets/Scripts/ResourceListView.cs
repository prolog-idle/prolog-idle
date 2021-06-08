using UnityEngine;

public class ResourceListView : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private ResourceView _resourcePrefab;

    public void Add(Resource resource)
    {
        var view = Instantiate(_resourcePrefab, _container);
        view.Resource = resource;
    }
}