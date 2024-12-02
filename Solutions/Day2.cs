namespace AoC2024;

public class Day2 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day2Input.txt");

        var safe = 0;

        foreach (var line in input)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToList();

            if (IsSafe(numbers))
            {
                safe++;
            }
        }

        Console.WriteLine($"a: {safe}");

        safe = 0;

        foreach (var line in input)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToList();

            var isSafe = IsSafe(numbers);

            if (!isSafe)
            {
                var indexToRemove = 0;

                while (!isSafe && indexToRemove < numbers.Count)
                {
                    var numCopy = numbers.ToList();
                    numCopy.RemoveAt(indexToRemove);
                    isSafe = IsSafe(numCopy);
                    indexToRemove++;
                }
            }

            if (isSafe)
            {
                safe++;
            }
        }

        Console.WriteLine($"b: {safe}");
    }

    private bool IsSafe(List<int> numbers)
    {
        var isIncreasing = numbers[0] < numbers[1];
        var isSafe = true;

        for (var i = 0; i < numbers.Count - 1; i++)
        {
            var diff = isIncreasing ? numbers[i + 1] - numbers[i] : numbers[i] - numbers[i + 1];

            if (diff < 1 || diff > 3)
            {
                isSafe = false;
                break;
            }
        }

        return isSafe;
    }
}
