using Microsoft.Extensions.DependencyInjection;
using Utils = OOPProj1.Utils;
using Backend = OOPProj1Backend;
using TUI = OOPProj1.TUI;


// Dependecy injection
ServiceCollection services = new ServiceCollection();
services.AddSingleton<TUI.UserInput>();
services.AddSingleton<TUI.SearchClass>();
services.AddSingleton<Backend.WordCountHistory>();

using ServiceProvider serviceProvider = services.BuildServiceProvider();

// Instantiate
TUI.UserInput userInput = serviceProvider.GetRequiredService<TUI.UserInput>();
TUI.SearchClass searching = serviceProvider.GetRequiredService<TUI.SearchClass>();
Backend.WordCountHistory history = serviceProvider.GetRequiredService<Backend.WordCountHistory>();


bool running = true;
while (running)
{
    Menu();

    MenuChoice();
}

void Menu()
{
    Console.Write(
        "\n\n{0}\n" +
        "Word Counter Program\n" +
        "[1] New Search\n" +
        "[2] View History\n" +
        "[3] Exit\n" +
        "{0}\n\n" + 
        "Input: ",
        Utils.Utils.StraightLine);
}

// Returns true if invalid input
void MenuChoice()
{
    char inputChar = '\0';
    while (true)
    {
        inputChar = Console.ReadKey(true).KeyChar;

        if (inputChar == '1')
        {
            Console.Clear();
            searching.Search(userInput, history);
            break;
        }
        else if (inputChar == '2')
        {
            Console.Clear();
            ViewHistory();
            break;
        }
        else if (inputChar == '3')
        {
            running = false;
            break;
        }
    }
}


void ViewHistory()
{
    Console.WriteLine(
        "\n\n" +
        Utils.Utils.StraightLine + "\n" +
        "Search History\n" +
        Utils.Utils.StraightLine);
    
    List<Backend.WordCountResult> sortedHistory = history.GetSortedResults();

    if (sortedHistory.Count == 0)
        Console.WriteLine("No searches have been made yet.");
    
    else
    {
        foreach (Backend.WordCountResult result in history.GetSortedResults())
        {
            string timeElapsed = result.TimeElapsed is not null
                ? $"Time Elapsed: {result.TimeElapsed.Value.Milliseconds:F2}ms\n"
                : "";

            // yes this can be reused, chose not to.
            Console.WriteLine(
                "\n\n" +
                Utils.Utils.StraightLine + "\n" +
                $"Keyword: {result.Keyword}\n" +
                $"Appearances: {result.Count.Amount}\n" +
                timeElapsed +
                Utils.Utils.StraightLine);
        }
    }
}