public static class ResourceIndex
{
    public static EntityId Fruit { get; } = new EntityId("fruit");
    public static EntityId Stone { get; } = new EntityId("stone");
    public static EntityId Stick { get; } = new EntityId("stick");
    public static EntityId KnappedStone { get; } = new EntityId("knapped_stone");
    public static EntityId Spear { get; } = new EntityId("spear");

    public static string GetLabel(EntityId entityId)
    {
        if (entityId == Fruit) return "Fruits";
        if (entityId == Stone) return "Stones";
        if (entityId == Stick) return "Sticks";
        if (entityId == KnappedStone) return "Knapped Stones";
        if (entityId == Spear) return "Spears";
        return "???";
    }
}