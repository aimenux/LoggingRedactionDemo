using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;
using Microsoft.Extensions.DependencyInjection;

namespace Example03.Extensions;

public static class RedactionExtensions
{
    public static void AddRedaction(this IServiceCollection services, RedactionSettings redactionSettings)
    {
        services.AddRedaction(x =>
        {
            x.SetRedactor<StarRedactor>(new DataClassificationSet(DataTaxonomy.SensitiveData));
            x.SetHmacRedactor(options =>
            {
                options.Key = redactionSettings.Key;
                options.KeyId = redactionSettings.KeyId;
            }, new DataClassificationSet(DataTaxonomy.PersonalData));
        });
    }
}

public sealed class StarRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;
    
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        destination.Fill('*');
        return destination.Length;
    }
}

public sealed record RedactionSettings
{
    public required int KeyId { get; init; }
    public required string Key { get; init; }
}

public static class DataTaxonomy
{
    public static readonly DataClassification SensitiveData = new(nameof(DataTaxonomy), nameof(SensitiveData));
    public static readonly DataClassification PersonalData = new(nameof(DataTaxonomy), nameof(PersonalData));
}

public sealed class SensitiveDataAttribute() : DataClassificationAttribute(DataTaxonomy.SensitiveData);

public sealed class PersonalDataAttribute() : DataClassificationAttribute(DataTaxonomy.PersonalData);