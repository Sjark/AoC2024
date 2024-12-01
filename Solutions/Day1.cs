namespace AoC2024;

public class Day1 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day1Input.txt");
        List<int> leftList = [];
        List<int> rightList = [];

        foreach (var item in input)
        {
            var nums = item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            leftList.Add(nums[0]);
            rightList.Add(nums[1]);
        }

        leftList.Sort();
        rightList.Sort();

        var totalDistance = 0;

        for (int i = 0; i < leftList.Count; i++)
        {
            totalDistance += int.Abs(leftList[i] - rightList[i]);
        }

        Console.WriteLine(totalDistance);

        List<int> leftListPart2 = [];
        Dictionary<int, int> rightListPart2 = [];

        foreach (var item in input)
        {
            var nums = item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            leftListPart2.Add(nums[0]);
            
            if (rightListPart2.TryGetValue(nums[1], out int value))
            {
                rightListPart2[nums[1]] = ++value;
            }
            else
            {
                rightListPart2[nums[1]] = 1;
            }
        }

        var result = 0;

        foreach (var num in leftListPart2)
        {
            if (rightListPart2.TryGetValue(num, out int value))
            {
                result += num * value;
            }
        }

        Console.WriteLine(result);
    }
}
