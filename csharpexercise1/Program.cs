internal class Program
{
    static void Main(string[] args)
    {
        void PrintMultiplicationTable(int gridNumbers)
        {
            for (int i = 1; i <= gridNumbers; i++)
            {
                for (int j = 1; j <= gridNumbers; j++)
                    Console.Write($"{i * j,4}");

                Console.WriteLine();
            }
        }

        void PrintDiceCombinationsTo5()
        {
            for (int a = 1; a <= 6; a++)
                for (int b = 1; b <= 6; b++)
                    if (a + b == 5)
                        Console.WriteLine($"({a},{b})");
        }

        // Procedure

        PrintMultiplicationTable(gridNumbers: 10);

        Console.WriteLine();

        PrintDiceCombinationsTo5();

        Console.ReadLine();
    }
}


// for reference, startede med det her:

/*
internal class Program
{
    static void Main(string[] args)
    {
        Program Program = new(); // Dependency injection


        Program.doPrintMultiplicationTable(gridNumbers: 10, gridDecimals: 3, gridSpacing: " ");

        Console.WriteLine();

        Program.doPrintCombinations(countTo: 5, sum: 5, withLines: true);

        Console.ReadLine();
    }

    /// <summary>
    /// Multiplication table printer
    /// </summary>
    public void doPrintMultiplicationTable(int gridNumbers, int gridDecimals, string gridSpacing = " ")
    {
        Program Program = new(); // Dependency injection

        // Grid loop
        for (int i = 1; i <= gridNumbers; i++)
        {
            int[] f = Program.intArr_getForMultiplication(i, gridNumbers);

            for (int j = 1; j <= gridNumbers; j++)
            {
                // Count spaces, and repeat spaces
                string spaces = Program.str_getDecimalSpaces(f[j], gridDecimals);

                // Write number
                Console.Write(spaces + f[j] + gridSpacing);
            }

            Console.WriteLine();
        }
    }

    private int[] intArr_getForMultiplication(int left, int right)
    {
        int[] _result = new int[right + 1];

        for (int i = 0; i <= right; i++)
            _result[i] = left * i;

        return _result;
    }

    private string str_getDecimalSpaces(int number, int decimals)
    {
        return new(' ', decimals - number.ToString().Length);
    }


    /// <summary>
    /// Combinations printer
    /// </summary>
    public void doPrintCombinations(int countTo, int sum, bool withLines = true)
    {
        Console.WriteLine("Possible combinations from 1 to 6 that sum to 5.");

        foreach (string combination in strArr_getPossibleCombinations(countTo, sum, withLines))
        {
            if (combination != null)
                Console.WriteLine(combination);
        }
    }

    private string[] strArr_getPossibleCombinations(int countTo, int sum, bool withLines = true)
    {
        string[] _result = new string[countTo * countTo];

        for (int i = 0; i <= countTo; i++)
        {
            for (int j = 0; j <= countTo; j++)
            {
                if (i + j == sum)
                {
                    // Get both combinations
                    _result[i * j] = $"({i},{j})\n({j},{i})";
                }
            }
        }

        return _result;
    }

    // --
}
*/