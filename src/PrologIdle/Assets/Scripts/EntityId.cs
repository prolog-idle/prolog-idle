using System;
using JetBrains.Annotations;

public class EntityId : IEquatable<EntityId>
{
    public EntityId([NotNull] string id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
    
    [NotNull] public string Id { get; }

    public bool Equals(EntityId other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (!(obj is EntityId otherId)) return false;
        return Id == otherId.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==([CanBeNull] EntityId left, [CanBeNull] EntityId right)
    {
        return Equals(left, right);
    }

    public static bool operator !=([CanBeNull] EntityId left, [CanBeNull] EntityId right)
    {
        return !Equals(left, right);
    }
}