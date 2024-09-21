using Example01.Extensions;
using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;

namespace Example01.Services;

public interface IRedactionService
{
    string RedactPersonalData(string data);
    
    string RedactSensitiveData(string data);
}

public class RedactionService(IRedactorProvider redactorProvider) : IRedactionService
{
    private readonly Redactor _personalRedactor = redactorProvider.GetRedactor(new DataClassificationSet(DataTaxonomy.Personal));
    
    private readonly Redactor _sensitiveRedactor = redactorProvider.GetRedactor(new DataClassificationSet(DataTaxonomy.Sensitive));

    public string RedactPersonalData(string data) => _personalRedactor.Redact(data);

    public string RedactSensitiveData(string data) => _sensitiveRedactor.Redact(data);
}