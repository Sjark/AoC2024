namespace AoC2024.Solutions;

public class Day4 : ISolution
{
    private readonly string[] _input;

    public Day4()
    {
        _input = File.ReadAllLines("Solutions/Day4Input.txt");
    }

    public string PartOne()
    {
        var result = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[0].Length; j++)
            {
                if (_input[i][j] == 'X')
                {
                    result += GetNumberOfXmas(new Coord(j, i));
                }
            }
        }

        return result.ToString();
    }

    public string PartTwo()
    {
        var result = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[0].Length; j++)
            {
                if (_input[i][j] == 'A')
                {
                    if (IsXmas(new Coord(j, i)))
                    {
                        result++;
                    }
                }
            }
        }

        return result.ToString();
    }

    private bool IsXmas(Coord coord)
    {
        if (
            coord.X == 0
            || coord.Y == 0
            || coord.X == _input[0].Length - 1
            || coord.Y == _input.Length - 1
        )
        {
            return false;
        }

        if (
            _input[coord.Y - 1][coord.X - 1] == 'M'
            && _input[coord.Y - 1][coord.X + 1] == 'M'
            && _input[coord.Y + 1][coord.X - 1] == 'S'
            && _input[coord.Y + 1][coord.X + 1] == 'S'
        )
        {
            return true;
        }

        if (
            _input[coord.Y - 1][coord.X - 1] == 'M'
            && _input[coord.Y - 1][coord.X + 1] == 'S'
            && _input[coord.Y + 1][coord.X - 1] == 'M'
            && _input[coord.Y + 1][coord.X + 1] == 'S'
        )
        {
            return true;
        }

        if (
            _input[coord.Y - 1][coord.X - 1] == 'S'
            && _input[coord.Y - 1][coord.X + 1] == 'S'
            && _input[coord.Y + 1][coord.X - 1] == 'M'
            && _input[coord.Y + 1][coord.X + 1] == 'M'
        )
        {
            return true;
        }

        if (
            _input[coord.Y - 1][coord.X - 1] == 'S'
            && _input[coord.Y - 1][coord.X + 1] == 'M'
            && _input[coord.Y + 1][coord.X - 1] == 'S'
            && _input[coord.Y + 1][coord.X + 1] == 'M'
        )
        {
            return true;
        }

        return false;
    }

    private int GetNumberOfXmas(Coord coord)
    {
        var count = 0;

        // forward
        if (
            coord.X + 3 < _input[0].Length
            && _input[coord.Y][coord.X + 1] == 'M'
            && _input[coord.Y][coord.X + 2] == 'A'
            && _input[coord.Y][coord.X + 3] == 'S'
        )
        {
            count++;
        }

        // backward
        if (
            coord.X - 3 >= 0
            && _input[coord.Y][coord.X - 1] == 'M'
            && _input[coord.Y][coord.X - 2] == 'A'
            && _input[coord.Y][coord.X - 3] == 'S'
        )
        {
            count++;
        }

        // down
        if (
            coord.Y + 3 < _input.Length
            && _input[coord.Y + 1][coord.X] == 'M'
            && _input[coord.Y + 2][coord.X] == 'A'
            && _input[coord.Y + 3][coord.X] == 'S'
        )
        {
            count++;
        }

        // up
        if (
            coord.Y - 3 >= 0
            && _input[coord.Y - 1][coord.X] == 'M'
            && _input[coord.Y - 2][coord.X] == 'A'
            && _input[coord.Y - 3][coord.X] == 'S'
        )
        {
            count++;
        }

        // left up
        if (
            coord.Y - 3 >= 0
            && coord.X - 3 >= 0
            && _input[coord.Y - 1][coord.X - 1] == 'M'
            && _input[coord.Y - 2][coord.X - 2] == 'A'
            && _input[coord.Y - 3][coord.X - 3] == 'S'
        )
        {
            count++;
        }

        // right up
        if (
            coord.Y - 3 >= 0
            && coord.X + 3 < _input[0].Length
            && _input[coord.Y - 1][coord.X + 1] == 'M'
            && _input[coord.Y - 2][coord.X + 2] == 'A'
            && _input[coord.Y - 3][coord.X + 3] == 'S'
        )
        {
            count++;
        }

        // left down
        if (
            coord.Y + 3 < _input.Length
            && coord.X - 3 >= 0
            && _input[coord.Y + 1][coord.X - 1] == 'M'
            && _input[coord.Y + 2][coord.X - 2] == 'A'
            && _input[coord.Y + 3][coord.X - 3] == 'S'
        )
        {
            count++;
        }

        // right down
        if (
            coord.Y + 3 < _input.Length
            && coord.X + 3 < _input[0].Length
            && _input[coord.Y + 1][coord.X + 1] == 'M'
            && _input[coord.Y + 2][coord.X + 2] == 'A'
            && _input[coord.Y + 3][coord.X + 3] == 'S'
        )
        {
            count++;
        }

        return count;
    }
}
