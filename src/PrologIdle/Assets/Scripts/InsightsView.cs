using UnityEngine;
using UnityEngine.UI;

public class InsightsView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            //var additionalGatherings = new WorkplaceModifier(
            //    Gatherer,
            //    new List<ResourceAmount>(),
            //    new List<ResourceAmount>
            //    {
            //        (Stick, 0.02),
            //        (Stone, 0.01)
            //    }
            //);
            //GameState.Instance.WorkplaceModifiers.Add(additionalGatherings);
        });
    }
}