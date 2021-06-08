using System.Collections.Generic;

public class ResourceDatabase
{
    public List<Resource> Resources { get; } = new List<Resource>();

    public Resource this[ResourceId id]
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

    public void Ensure(ResourceId id)
    {
        var resource = Resources.Find(r => r.Id == id);
        if (resource == null)
        {
            Add(id);
        }
    }

    private Resource Add(ResourceId id)
    {
        var resource = new Resource(id);
        Resources.Add(resource);
        return resource;
    }
}