public class Resource
{
    public Resource(ResourceId id)
    {
        Id = id;
    }

    public ResourceId Id { get; }

    public double Value { get; set; }

    public static implicit operator Resource((ResourceId id, double value) parameters)
    {
        var (id, value) = parameters;
        return new Resource(id) { Value = value };
    }
}