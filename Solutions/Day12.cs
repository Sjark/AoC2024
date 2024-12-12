namespace AoC2024.Solutions;

public class Day12 : ISolution
{
    private readonly string[] _input;

    public Day12()
    {
        _input = File.ReadAllLines("Solutions/Day12InputExample.txt");
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
        HashSet<Coord> visited = [];
        var result = 0L;

        for (var y = 0; y < _input.Length; y++)
        {
            for (var x = 0; x < _input[y].Length; x++)
            {
                Dictionary<Coord, List<PerimeterSides>> perimeters = [];
                var (area, perimeter) = CalculateAreaAndPerimeter(_input[y][x], new Coord(x, y), visited, perimeters);
                result += area * perimeter;
            }
        }

        return result.ToString();
    }

    private (int Area, int Perimeter) CalculateAreaAndPerimeter(
        char v,
        Coord coord,
        HashSet<Coord> visited,
        Dictionary<Coord, List<PerimeterSides>>? perimeters = null
    )
    {
        if (coord.X < 0 || coord.Y < 0 || coord.X >= _input[0].Length || coord.Y >= _input.Length)
        {
            if (perimeters != null)
            {
                var side =
                    coord.X < 0 ? PerimeterSides.Left
                    : coord.Y < 0 ? PerimeterSides.Top
                    : coord.X >= _input[0].Length ? PerimeterSides.Right
                    : PerimeterSides.Bottom;

                var lastCoord = side switch
                {
                    PerimeterSides.Left => new Coord(coord.X + 1, coord.Y),
                    PerimeterSides.Top => new Coord(coord.X, coord.Y + 1),
                    PerimeterSides.Right => new Coord(coord.X - 1, coord.Y),
                    PerimeterSides.Bottom => new Coord(coord.X, coord.Y - 1),
                    _ => throw new NotImplementedException(),
                };

                if (perimeters.TryGetValue(lastCoord, out var sides))
                {
                    sides.Add(side);
                }
                else
                {
                    perimeters[lastCoord] = [side];
                }
            }
            return (0, 1);
        }

        if (v == _input[coord.Y][coord.X] && visited.Contains(coord))
        {
            return (0, 0);
        }

        if (_input[coord.Y][coord.X] != v)
        {
            if (perimeters != null)
            {
                var side =
                    coord.Y > 0 && _input[coord.Y - 1][coord.X] == v ? PerimeterSides.Bottom
                    : coord.X > 0 && _input[coord.Y][coord.X - 1] == v ? PerimeterSides.Right
                    : coord.Y < _input.Length - 1 && _input[coord.Y + 1][coord.X] == v ? PerimeterSides.Top
                    : PerimeterSides.Left;

                var lastCoord = side switch
                {
                    PerimeterSides.Left => new Coord(coord.X + 1, coord.Y),
                    PerimeterSides.Top => new Coord(coord.X, coord.Y + 1),
                    PerimeterSides.Right => new Coord(coord.X - 1, coord.Y),
                    PerimeterSides.Bottom => new Coord(coord.X, coord.Y - 1),
                    _ => throw new NotImplementedException(),
                };

                if (perimeters.TryGetValue(lastCoord, out var sides))
                {
                    sides.Add(side);
                }
                else
                {
                    perimeters[lastCoord] = [side];
                }
            }

            return (0, 1);
        }
        else
        {
            visited.Add(coord);
            var (leftArea, leftPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X - 1, coord.Y), visited, perimeters);
            var (upArea, upPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X, coord.Y - 1), visited, perimeters);
            var (rightArea, rightPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X + 1, coord.Y), visited, perimeters);
            var (downArea, downPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X, coord.Y + 1), visited, perimeters);

            return (leftArea + upArea + rightArea + downArea + 1, leftPerimeter + upPerimeter + rightPerimeter + downPerimeter);
        }
    }
}

public enum PerimeterSides
{
    Left,
    Top,
    Right,
    Bottom,
}
