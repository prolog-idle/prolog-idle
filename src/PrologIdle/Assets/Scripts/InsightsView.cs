using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ResourceIndex;
using static WorkplaceIndex;

public class InsightsView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            var additionalGatherings = new WorkplaceModifier(
                Gatherer,
                new List<Resource>(),
                new List<Resource>
                {
                    (Stick, 0.02),
                    (Stone, 0.01)
                }
            );
            GameState.Instance.WorkplaceModifiers.Add(additionalGatherings);
        });
    }
}