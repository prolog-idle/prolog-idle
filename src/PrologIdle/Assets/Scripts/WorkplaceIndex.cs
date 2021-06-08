public class WorkplaceIndex
{
    public static EntityId Gatherer { get; } = new EntityId("gatherer");

    public static string GetLabel(EntityId entityId)
    {
        if (entityId == Gatherer) return "Gatherers";
        return "???";
    }
}