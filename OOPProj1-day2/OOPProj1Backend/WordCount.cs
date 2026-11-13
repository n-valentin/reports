using System.Diagnostics;

namespace OOPProj1Backend;

// Encapsulation - Type-safety
public readonly record struct WordCount(int Amount);
public readonly record struct ExecutionTime(double Milliseconds);

// Helper Type 
public readonly struct SearchOptions
{
    public string Keyword { get; }
    public bool CaseSensitive { get; }
    public bool WholeWord { get; }
    public bool MeasureExecutionTime { get; }

    public SearchOptions(
        string keyword,
        bool caseSensitive = true,
        bool wholeWord = false,
        bool measureExecutionTime = false)
    {
        if (string.IsNullOrEmpty(keyword))
            throw new ArgumentException("Keyword cannot be empty.", nameof(keyword));

        Keyword = keyword;
        CaseSensitive = caseSensitive;
        WholeWord = wholeWord;
        MeasureExecutionTime = measureExecutionTime;
    }
}

// DTO (Immutable)
public sealed record WordCountResult
{
    public string Keyword { get; private init; }
    public bool CaseSensitive { get; private init; }
    public WordCount Count { get; private init; }
    public ExecutionTime? TimeElapsed { get; private init; }

    public WordCountResult(
        string keyword,
        bool caseSensitive,
        WordCount count,
        ExecutionTime? timeElapsed)
    {
        Keyword = keyword;
        CaseSensitive = caseSensitive;
        Count = count;
        TimeElapsed = timeElapsed;
    }
}

// Collection - Encapsulation of related objects
public sealed class WordCountHistory
{
    // prevent external modification (Immutable)
    private readonly List<WordCountResult> _results = [];

    public void Add(WordCountResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        _results.Add(result);
    }

    public List<WordCountResult> GetSortedResults()
    {
        List<WordCountResult> sorted = [.. _results];

        // ai
        sorted.Sort((a, b) =>
            string.Compare(
                a.Keyword,
                b.Keyword,
                StringComparison.OrdinalIgnoreCase));

        return sorted;
    }
}

// Object
public class WordCounter
{
    public string Text { get; protected set; }
    //public bool CaseSensitive { get; protected set; }

    // Expression-bodied constructor
    public WordCounter(string text)
    {
        WordCountValidator.ValidateText(text);
        Text = text;
    }

    public WordCountResult Count(SearchOptions options)
    {
        Stopwatch? stopwatch = options.MeasureExecutionTime
            ? Stopwatch.StartNew()
            : null;

        StringComparison comparison = options.CaseSensitive
            ? StringComparison.Ordinal
            : StringComparison.OrdinalIgnoreCase;

        WordCount count = new(
            Count(this.Text, options.Keyword, comparison, options.WholeWord));

        stopwatch?.Stop();

        ExecutionTime? executionTime = stopwatch is not null
            ? new ExecutionTime(stopwatch.Elapsed.TotalMilliseconds)
            : null;

        return new WordCountResult(
            options.Keyword,
            options.CaseSensitive,
            count,
            executionTime);
    }

    protected static int Count(
        string text,
        string keyword,
        StringComparison comparison,
        bool wholeWord)
    {
        WordCountValidator.ValidateKeyword(keyword);

        int count = 0;
        int index = 0;
        while ((index = text.IndexOf(keyword, index, comparison)) != -1)
        {
            bool validMatch = !wholeWord ||
                (IsWordBoundary(text, index - 1) &&
                IsWordBoundary(text, index + keyword.Length));

            if (validMatch)
            {
                count++;

                //index++;
                index += keyword.Length; // avoid overlapping matches
            }

            else index++;
        }
        return count;
    }

    // Helper method check if whole word
    private static bool IsWordBoundary(string text, int index)
    {
        return index < 0 ||
                index >= text.Length ||
                (!char.IsLetterOrDigit(text[index]) && text[index] != '_');
    }
}

// Dependency injection, Factory pattern
public class WordCounterFactory
{
    public static WordCounter Create(string text) => new(text);
}