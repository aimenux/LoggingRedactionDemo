using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;

namespace Example02.Extensions;

public static class RedactionExtensions
{
    public static void AddRedaction(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.EnableRedaction();
        
        loggingBuilder.Services.AddRedaction(x =>
        {
            x.SetRedactor<StarRedactor>(new DataClassificationSet(DataTaxonomy.Sensitive));
            x.SetHmacRedactor(options =>
            {
                options.Key = RedactionSettings.Key;
                options.KeyId = RedactionSettings.KeyId;
            }, new DataClassificationSet(DataTaxonomy.Personal));
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

public static class RedactionSettings
{
    public const int KeyId = 1;
    public static readonly string Key = Convert.ToBase64String("e0a3afcf-c1d0-4fb8-abdb-4a89ed028136"u8);
}

public static class DataTaxonomy
{
    public static readonly DataClassification Sensitive = new(nameof(DataTaxonomy), nameof(Sensitive));
    public static readonly DataClassification Personal = new(nameof(DataTaxonomy), nameof(Personal));
}

public sealed class SensitiveAttribute() : DataClassificationAttribute(DataTaxonomy.Sensitive);

public sealed class PersonalAttribute() : DataClassificationAttribute(DataTaxonomy.Personal);