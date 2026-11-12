using System.Diagnostics; // stopwatch

namespace OOPProj1.Backend;

// DTO
public record WordCountResult
(
    int Count,
    string Keyword,
    bool CaseSensitive,
    TimeSpan TimeElapsed
);

// Helper Type
public readonly struct SearchOptions
{
    public string Keyword { get; }
    public bool CaseSensitive { get; }

    public SearchOptions(string keyword, bool caseSensitive = true)
    {
        if (string.IsNullOrEmpty(keyword))
            throw new ArgumentException("Keyword cannot be empty.", nameof(keyword));

        Keyword = keyword;
        CaseSensitive = caseSensitive;
    }
}

// Object
public class WordCounter
{
    public string Text { get; protected set; }
    public bool CaseSensitive { get; protected set; }

    // Expression-bodied constructor
    public WordCounter(string text) =>
        Text = text ?? throw new ArgumentNullException(nameof(text));

    public WordCountResult Count(SearchOptions options)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        StringComparison comparison = options.CaseSensitive
            ? StringComparison.Ordinal
            : StringComparison.OrdinalIgnoreCase;

        int count = Count(this.Text, options.Keyword, comparison);

        stopwatch.Stop();
        TimeSpan timeElapsed = stopwatch.Elapsed;

        return new WordCountResult(count, options.Keyword, options.CaseSensitive, timeElapsed);
    }

    protected int Count(string text, string keyword, StringComparison comparison)
    {
        int count = 0;
        int index = 0;
        while ((index = text.IndexOf(keyword, index, comparison)) != -1)
        {
            count++;
            index++;
        }
        return count;
    }
}

// Dependency injection, Factory pattern
public class WordCounterFactory
{
    public WordCounter Create(string text) => new(text);
}