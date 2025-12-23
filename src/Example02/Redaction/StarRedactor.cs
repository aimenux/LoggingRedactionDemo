using Microsoft.Extensions.Compliance.Redaction;

namespace Example02.Redaction;

public sealed class StarRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;
    
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        destination.Fill('*');
        return destination.Length;
    }
}