using Backend = OOPProj1Backend;

namespace OOPProj1.TUI;

public class SearchClass
{
    public void Search(UserInput userInput, Backend.WordCountHistory history)
    {
        // Get user inputs
        Console.WriteLine("Enter text to search in (\"END\" to finish.):\n");
        string _text = userInput.GetParagraph();

        Console.Write("Enter search keyword: ");
        string _keyword = userInput.GetLine();

        // Create WordCounter instances
        Backend.WordCounter wordCount = Backend.WordCounterFactory.Create(_text);

        Backend.SearchOptions options = SetSearchOptions(_keyword);

        // Pack DTO
        Backend.WordCountResult result = wordCount.Count(options);

        // Put into search history
        history.Add(result);

        string timeElapsed = result.TimeElapsed is not null
            ? $"Time Elapsed: {result.TimeElapsed.Value.Milliseconds:F2}ms\n"
            : "";
        
        // Print Result
        Console.WriteLine(
            "\n\n" +
            Utils.Utils.StraightLine + "\n" +

            $"Keyword: {result.Keyword}\n" +
            $"Appearances: {result.Count.Amount}\n" +
            timeElapsed +

            Utils.Utils.StraightLine);
    }

    private Backend.SearchOptions SetSearchOptions(string keyword)
    {
        bool caseSensitive = GetOptionDecision("Case Sensitive");
        bool wholeWord = GetOptionDecision("Whole Words");
        bool measureExecutionTime = GetOptionDecision("Measure Execution Time");

        return new Backend.SearchOptions(
            keyword,
            caseSensitive,
            wholeWord,
            measureExecutionTime);
    }

    private bool GetOptionDecision(string option)
    {
        Console.Write(
            "\n\nOption: {0}?\n" +
            "[1] Yes\n" +
            "[2] No\n\n" +
            "Input: ",
            option);

        char inputChar = '\0';

        while (true)
        {
            inputChar = Console.ReadKey(true).KeyChar;

            if (inputChar == '1' || inputChar == '2')
                break;
        }

        Console.Write(inputChar);

        return inputChar == '1';
    }
}
