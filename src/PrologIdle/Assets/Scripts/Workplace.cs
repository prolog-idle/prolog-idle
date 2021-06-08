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
        var modifiers = GameState.Instance.WorkplaceModifiers
            .Where(m => m.WorkplaceId == Id)
            .ToList();
        TickConsumption(ConsumptionPerSecond, people, delta);
        foreach (var modifier in modifiers)
        {
            TickConsumption(modifier.ConsumptionPerSecond, people, delta);
        }
        TickProduction(ProductionPerSecond, people, delta);
        foreach (var modifier in modifiers)
        {
            TickProduction(modifier.ProductionPerSecond, people, delta);
        }
    }

    private static void TickConsumption(List<Resource> consumptionPerSecond, int people, float delta)
    {
        var resources = GameState.Instance.Resources;
        foreach (var resource in consumptionPerSecond)
        {
            resources.Remove(resource, people * delta);
        }
    }

    private static void TickProduction(List<Resource> productionPerSecond, int people, float delta)
    {
        var resources = GameState.Instance.Resources;
        foreach (var resource in productionPerSecond)
        {
            resources.Add(resource, people * delta);
        }
    }
}