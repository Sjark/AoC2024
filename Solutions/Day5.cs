using System.Drawing;
using Microsoft.Diagnostics.Tracing.Parsers.ClrPrivate;

namespace AoC2024.Solutions;

public class Day5 : ISolution
{
    private readonly List<string> _input = [];
    private readonly Dictionary<int, List<int>> _ordering = [];

    public Day5()
    {
        var input = File.ReadAllLines("Solutions/Day5Input.txt");

        foreach (var line in input)
        {
            if (line.Contains('|'))
            {
                var lineSplitted = line.Split('|').Select(int.Parse).ToArray();

                if (_ordering.TryGetValue(lineSplitted[0], out var value))
                {
                    value.Add(lineSplitted[1]);
                }
                else
                {
                    _ordering[lineSplitted[0]] = [lineSplitted[1]];
                }
            }
            else if (line.Contains(','))
            {
                _input.Add(line);
            }
        }
    }

    public string PartOne()
    {
        var sorted = _input.Where(a => IsSorted(a.Split(',').Select(int.Parse).ToArray())).ToList();

        var result = 0;

        foreach (var line in sorted)
        {
            var lineArray = line.Split(",").ToList();

            result += int.Parse(lineArray[lineArray.Count / 2]);
        }

        return result.ToString();
    }

    public string PartTwo()
    {
        var notSorted = _input
            .Select(a => a.Split(',').Select(int.Parse).ToArray())
            .Where(a => !IsSorted(a))
            .Select(Sort)
            .ToList();

        var result = 0;

        foreach (var line in notSorted)
        {
            result += line[line.Length / 2];
        }

        return result.ToString();
    }

    private int[] Sort(int[] pages)
    {
        for (int i = 0; i < pages.Length - 1; i++)
        {
            for (int j = 0; j < pages.Length - i - 1; j++)
            {
                if (_ordering.TryGetValue(pages[j + 1], out var value) && value.Contains(pages[j]))
                {
                    (pages[j + 1], pages[j]) = (pages[j], pages[j + 1]);
                }
            }
        }

        return pages;
    }

    private bool IsSorted(int[] pages)
    {
        for (var i = 0; i < pages.Length; i++)
        {
            var curPage = pages[i];

            for (var j = i + 1; j < pages.Length; j++)
            {
                var pageToCheckAgainst = pages[j];

                if (
                    _ordering.TryGetValue(pageToCheckAgainst, out var value)
                    && value.Contains(curPage)
                )
                {
                    return false;
                }
            }
        }

        return true;
    }
}
