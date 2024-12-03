using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2024;

public partial class Day3 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllText("Solutions/Day3Input.txt").ReplaceLineEndings("");
        var regex = FindMultiplications();
        var multiplications = FindMultiplications().Matches(input);

        var result = 0;

        foreach (var multiplication in multiplications)
        {
            var multiplyString = multiplication.ToString();

            if (multiplyString != null)
            {
                result += Multiply(multiplyString);
            }
        }

        Console.WriteLine($"a: {result}");

        result = 0;
        var does = true;

        foreach (var match in FindMultiplicationsDoesAndDonts().EnumerateMatches(input))
        {
            var matchWord = input.Substring(match.Index, match.Length);

            if (matchWord == "do()")
            {
                does = true;
            }
            else if (matchWord == "don't()")
            {
                does = false;
            }
            else if (does)
            {
                result += Multiply(matchWord);
            }
        }

        Console.WriteLine($"b: {result}");
    }

    private int Multiply(string input)
    {
        var multi = input[4..(input.Length - 1)].Split(',').Select(int.Parse).ToList();

        return multi[0] * multi[1];
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    private static partial Regex FindMultiplications();

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)")]
    private static partial Regex FindMultiplicationsDoesAndDonts();
}
