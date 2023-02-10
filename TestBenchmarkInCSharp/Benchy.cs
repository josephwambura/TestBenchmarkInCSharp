using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

[MemoryDiagnoser]
public class Benchy
{
    public static readonly string _dateAsText = DateTime.UtcNow.ToString("dd MM yyyy");

    [Benchmark]
    public (int dat, int month, int year) DateWithStringAndSubstring()
    {
        var dayAsText = _dateAsText[..2];
        var monthAsText = _dateAsText.Substring(3, 2);
        var yearAsText = _dateAsText[6..];

        var day = int.Parse(dayAsText);
        var month = int.Parse(monthAsText);
        var year = int.Parse(yearAsText);

        return (day, month, year);
    }

    [Benchmark]
    public (int dat, int month, int year) DateWithStringAndSpan()
    {
        ReadOnlySpan<char> dateAsSpan = _dateAsText;

        var dayAsText = dateAsSpan[..2];
        var monthAsText = dateAsSpan.Slice(3, 2);
        var yearAsText = dateAsSpan[6..];

        var day = int.Parse(dayAsText);
        var month = int.Parse(monthAsText);
        var year = int.Parse(yearAsText);

        return (day, month, year);
    }
}