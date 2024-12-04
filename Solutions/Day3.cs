using System.Text.RegularExpressions;

namespace AoC2024.Solutions;

public partial class Day3 : ISolution
{
    private readonly string _input;

    public Day3()
    {
        _input = File.ReadAllText("Solutions/Day3Input.txt").ReplaceLineEndings("");
    }

    public string PartOne()
    {
        var multiplications = FindMultiplications().Matches(_input);

        var result = 0;

        foreach (var multiplication in multiplications)
        {
            var multiplyString = multiplication.ToString();

            if (multiplyString != null)
            {
                result += Multiply(multiplyString);
            }
        }

        return result.ToString();
    }

    public string PartTwo()
    {
        var result = 0;
        var does = true;

        foreach (var match in FindMultiplicationsDoesAndDonts().EnumerateMatches(_input))
        {
            var matchWord = _input.Substring(match.Index, match.Length);

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

        return result.ToString();
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
