using System.Collections.Generic;

public class WorkplaceModifier
{
    public WorkplaceModifier(
        EntityId workplaceId,
        List<Resource> consumptionPerSecond,
        List<Resource> productionPerSecond)
    {
        WorkplaceId = workplaceId;
        ConsumptionPerSecond = consumptionPerSecond;
        ProductionPerSecond = productionPerSecond;
    }

    public EntityId WorkplaceId { get; set; }

    public List<Resource> ConsumptionPerSecond { get; }

    public List<Resource> ProductionPerSecond { get; }
}