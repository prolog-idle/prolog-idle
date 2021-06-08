using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceDatabase
{
    public List<Resource> Resources { get; } = new List<Resource>();

    public Resource this[EntityId id]
    {
        get
        {
            var resource = Resources.Find(r => r.Id == id);
            if (resource == null)
            {
                resource = Add(id);
            }

            return resource;
        }
    }

    public void Ensure(EntityId id)
    {
        var resource = Resources.Find(r => r.Id == id);
        if (resource == null)
        {
            Add(id);
        }
    }

    private Resource Add(EntityId id)
    {
        var resource = new Resource(id);
        Resources.Add(resource);
        return resource;
    }

    public void Remove(Resource resource, float multiplier = 1)
    {
        this[resource.Id].Value = Clamp(this[resource.Id].Value - resource.Value * multiplier, 0, double.MaxValue);
    }

    public void Add(Resource resource, float multiplier = 1)
    {
        this[resource.Id].Value += resource.Value * multiplier;
    }

    public bool Purchase(IEnumerable<Resource> resources)
    {
        var list = resources.ToList();
        if (!IsAffordable(list))
        {
            return false;
        }

        foreach (var resource in list)
        {
            this[resource.Id].Value -= resource.Value;
        }

        return true;
    }

    public bool Purchase(Resource resource)
    {
        if (!IsAffordable(resource))
        {
            return false;
        }

        this[resource.Id].Value -= resource.Value;
        return true;
    }

    public bool IsAffordable(IEnumerable<Resource> resources)
    {
        return resources.All(IsAffordable);
    }

    public bool IsAffordable(Resource resource)
    {
        return this[resource.Id].Value >= resource.Value;
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