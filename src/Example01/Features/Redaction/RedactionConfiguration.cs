using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;

namespace Example01.Features.Redaction;

public static class RedactionConfiguration
{
    private const int KeyId = 1;
    
    private static readonly string Key = Convert.ToBase64String("e0a3afcf-c1d0-4fb8-abdb-4a89ed028136"u8);
    
    public static Action<IRedactionBuilder> Build()
    {
        return builder => 
        {
            builder.SetRedactor<StarRedactor>(new DataClassificationSet(DataTaxonomy.Sensitive));
            builder.SetHmacRedactor(options =>
            {
                options.Key = Key;
                options.KeyId = KeyId;
            }, new DataClassificationSet(DataTaxonomy.Personal));
        };
    }
}