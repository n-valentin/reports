using Microsoft.Extensions.DependencyInjection; // service colleciton


// Dependecy injection
ServiceCollection services = new ServiceCollection();
services.AddSingleton<OOPProj1.TUI.UserInput>();
services.AddSingleton<OOPProj1.Backend.WordCounterFactory>();

using ServiceProvider serviceProvider = services.BuildServiceProvider();


// Instantiate
OOPProj1.TUI.UserInput userInput = serviceProvider.GetRequiredService<OOPProj1.TUI.UserInput>();
OOPProj1.Backend.WordCounterFactory wordCounter = serviceProvider.GetRequiredService<OOPProj1.Backend.WordCounterFactory>();


// Get user inputs
Console.WriteLine("Enter paragraph (\"END\" to finish.):");
string inputParagraph = userInput.GetParagraph();

Console.Write("Enter search keyword: ");
string inputKeyword = userInput.GetLine();


// Create WordCounter instances
OOPProj1.Backend.WordCounter wordCount = wordCounter.Create(inputParagraph);

// Set Helper type
OOPProj1.Backend.SearchOptions options = new(inputKeyword, caseSensitive: true);

// Pack DTO
OOPProj1.Backend.WordCountResult result = wordCount.Count(options);


// Print Result
Console.WriteLine($"\nKeyword: {result.Keyword}\nAppearances: {result.Count}\nTime Elapsed: {result.TimeElapsed.TotalMilliseconds:F2}ms");
