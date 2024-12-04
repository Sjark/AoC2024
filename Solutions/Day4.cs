namespace AoC2024;

public class Day4 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day4Input.txt");
        var matrix = new char[input[0].Length, input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                matrix[i, j] = line[j];
            }
        }

        var result = 0;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 'X')
                {
                    result += GetNumberOfXmas(matrix, new Coord(j, i));
                }
            }
        }

        Console.WriteLine($"a: {result}");

        result = 0;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 'A')
                {
                    if (IsXmas(matrix, new Coord(j, i)))
                    {
                        result++;
                    }
                }
            }
        }

        Console.WriteLine($"b: {result}");
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
