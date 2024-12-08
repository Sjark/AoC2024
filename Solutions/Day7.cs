using Microsoft.Diagnostics.Runtime.Utilities;

namespace AoC2024.Solutions;

public class Day7 : ISolution
{
    private readonly string[] _input;

    public Day7()
    {
        _input = File.ReadAllLines("Solutions/Day7Input.txt");
    }

    public string PartOne()
    {
        var result = 0UL;

        foreach (var line in _input)
        {
            if (IsValidEquation(line, out ulong equationResult, ['+', '*']))
            {
                result += equationResult;
            }
        }

        return result.ToString();
    }

    public string PartTwo()
    {
        var result = 0UL;

        foreach (var line in _input)
        {
            if (IsValidEquation(line, out ulong equationResult, ['+', '*', '|']))
            {
                result += equationResult;
            }
        }

        return result.ToString();
    }

    private bool IsValidEquation(string line, out ulong equationResult, char[] possibleOperators)
    {
        var lineSplitted = line.Split(':');
        equationResult = ulong.Parse(lineSplitted[0]);
        var equationNumbers = lineSplitted[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(a => ulong.Parse(a.Trim()))
            .ToArray();
        var numberOfCombinations = equationNumbers.Length - 1;
        List<char[]> combinations = [];
        GenerateOperatorCombinations(equationNumbers, 0, new char[numberOfCombinations], combinations, possibleOperators);

        foreach (var combination in combinations)
        {
            var currentResult = equationNumbers[0];

            for (int i = 0; i < combination.Length; i++)
            {
                if (combination[i] == '+')
                {
                    currentResult += equationNumbers[i + 1];
                }
                else if (combination[i] == '*')
                {
                    currentResult *= equationNumbers[i + 1];
                }
                else
                {
                    currentResult = Concat(currentResult, equationNumbers[i + 1]);
                }

                if (currentResult > equationResult)
                {
                    break;
                }
            }

            if (currentResult == equationResult)
            {
                return true;
            }
        }

        return false;
    }

    private void GenerateOperatorCombinations(
        ulong[] equationNumbers,
        int index,
        char[] currentOperators,
        List<char[]> combinations,
        char[] possibleOperators
    )
    {
        if (index == equationNumbers.Length - 1)
        {
            combinations.Add((char[])currentOperators.Clone());
            return;
        }

        foreach (var op in possibleOperators)
        {
            currentOperators[index] = op;
            GenerateOperatorCombinations(equationNumbers, index + 1, currentOperators, combinations, possibleOperators);
        }
    }

    static ulong Concat(ulong a, ulong b)
    {
        if (b < 10U)
            return 10UL * a + b;
        if (b < 100U)
            return 100UL * a + b;
        if (b < 1000U)
            return 1000UL * a + b;
        if (b < 10000U)
            return 10000UL * a + b;
        if (b < 100000U)
            return 100000UL * a + b;
        if (b < 1000000U)
            return 1000000UL * a + b;
        if (b < 10000000U)
            return 10000000UL * a + b;
        if (b < 100000000U)
            return 100000000UL * a + b;
        if (b < 1000000000U)
            return 1000000000UL * a + b;
        if (b < 10000000000U)
            return 10000000000UL * a + b;
        if (b < 100000000000U)
            return 100000000000UL * a + b;
        return 1000000000000U * a + b;
    }
}
