using Microsoft.Extensions.Compliance.Redaction;

namespace Example02.Features.Redaction;

public sealed class StarRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => Math.Min(input.Length, 10);
    
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        destination.Fill('*');
        return destination.Length;
    }
}