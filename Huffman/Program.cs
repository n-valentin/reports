internal class Program
{
    static void Main(string[] args)
    {
        string GetEncodedString(string input, Dictionary<char, byte> legend)
        {
            string output = "";

            foreach (char c in input)
            {
                output = $"{output} {Convert.ToString(legend[c], 2)}";
            }

            return output.Trim();
        }

        string GetDecodedString(string input, Dictionary<char, byte> legend)
        {
            // get <byte, char>
            Dictionary<byte, char> reverseLegend = legend.ToDictionary(kv => kv.Value, kv => kv.Key);

            string output = "";

            foreach (string token in input.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                byte value = Convert.ToByte(token, 2);
                output += reverseLegend[value];
            }

            return output.Trim();
        }

        // count the number of occurrences of each character in the input string
        Dictionary<char, int> GetCharacterCount(string input)
        {
            Dictionary<char, int> dict = new();

            foreach (char c in input)
            {
                int b = (byte)c;
                if (dict.ContainsKey(c))
                    dict[c]++;
                else
                    dict[c] = 1;

                dict =  dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            }

            return dict;
        }

        // store each character as a byte in the list as a legend
        Dictionary<char, byte> GetByteLegend(Dictionary<char, int> charCount)
        {
            Dictionary<char, byte> dict = new();

            byte _nextByte = 0;
            foreach (KeyValuePair<char, int> c in charCount)
            {
                if (!dict.ContainsKey(c.Key))
                    dict[c.Key] = _nextByte++;
            }
            return dict;
        }



        Console.WriteLine("Input: ");

        string input = "";
        do
        {
            input = Console.ReadLine().Trim();
        }
        while (input == "");

        Dictionary<char, int> charCount = GetCharacterCount(input);
        Dictionary<char, byte> byteLegend = GetByteLegend(charCount);

        string str_byteLegend = string.Join(" ", byteLegend.Select(kvp => $"{kvp.Key}={Convert.ToString(kvp.Value, 2)}"));
        string str_byteCounts = string.Join(" ", charCount.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        string str_encodedInput = GetEncodedString(input, byteLegend);
        string str_decodedOutput = GetDecodedString(str_encodedInput, byteLegend);

        Console.WriteLine("\nByte Legend: " + str_byteLegend);
        Console.WriteLine("Characters: " + str_byteCounts);
        Console.WriteLine("\nEncoded Output: " + str_encodedInput);
        Console.WriteLine("\nDecoded Output: " + str_decodedOutput);

        Console.ReadLine();
    }
}
