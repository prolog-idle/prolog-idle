using System.Collections.Generic;
using System.Linq;

public class ResourceStockpile
{
    public Dictionary<string, ResourceAmount> Resources { get; } = new Dictionary<string, ResourceAmount>();

    public ResourceAmount this[ResourceAmount resourceAmount] => this[resourceAmount.Resource];

    public ResourceAmount this[Resource resource] => this[resource.Id];

    public ResourceAmount this[string id]
    {
        get
        {
            if (Resources.TryGetValue(id, out var resourceAmount))
            {
                return resourceAmount;
            }
            var resource = GameDatabase.Instance.Resources.Find(r => r.Id == id);
            resourceAmount = new ResourceAmount(resource);
            Resources[id] = resourceAmount;
            return resourceAmount;
        }
    }

    public void Ensure(string id)
    {
        var a = this[id].Amount;
    }

    private ResourceAmount Add(string id)
    {
        var resource = GameDatabase.Instance.Resources.Find(r => r.Id == id);
        var resourceAmount = new ResourceAmount(resource);
        Resources[id] = resourceAmount;
        return resourceAmount;
    }

    public void Remove(ResourceAmount resourceAmount, float multiplier = 1)
    {
        this[resourceAmount].Amount = Clamp(this[resourceAmount].Amount - resourceAmount.Amount * multiplier, 0, double.MaxValue);
    }

    public void Add(ResourceAmount resourceAmount, float multiplier = 1)
    {
        this[resourceAmount].Amount += resourceAmount.Amount * multiplier;
    }

    public bool Purchase(IEnumerable<ResourceAmount> resources)
    {
        var list = resources.ToList();
        if (!IsAffordable(list))
        {
            return false;
        }

        foreach (var resource in list)
        {
            this[resource].Amount -= resource.Amount;
        }

        return true;
    }

    public bool Purchase(ResourceAmount resourceAmount)
    {
        if (!IsAffordable(resourceAmount))
        {
            return false;
        }

        this[resourceAmount].Amount -= resourceAmount.Amount;
        return true;
    }

    public bool IsAffordable(IEnumerable<ResourceAmount> resources)
    {
        return resources.All(IsAffordable);
    }

    public bool IsAffordable(ResourceAmount resourceAmount)
    {
        return this[resourceAmount].Amount >= resourceAmount.Amount;
    }

    public static double Clamp(double value, double min, double max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;
        return value;
    }
}