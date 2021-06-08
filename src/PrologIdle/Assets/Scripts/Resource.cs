public class Resource
{
    public Resource(EntityId id)
    {
        Id = id;
    }

    public EntityId Id { get; }

    public double Value { get; set; }

    public static implicit operator Resource((EntityId id, double value) parameters)
    {
        var (id, value) = parameters;
        return new Resource(id) { Value = value };
    }
}