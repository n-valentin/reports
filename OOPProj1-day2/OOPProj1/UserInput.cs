namespace OOPProj1.TUI;

// User input handler, front-end
public class UserInput
{
    private const string _END = "END";
    private const string _NL = "\n\n";

    public string GetLine()
    {
        string input;

        do input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input));

        return input;
    }
    
    public string GetParagraph()
    {
        string paragraph = "";
        string line;
        do
        {
            line = GetLine();
            if (!string.IsNullOrWhiteSpace(line) && line.Trim() != _END)
                paragraph += line + _NL;

        } while (string.IsNullOrWhiteSpace(paragraph) || line.Trim() != _END);

        return paragraph.Trim();
    }
}
