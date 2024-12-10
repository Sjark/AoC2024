namespace AoC2024.Solutions;

public class Day10 : ISolution
{
    private readonly string[] _input;

    public Day10()
    {
        _input = File.ReadAllLines("Solutions/Day10Input.txt");
    }

    public string PartOne()
    {
        var results = 0;

        List<Coord> startPositions = GetStartPositions();

        foreach (var position in startPositions)
        {
            HashSet<Coord> endPositions = [];
            GetTrailheadScore(position, endPositions);
            results += endPositions.Count;
        }

        return results.ToString();
    }

    public string PartTwo()
    {
        var results = 0;

        List<Coord> startPositions = GetStartPositions();

        foreach (var position in startPositions)
        {
            List<Coord> endPositions = [];
            GetTrailheadScore(position, endPositions);
            results += endPositions.Count;
        }

        return results.ToString();
    }

    private void GetTrailheadScore(Coord position, ICollection<Coord> endPositions)
    {
        var currentNumber = _input[position.Y][position.X] - '0';

        if (currentNumber == 9)
        {
            endPositions.Add(position);
            return;
        }

        if (position.Y - 1 >= 0 && _input[position.Y - 1][position.X] - '0' == currentNumber + 1)
        {
            GetTrailheadScore(new Coord(position.X, position.Y - 1), endPositions);
        }
        if (position.X - 1 >= 0 && _input[position.Y][position.X - 1] - '0' == currentNumber + 1)
        {
            GetTrailheadScore(new Coord(position.X - 1, position.Y), endPositions);
        }
        if (position.Y + 1 < _input.Length && _input[position.Y + 1][position.X] - '0' == currentNumber + 1)
        {
            GetTrailheadScore(new Coord(position.X, position.Y + 1), endPositions);
        }
        if (position.X + 1 < _input[position.Y].Length && _input[position.Y][position.X + 1] - '0' == currentNumber + 1)
        {
            GetTrailheadScore(new Coord(position.X + 1, position.Y), endPositions);
        }
    }

    private List<Coord> GetStartPositions()
    {
        List<Coord> startPositions = [];

        for (var y = 0; y < _input.Length; y++)
        {
            for (var x = 0; x < _input[y].Length; x++)
            {
                if (_input[y][x] == '0')
                {
                    startPositions.Add(new Coord(x, y));
                }
            }
        }

        return startPositions;
    }
}
