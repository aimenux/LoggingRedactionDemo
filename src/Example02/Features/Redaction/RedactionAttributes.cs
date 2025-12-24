using Microsoft.Extensions.Compliance.Classification;

namespace Example02.Features.Redaction;

public static class DataTaxonomy
{
    public static readonly DataClassification Restricted = new(nameof(DataTaxonomy), nameof(Restricted));
    public static readonly DataClassification Sensitive = new(nameof(DataTaxonomy), nameof(Sensitive));
    public static readonly DataClassification Personal = new(nameof(DataTaxonomy), nameof(Personal));
    public static readonly DataClassification Default = new(nameof(DataTaxonomy), nameof(Default));
}

[AttributeUsage(AttributeTargets.All)]
public sealed class RestrictedAttribute() : DataClassificationAttribute(DataTaxonomy.Restricted);

[AttributeUsage(AttributeTargets.All)]
public sealed class SensitiveAttribute() : DataClassificationAttribute(DataTaxonomy.Sensitive);

[AttributeUsage(AttributeTargets.All)]
public sealed class PersonalAttribute() : DataClassificationAttribute(DataTaxonomy.Personal);

[AttributeUsage(AttributeTargets.All)]
public sealed class DefaultAttribute() : DataClassificationAttribute(DataTaxonomy.Default);