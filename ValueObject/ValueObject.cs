namespace ValueObject;

/// <summary>
/// Every class which inherits from it was considered a value object.
/// <example>
/// 
/// <code>
/// public class Address 
/// {
///     public string Street { get; init; }
///     public string ZipCode { get; init; }
///     public string City { get; init; }
///     
///     public Address(string street, string zipCode, string city)
///     {
///         Street = street;
///         ZipCode = zipCode;
///         City = city;
///     }
/// }
/// 
/// </code>
/// 
/// </example>
/// </summary>
public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }
        return left is null || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !(EqualOperator(left, right));
    }

    /// <summary>
    /// Every child of the ValueObject parent has to implement theGetEqualityComponents method.
    /// <example>
    /// <code>
    ///     protected override IEnumerable --OF TYPE OBJECT-- GetEqualityComponents()
    ///     {
    ///         yield return Street;
    ///         yield return ZipCode;
    ///         yield return City;
    ///     }
    /// </code>
    /// </example>
    /// </summary>
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }
        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
    // Other utility methods
}