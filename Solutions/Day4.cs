namespace AoC2024.Solutions;

public class Day4 : ISolution
{
    private readonly char[,] _matrix;

    public Day4()
    {
        var input = File.ReadAllLines("Solutions/Day4Input.txt");
        _matrix = new char[input[0].Length, input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                _matrix[i, j] = line[j];
            }
        }
    }

    public string PartOne()
    {
        var result = 0;

        for (int i = 0; i < _matrix.GetLength(0); i++)
        {
            for (int j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] == 'X')
                {
                    result += GetNumberOfXmas(_matrix, new Coord(j, i));
                }
            }
        }

        return result.ToString();
    }

    public string PartTwo()
    {
        var result = 0;

        for (int i = 0; i < _matrix.GetLength(0); i++)
        {
            for (int j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] == 'A')
                {
                    if (IsXmas(_matrix, new Coord(j, i)))
                    {
                        result++;
                    }
                }
            }
        }

        return result.ToString();
    }

    private bool IsXmas(char[,] matrix, Coord coord)
    {
        if (
            coord.X == 0
            || coord.Y == 0
            || coord.X == matrix.GetLength(1) - 1
            || coord.Y == matrix.GetLength(0) - 1
        )
        {
            return false;
        }

        if (
            matrix[coord.Y - 1, coord.X - 1] == 'M'
            && matrix[coord.Y - 1, coord.X + 1] == 'M'
            && matrix[coord.Y + 1, coord.X - 1] == 'S'
            && matrix[coord.Y + 1, coord.X + 1] == 'S'
        )
        {
            return true;
        }

        if (
            matrix[coord.Y - 1, coord.X - 1] == 'M'
            && matrix[coord.Y - 1, coord.X + 1] == 'S'
            && matrix[coord.Y + 1, coord.X - 1] == 'M'
            && matrix[coord.Y + 1, coord.X + 1] == 'S'
        )
        {
            return true;
        }

        if (
            matrix[coord.Y - 1, coord.X - 1] == 'S'
            && matrix[coord.Y - 1, coord.X + 1] == 'S'
            && matrix[coord.Y + 1, coord.X - 1] == 'M'
            && matrix[coord.Y + 1, coord.X + 1] == 'M'
        )
        {
            return true;
        }

        if (
            matrix[coord.Y - 1, coord.X - 1] == 'S'
            && matrix[coord.Y - 1, coord.X + 1] == 'M'
            && matrix[coord.Y + 1, coord.X - 1] == 'S'
            && matrix[coord.Y + 1, coord.X + 1] == 'M'
        )
        {
            return true;
        }

        return false;
    }

    private int GetNumberOfXmas(char[,] matrix, Coord coord)
    {
        var count = 0;

        // forward
        if (
            coord.X + 3 < matrix.GetLength(1)
            && matrix[coord.Y, coord.X + 1] == 'M'
            && matrix[coord.Y, coord.X + 2] == 'A'
            && matrix[coord.Y, coord.X + 3] == 'S'
        )
        {
            count++;
        }

        // backward
        if (
            coord.X - 3 >= 0
            && matrix[coord.Y, coord.X - 1] == 'M'
            && matrix[coord.Y, coord.X - 2] == 'A'
            && matrix[coord.Y, coord.X - 3] == 'S'
        )
        {
            count++;
        }

        // down
        if (
            coord.Y + 3 < matrix.GetLength(0)
            && matrix[coord.Y + 1, coord.X] == 'M'
            && matrix[coord.Y + 2, coord.X] == 'A'
            && matrix[coord.Y + 3, coord.X] == 'S'
        )
        {
            count++;
        }

        // up
        if (
            coord.Y - 3 >= 0
            && matrix[coord.Y - 1, coord.X] == 'M'
            && matrix[coord.Y - 2, coord.X] == 'A'
            && matrix[coord.Y - 3, coord.X] == 'S'
        )
        {
            count++;
        }

        // left up
        if (
            coord.Y - 3 >= 0
            && coord.X - 3 >= 0
            && matrix[coord.Y - 1, coord.X - 1] == 'M'
            && matrix[coord.Y - 2, coord.X - 2] == 'A'
            && matrix[coord.Y - 3, coord.X - 3] == 'S'
        )
        {
            count++;
        }

        // right up
        if (
            coord.Y - 3 >= 0
            && coord.X + 3 < matrix.GetLength(1)
            && matrix[coord.Y - 1, coord.X + 1] == 'M'
            && matrix[coord.Y - 2, coord.X + 2] == 'A'
            && matrix[coord.Y - 3, coord.X + 3] == 'S'
        )
        {
            count++;
        }

        // left down
        if (
            coord.Y + 3 < matrix.GetLength(0)
            && coord.X - 3 >= 0
            && matrix[coord.Y + 1, coord.X - 1] == 'M'
            && matrix[coord.Y + 2, coord.X - 2] == 'A'
            && matrix[coord.Y + 3, coord.X - 3] == 'S'
        )
        {
            count++;
        }

        // right down
        if (
            coord.Y + 3 < matrix.GetLength(0)
            && coord.X + 3 < matrix.GetLength(1)
            && matrix[coord.Y + 1, coord.X + 1] == 'M'
            && matrix[coord.Y + 2, coord.X + 2] == 'A'
            && matrix[coord.Y + 3, coord.X + 3] == 'S'
        )
        {
            count++;
        }

        return count;
    }
}
