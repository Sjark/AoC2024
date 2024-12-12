namespace AoC2024.Solutions;

public class Day12 : ISolution
{
    private readonly string[] _input;

    public Day12()
    {
        _input = File.ReadAllLines("Solutions/Day12Input.txt");
    }

    public string PartOne()
    {
        HashSet<Coord> visited = [];
        var result = 0L;

        for (var y = 0; y < _input.Length; y++)
        {
            for (var x = 0; x < _input[y].Length; x++)
            {
                var (area, perimeter) = CalculateAreaAndPerimeter(_input[y][x], new Coord(x, y), visited);
                result += area * perimeter;
            }
        }

        return result.ToString();
    }

    public string PartTwo()
    {
        return "Not implemented";
    }

    private (int Area, int Perimeter) CalculateAreaAndPerimeter(char v, Coord coord, HashSet<Coord> visited)
    {
        if (coord.X < 0 || coord.Y < 0 || coord.X >= _input[0].Length || coord.Y >= _input.Length)
        {
            return (0, 1);
        }

        if (v == _input[coord.Y][coord.X] && visited.Contains(coord))
        {
            return (0, 0);
        }

        if (_input[coord.Y][coord.X] != v)
        {
            return (0, 1);
        }
        else
        {
            visited.Add(coord);
            var (leftArea, leftPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X - 1, coord.Y), visited);
            var (upArea, upPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X, coord.Y - 1), visited);
            var (rightArea, rightPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X + 1, coord.Y), visited);
            var (downArea, downPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X, coord.Y + 1), visited);

            return (leftArea + upArea + rightArea + downArea + 1, leftPerimeter + upPerimeter + rightPerimeter + downPerimeter);
        }
    }
}
