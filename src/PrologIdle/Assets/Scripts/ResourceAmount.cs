public class ResourceAmount
{
    public ResourceAmount(Resource resource, double amount = 0)
    {
        Resource = resource;
        Amount = amount;
    }

    public Resource Resource { get; }

    public double Amount { get; set; }

    public static implicit operator ResourceAmount((Resource resource, double value) parameters)
    {
        var (resource, value) = parameters;
        return new ResourceAmount(resource, value);
    }
}