using Microsoft.Extensions.Compliance.Redaction;

namespace Example01.Features.Redaction;

public sealed class ReverseRedactor : Redactor
{
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        if (source.IsEmpty)
        {
            return 0;
        }
        
        if (destination.Length < source.Length)
        {
            return -1;
        }
        
        for (var i = 0; i < source.Length; i++)
        {
            destination[i] = source[source.Length - 1 - i];
        }
        
        return source.Length;
    }

    public override int GetRedactedLength(ReadOnlySpan<char> input)
    {
        return input.Length;
    }
}