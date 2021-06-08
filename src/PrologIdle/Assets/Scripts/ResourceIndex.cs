public static class ResourceIndex
{
    public static ResourceId Fruit { get; } = new ResourceId("fruit");
    public static ResourceId Stone { get; } = new ResourceId("stone");
    public static ResourceId Stick { get; } = new ResourceId("stick");
    public static ResourceId KnappedStone { get; } = new ResourceId("knapped_stone");
    public static ResourceId Spear { get; } = new ResourceId("spear");

    public static string GetLabel(ResourceId resourceId)
    {
        if (resourceId == Fruit) return "Fruits";
        if (resourceId == Stone) return "Stones";
        if (resourceId == Stick) return "Sticks";
        if (resourceId == KnappedStone) return "Knapped Stones";
        if (resourceId == Spear) return "Spears";
        return "???";
    }
}