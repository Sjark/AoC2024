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
        HashSet<Coord> visited = [];
        var result = 0L;

        for (var y = 0; y < _input.Length; y++)
        {
            for (var x = 0; x < _input[y].Length; x++)
            {
                Dictionary<Coord, HashSet<PerimeterSides>> perimeters = [];
                var (area, _) = CalculateAreaAndPerimeter(_input[y][x], new Coord(x, y), visited, perimeters);
                var numWalls = CalculateWalls(perimeters);
                result += area * numWalls;
            }
        }

        return result.ToString();
    }

    private int CalculateWalls(Dictionary<Coord, HashSet<PerimeterSides>> perimeters)
    {
        if (perimeters.Count == 0)
        {
            return 0;
        }

        var deletedCoords = new Dictionary<Coord, HashSet<PerimeterSides>>();
        var walls = 0;
        var coord = perimeters.First();
        var direction = coord.Value.First();

        if (coord.Value.Count == 3)
        {
            if (!coord.Value.Contains(PerimeterSides.Left))
            {
                direction = PerimeterSides.Top;
            }
            else if (!coord.Value.Contains(PerimeterSides.Top))
            {
                direction = PerimeterSides.Right;
            }
            else if (!coord.Value.Contains(PerimeterSides.Right))
            {
                direction = PerimeterSides.Bottom;
            }
            else
            {
                direction = PerimeterSides.Left;
            }
        }

        while (perimeters.Count > 0)
        {
            if (coord.Value.Count == 4)
            {
                deletedCoords[coord.Key] = [PerimeterSides.Left, PerimeterSides.Top, PerimeterSides.Right, PerimeterSides.Bottom];
                walls += 4;
                perimeters.Remove(coord.Key);
                if (perimeters.Count > 0)
                {
                    coord = perimeters.First();
                    direction = coord.Value.First();
                }
                continue;
            }

            //if (perimeters.Count == 1)
            //{
            //    walls += coord.Value.Count - 1;
            //    break;
            //}

            Coord? nextCoord = null;

            switch (direction)
            {
                case PerimeterSides.Left:
                    DeleteCoord(deletedCoords, coord, direction);

                    if (coord.Value.Any(a => a == PerimeterSides.Top))
                    {
                        walls++;
                        direction = PerimeterSides.Top;
                        continue;
                    }

                    nextCoord = perimeters
                        .FirstOrDefault(a => a.Key == new Coord(coord.Key.X, coord.Key.Y - 1) && a.Value.Any(b => b == direction))
                        .Key;

                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Top;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y + 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (
                            nextCoord != null
                        )
                        {
                            walls++;
                        }
                    }
                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Bottom;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y - 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (nextCoord != null || deletedCoords.Any(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y - 1) && a.Value.Any(b => b == direction)))
                        {
                            walls++;
                        }
                    }
                    break;
                case PerimeterSides.Top:
                    DeleteCoord(deletedCoords, coord, direction);

                    if (coord.Value.Any(a => a == PerimeterSides.Right))
                    {
                        walls++;
                        direction = PerimeterSides.Right;
                        continue;
                    }

                    nextCoord = perimeters
                        .FirstOrDefault(a => a.Key == new Coord(coord.Key.X + 1, coord.Key.Y) && a.Value.Any(b => b == direction))
                        .Key;

                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Right;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y - 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (
                            nextCoord != null
                        )
                        {
                            walls++;
                        }
                    }
                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Left;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X + 1, coord.Key.Y - 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (nextCoord != null || deletedCoords.Any(a => a.Key == new Coord(coord.Key.X + 1, coord.Key.Y - 1) && a.Value.Any(b => b == direction)))
                        {
                            walls++;
                        }
                    }
                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Right;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y - 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (
                            nextCoord != null
                        )
                        {
                            walls++;
                        }
                    }
                    break;
                case PerimeterSides.Right:
                    DeleteCoord(deletedCoords, coord, direction);

                    if (coord.Value.Any(a => a == PerimeterSides.Bottom))
                    {
                        walls++;
                        direction = PerimeterSides.Bottom;
                        continue;
                    }

                    nextCoord = perimeters
                        .FirstOrDefault(a => a.Key == new Coord(coord.Key.X, coord.Key.Y + 1) && a.Value.Any(b => b == direction))
                        .Key;

                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Bottom;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X + 1, coord.Key.Y - 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (
                            nextCoord != null
                        )
                        {
                            walls++;
                        }
                    }
                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Top;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X + 1, coord.Key.Y + 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (nextCoord != null || deletedCoords.Any(a => a.Key == new Coord(coord.Key.X + 1, coord.Key.Y + 1) && a.Value.Any(b => b == direction)))
                        {
                            walls++;
                        }
                    }
                    break;
                case PerimeterSides.Bottom:
                    DeleteCoord(deletedCoords, coord, direction);

                    if (coord.Value.Any(a => a == PerimeterSides.Left))
                    {
                        walls++;
                        direction = PerimeterSides.Left;
                        continue;
                    }

                    nextCoord = perimeters
                        .FirstOrDefault(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y) && a.Value.Any(b => b == direction))
                        .Key;

                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Left;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X + 1, coord.Key.Y + 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (
                            nextCoord != null
                        )
                        {
                            walls++;
                        }
                    }
                    if (nextCoord == null)
                    {
                        direction = PerimeterSides.Right;
                        nextCoord = perimeters
                            .FirstOrDefault(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y + 1) && a.Value.Any(b => b == direction))
                            .Key;

                        if (nextCoord != null || deletedCoords.Any(a => a.Key == new Coord(coord.Key.X - 1, coord.Key.Y + 1) && a.Value.Any(b => b == direction)))
                        {
                            walls++;
                        }
                    }
                    break;
            }

            if (coord.Value.Count == 0)
            {
                perimeters.Remove(coord.Key);
            }

            if (perimeters.Count == 0)
            {
                break;
            }

            if (nextCoord == null)
            {
                coord = perimeters.First();
                direction = coord.Value.First();

                if (coord.Value.Count == 3)
                {
                    if (!coord.Value.Contains(PerimeterSides.Left))
                    {
                        direction = PerimeterSides.Top;
                    }
                    else if (!coord.Value.Contains(PerimeterSides.Top))
                    {
                        direction = PerimeterSides.Right;
                    }
                    else if (!coord.Value.Contains(PerimeterSides.Right))
                    {
                        direction = PerimeterSides.Bottom;
                    }
                    else
                    {
                        direction = PerimeterSides.Left;
                    }
                }
            }
            else
            {
                coord = new KeyValuePair<Coord, HashSet<PerimeterSides>>(nextCoord, perimeters[nextCoord]);
            }
        }

        return walls;
    }

    private static void DeleteCoord(
        Dictionary<Coord, HashSet<PerimeterSides>> deletedCoords,
        KeyValuePair<Coord, HashSet<PerimeterSides>> coord,
        PerimeterSides perimeterSide
    )
    {
        if (deletedCoords.TryGetValue(coord.Key, out var deletedPerimeters))
        {
            deletedPerimeters.Add(perimeterSide);
        }
        else
        {
            deletedCoords[coord.Key] = [perimeterSide];
        }
        coord.Value.Remove(perimeterSide);
    }

    private (int Area, int Perimeter) CalculateAreaAndPerimeter(
        char v,
        Coord coord,
        HashSet<Coord> visited,
        Dictionary<Coord, HashSet<PerimeterSides>>? perimeters = null,
        Coord? lastCoord = null
    )
    {
        if (coord.X < 0 || coord.Y < 0 || coord.X >= _input[0].Length || coord.Y >= _input.Length)
        {
            if (perimeters != null && lastCoord != null)
            {
                var side =
                    coord.X < 0 ? PerimeterSides.Left
                    : coord.Y < 0 ? PerimeterSides.Top
                    : coord.X >= _input[0].Length ? PerimeterSides.Right
                    : PerimeterSides.Bottom;

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
            if (perimeters != null && lastCoord != null)
            {
                var side =
                    lastCoord.X < coord.X ? PerimeterSides.Right
                    : lastCoord.X > coord.X ? PerimeterSides.Left
                    : lastCoord.Y < coord.Y ? PerimeterSides.Bottom
                    : PerimeterSides.Top;

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
            var (leftArea, leftPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X - 1, coord.Y), visited, perimeters, coord);
            var (upArea, upPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X, coord.Y - 1), visited, perimeters, coord);
            var (rightArea, rightPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X + 1, coord.Y), visited, perimeters, coord);
            var (downArea, downPerimeter) = CalculateAreaAndPerimeter(v, new Coord(coord.X, coord.Y + 1), visited, perimeters, coord);

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
