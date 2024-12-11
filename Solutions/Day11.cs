namespace AoC2024.Solutions;

public class Day11 : ISolution
{
    private readonly List<long> _input;

    public Day11()
    {
        _input = [.. File.ReadAllText("Solutions/Day11Input.txt").Split(' ').Select(long.Parse)];
    }

    public string PartOne()
    {
        var stones = 0L;
        var cache = new Dictionary<StoneIter, long>();
        foreach (var stone in _input)
        {
            stones += CalculateStonesAfterBlinks(stone, 25, cache);
        }

        return stones.ToString();
    }

    public string PartTwo()
    {
        var stones = 0L;
        var cache = new Dictionary<StoneIter, long>();
        foreach (var stone in _input)
        {
            stones += CalculateStonesAfterBlinks(stone, 75, cache);
        }

        return stones.ToString();
    }

    private long CalculateStonesAfterBlinks(long stone, int n, Dictionary<StoneIter, long> cache)
    {
        if (n == 0)
        {
            return 1;
        }

        var stoneIter = new StoneIter(stone, n);

        if (cache.TryGetValue(stoneIter, out var stones))
        {
            return stones;
        }

        long result;

        if (stone == 0)
        {
            result = CalculateStonesAfterBlinks(1, n - 1, cache);
        }
        else
        {
            var stoneString = stone.ToString();

            if (stoneString.Length % 2 == 0)
            {
                result =
                    CalculateStonesAfterBlinks(long.Parse(stoneString.AsSpan()[..(stoneString.Length / 2)]), n - 1, cache)
                    + CalculateStonesAfterBlinks(long.Parse(stoneString.AsSpan()[(stoneString.Length / 2)..]), n - 1, cache);
            }
            else
            {
                result = CalculateStonesAfterBlinks(stone * 2024, n - 1, cache);
            }
        }

        cache.Add(stoneIter, result);

        return result;
    }
}

public record StoneIter(long Stone, int Iter);
