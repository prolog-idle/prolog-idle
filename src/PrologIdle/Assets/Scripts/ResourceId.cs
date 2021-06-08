using System;
using JetBrains.Annotations;

public class ResourceId : IEquatable<ResourceId>
{
    public ResourceId([NotNull] string id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
    
    [NotNull] public string Id { get; }

    public bool Equals(ResourceId other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (!(obj is ResourceId otherResourceId)) return false;
        return Id == otherResourceId.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==([CanBeNull] ResourceId left, [CanBeNull] ResourceId right)
    {
        return Equals(left, right);
    }

    public static bool operator !=([CanBeNull] ResourceId left, [CanBeNull] ResourceId right)
    {
        return !Equals(left, right);
    }
}