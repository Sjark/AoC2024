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
        foreach (var stone in _input)
        {
            CalculateStonesAfterBlinks(stone, 25, ref stones);
        }

        return stones.ToString();
    }

    public string PartTwo()
    {
        var stones = 0L;
        foreach (var stone in _input)
        {
            CalculateStonesAfterBlinks(stone, 75, ref stones);
        }

        return stones.ToString();
    }

    private void CalculateStonesAfterBlinks(long stone, int n, ref long stones)
    {
        if (n == 0)
        {
            stones++;
            return;
        }

        if (stone == 0)
        {
            CalculateStonesAfterBlinks(1, n - 1, ref stones);
        }
        else
        {
            var stoneString = stone.ToString();

            if (stoneString.Length % 2 == 0)
            {
                CalculateStonesAfterBlinks(long.Parse(stoneString.AsSpan()[..(stoneString.Length / 2)]), n - 1, ref stones);
                CalculateStonesAfterBlinks(long.Parse(stoneString.AsSpan()[(stoneString.Length / 2)..]), n - 1, ref stones);
            }
            else
            {
                CalculateStonesAfterBlinks(stone * 2024, n - 1, ref stones);
            }
        }
    }
}
