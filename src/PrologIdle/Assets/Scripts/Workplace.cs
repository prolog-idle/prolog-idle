using System.Collections.Generic;
using System.Linq;

public class Workplace
{
    public Workplace(EntityId id, List<Resource> consumptionPerSecond, List<Resource> productionPerSecond)
    {
        Id = id;
        ConsumptionPerSecond = consumptionPerSecond;
        ProductionPerSecond = productionPerSecond;
    }
    
    public EntityId Id { get; }

    public List<Resource> ConsumptionPerSecond { get; }

    public List<Resource> ProductionPerSecond { get; }

    public bool IsAvailable()
    {
        var resources = GameState.Instance.Resources;
        return ConsumptionPerSecond.All(r => resources[r.Id].Value > 0);
    }

    public void Tick(int people, float delta)
    {
        var resources = GameState.Instance.Resources;
        foreach (var resource in ConsumptionPerSecond)
        {
            resources.Remove(resource, people * delta);
        }
        foreach (var resource in ProductionPerSecond)
        {
            resources.Add(resource, people * delta);
        }
    }
}