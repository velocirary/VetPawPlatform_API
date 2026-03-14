namespace VetPawPlatform.Domain.ValueObjects;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? @object)
    {
        if (@object == null || @object.GetType() != GetType()) 
            return false;

        var other = (ValueObject)@object;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(element => element != null ? element.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}