using Iced.Intel;

namespace AoC2024.Solutions;

public class Day6 : ISolution
{
    private readonly string[] _input;

    public Day6()
    {
        _input = File.ReadAllLines("Solutions/Day6Input.txt");
    }

    public string PartOne()
    {
        HashSet<Coord> visitedCoords = GetVisitedCoordinates();

        return visitedCoords.Count.ToString();
    }

    public string PartTwo()
    {
        var startPos = FindStartingPosition();
        var visitedCoordinates = GetVisitedCoordinates();

        var result = 0;
        for (var y = 0; y < _input.Length; y++)
        {
            for (var x = 0; x < _input[0].Length; x++)
            {
                var currentCoord = new Coord(x, y);

                if (visitedCoordinates.Contains(currentCoord) && _input[y][x] == '.')
                {
                    if (IsStuckInLoop(currentCoord, startPos))
                    {
                        result++;
                    }
                }
            }
        }

        return result.ToString();
    }

    private HashSet<Coord> GetVisitedCoordinates()
    {
        var visitedCoords = new HashSet<Coord>();
        var direction = 'N';
        var currentPosition = FindStartingPosition();
        visitedCoords.Add(currentPosition);

        var positionOutside = false;

        while (!positionOutside)
        {
            Coord newPos = CalculateNextPosition(currentPosition, direction);

            if (CheckBoundry(newPos))
            {
                if (_input[newPos.Y][newPos.X] == '#')
                {
                    direction = RotateDirection(direction);
                }
                else
                {
                    currentPosition = newPos;
                    visitedCoords.Add(currentPosition);
                }
            }
            else
            {
                positionOutside = true;
            }
        }

        return visitedCoords;
    }

    private Coord FindStartingPosition()
    {
        for (var y = 0; y < _input.Length; y++)
        {
            for (var x = 0; x < _input[0].Length; x++)
            {
                if (_input[y][x] == '^')
                {
                    return new Coord(x, y);
                }
            }
        }

        throw new Exception("Start position does not exist");
    }

    private bool IsStuckInLoop(Coord newBox, Coord startPos)
    {
        var visited = new HashSet<CoordWithDirection>();
        var currentPosition = startPos;
        var positionOutside = false;
        var direction = 'N';

        while (!positionOutside)
        {
            Coord newPos = CalculateNextPosition(currentPosition, direction);

            if (visited.Contains(new CoordWithDirection(newPos.X, newPos.Y, direction)))
            {
                return true;
            }

            if (CheckBoundry(newPos))
            {
                if (_input[newPos.Y][newPos.X] == '#' || newPos == newBox)
                {
                    direction = RotateDirection(direction);
                }
                else
                {
                    currentPosition = newPos;
                    visited.Add(new CoordWithDirection(newPos.X, newPos.Y, direction));
                }
            }
            else
            {
                positionOutside = true;
            }
        }

        return false;
    }

    private static char RotateDirection(char direction)
    {
        direction = direction switch
        {
            'N' => 'E',
            'E' => 'S',
            'S' => 'W',
            'W' => 'N',
            _ => throw new Exception("Not a valid direction")
        };
        return direction;
    }

    private static Coord CalculateNextPosition(Coord currentPosition, char direction)
    {
        return direction switch
        {
            'N' => new Coord(currentPosition.X, currentPosition.Y - 1),
            'E' => new Coord(currentPosition.X + 1, currentPosition.Y),
            'S' => new Coord(currentPosition.X, currentPosition.Y + 1),
            'W' => new Coord(currentPosition.X - 1, currentPosition.Y),
            _ => throw new Exception("Not a valid direction")
        };
    }

    private bool CheckBoundry(Coord currentPosition)
    {
        return currentPosition.Y >= 0
            && currentPosition.Y < _input.Length
            && currentPosition.X >= 0
            && currentPosition.X < _input[0].Length;
    }
}

public record CoordWithDirection(int X, int Y, char Direction);
