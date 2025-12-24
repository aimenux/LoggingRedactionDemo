using Microsoft.Extensions.Compliance.Classification;

namespace Example02.Features.Redaction;

public static class DataTaxonomy
{
    public static readonly DataClassification Sensitive = new(nameof(DataTaxonomy), nameof(Sensitive));
    public static readonly DataClassification Personal = new(nameof(DataTaxonomy), nameof(Personal));
}

[AttributeUsage(AttributeTargets.All)]
public sealed class SensitiveAttribute() : DataClassificationAttribute(DataTaxonomy.Sensitive);

[AttributeUsage(AttributeTargets.All)]
public sealed class PersonalAttribute() : DataClassificationAttribute(DataTaxonomy.Personal);