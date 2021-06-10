using System.Collections.Generic;

public class WorkplaceModifier
{
    public WorkplaceModifier(
        EntityId workplaceId,
        List<ResourceAmount> consumptionPerSecond,
        List<ResourceAmount> productionPerSecond)
    {
        WorkplaceId = workplaceId;
        ConsumptionPerSecond = consumptionPerSecond;
        ProductionPerSecond = productionPerSecond;
    }

    public EntityId WorkplaceId { get; set; }

    public List<ResourceAmount> ConsumptionPerSecond { get; }

    public List<ResourceAmount> ProductionPerSecond { get; }
}