namespace AoC2024.Solutions;

public class Day8 : ISolution
{
    private readonly string[] _input;

    public Day8()
    {
        _input = File.ReadAllLines("Solutions/Day8Input.txt");
    }

    public string PartOne()
    {
        var satLocations = GetSateliteLocations();
        HashSet<Coord> result = [];

        foreach (var satLocation in satLocations)
        {
            if (satLocation.Value.Count == 1)
            {
                continue;
            }

            List<(Coord Satelite1, Coord Satelite2)> combinations = [];
            var tempCombinations = new Coord[2];
            GenerateSateliteCombinations([.. satLocation.Value], tempCombinations, 0, satLocation.Value.Count - 1, 0, combinations);

            foreach (var (Satelite1, Satelite2) in combinations)
            {
                if (Satelite1.X >= Satelite2.X && Satelite1.Y >= Satelite2.Y)
                {
                    var xDiff = Satelite1.X - Satelite2.X;
                    var yDiff = Satelite1.Y - Satelite2.Y;

                    var antiNode1 = new Coord(Satelite2.X - xDiff, Satelite2.Y - yDiff);
                    var antiNode2 = new Coord(Satelite1.X + xDiff, Satelite1.Y + yDiff);

                    if (antiNode1.X >= 0 && antiNode1.Y >= 0)
                    {
                        result.Add(antiNode1);
                    }

                    if (antiNode2.X < _input[0].Length && antiNode2.Y < _input.Length)
                    {
                        result.Add(antiNode2);
                    }
                }
                else if (Satelite2.X >= Satelite1.X && Satelite2.Y >= Satelite1.Y)
                {
                    var xDiff = Satelite2.X - Satelite1.X;
                    var yDiff = Satelite2.Y - Satelite1.Y;

                    var antiNode1 = new Coord(Satelite1.X - xDiff, Satelite1.Y - yDiff);
                    var antiNode2 = new Coord(Satelite2.X + xDiff, Satelite2.Y + yDiff);

                    if (antiNode1.X >= 0 && antiNode1.Y >= 0)
                    {
                        result.Add(antiNode1);
                    }

                    if (antiNode2.X < _input[0].Length && antiNode2.Y < _input.Length)
                    {
                        result.Add(antiNode2);
                    }
                }
                else if (Satelite1.X >= Satelite2.X && Satelite1.Y <= Satelite2.Y)
                {
                    var xDiff = Satelite1.X - Satelite2.X;
                    var yDiff = Satelite2.Y - Satelite1.Y;

                    var antiNode1 = new Coord(Satelite2.X - xDiff, Satelite2.Y + yDiff);
                    var antiNode2 = new Coord(Satelite1.X + xDiff, Satelite1.Y - yDiff);

                    if (antiNode1.X >= 0 && antiNode1.Y < _input.Length)
                    {
                        result.Add(antiNode1);
                    }

                    if (antiNode2.X < _input[0].Length && antiNode2.Y >= 0)
                    {
                        result.Add(antiNode2);
                    }
                }
                else
                {
                    var xDiff = Satelite2.X - Satelite1.X;
                    var yDiff = Satelite1.Y - Satelite2.Y;

                    var antiNode1 = new Coord(Satelite2.X - xDiff, Satelite2.Y - yDiff);
                    var antiNode2 = new Coord(Satelite1.X + xDiff, Satelite1.Y + yDiff);

                    if (antiNode1.X >= 0 && antiNode1.Y >= 0)
                    {
                        result.Add((antiNode1));
                    }

                    if (antiNode2.X < _input[0].Length && antiNode2.Y < _input.Length)
                    {
                        result.Add((antiNode2));
                    }
                }
            }
        }

        //foreach (var asdf in result)
        //{
        //    var newString = _input[asdf.Y].ToCharArray();
        //    newString[asdf.X] = '#';
        //    _input[asdf.Y] = new string(newString);
        //}

        //foreach (var line in _input)
        //{
        //    Console.WriteLine(line);
        //}

        return result.Count.ToString();
    }

    public string PartTwo()
    {
        var satLocations = GetSateliteLocations();
        HashSet<Coord> result = [];

        foreach (var satLocation in satLocations)
        {
            if (satLocation.Value.Count == 1)
            {
                continue;
            }

            foreach (var sat in satLocation.Value)
            {
                result.Add(sat);
            }

            List<(Coord Satelite1, Coord Satelite2)> combinations = [];
            var tempCombinations = new Coord[2];
            GenerateSateliteCombinations([.. satLocation.Value], tempCombinations, 0, satLocation.Value.Count - 1, 0, combinations);

            foreach (var (Satelite1, Satelite2) in combinations)
            {
                if (Satelite1.X >= Satelite2.X && Satelite1.Y >= Satelite2.Y)
                {
                    var xDiff = Satelite1.X - Satelite2.X;
                    var yDiff = Satelite1.Y - Satelite2.Y;

                    var antiNode1 = new Coord(Satelite2.X - xDiff, Satelite2.Y - yDiff);
                    var antiNode2 = new Coord(Satelite1.X + xDiff, Satelite1.Y + yDiff);

                    while (antiNode1.X >= 0 && antiNode1.Y >= 0)
                    {
                        result.Add(antiNode1);
                        antiNode1 = new Coord(antiNode1.X - xDiff, antiNode1.Y - yDiff);
                    }

                    while (antiNode2.X < _input[0].Length && antiNode2.Y < _input.Length)
                    {
                        result.Add(antiNode2);
                        antiNode2 = new Coord(antiNode2.X + xDiff, antiNode2.Y + yDiff);
                    }
                }
                else if (Satelite2.X >= Satelite1.X && Satelite2.Y >= Satelite1.Y)
                {
                    var xDiff = Satelite2.X - Satelite1.X;
                    var yDiff = Satelite2.Y - Satelite1.Y;

                    var antiNode1 = new Coord(Satelite1.X - xDiff, Satelite1.Y - yDiff);
                    var antiNode2 = new Coord(Satelite2.X + xDiff, Satelite2.Y + yDiff);

                    while (antiNode1.X >= 0 && antiNode1.Y >= 0)
                    {
                        result.Add(antiNode1);
                        antiNode1 = new Coord(antiNode1.X - xDiff, antiNode1.Y - yDiff);
                    }

                    while (antiNode2.X < _input[0].Length && antiNode2.Y < _input.Length)
                    {
                        result.Add(antiNode2);
                        antiNode2 = new Coord(antiNode2.X + xDiff, antiNode2.Y + yDiff);
                    }
                }
                else if (Satelite1.X >= Satelite2.X && Satelite1.Y <= Satelite2.Y)
                {
                    var xDiff = Satelite1.X - Satelite2.X;
                    var yDiff = Satelite2.Y - Satelite1.Y;

                    var antiNode1 = new Coord(Satelite2.X - xDiff, Satelite2.Y + yDiff);
                    var antiNode2 = new Coord(Satelite1.X + xDiff, Satelite1.Y - yDiff);

                    while (antiNode1.X >= 0 && antiNode1.Y < _input.Length)
                    {
                        result.Add(antiNode1);
                        antiNode1 = new Coord(antiNode1.X - xDiff, antiNode1.Y + yDiff);
                    }

                    while (antiNode2.X < _input[0].Length && antiNode2.Y >= 0)
                    {
                        result.Add(antiNode2);
                        antiNode2 = new Coord(antiNode2.X + xDiff, antiNode2.Y - yDiff);
                    }
                }
                else
                {
                    var xDiff = Satelite2.X - Satelite1.X;
                    var yDiff = Satelite1.Y - Satelite2.Y;

                    var antiNode1 = new Coord(Satelite2.X - xDiff, Satelite2.Y - yDiff);
                    var antiNode2 = new Coord(Satelite1.X + xDiff, Satelite1.Y + yDiff);

                    while (antiNode1.X >= 0 && antiNode1.Y >= 0)
                    {
                        result.Add(antiNode1);
                        antiNode1 = new Coord(antiNode1.X - xDiff, antiNode1.Y - yDiff);
                    }

                    while (antiNode2.X < _input[0].Length && antiNode2.Y < _input.Length)
                    {
                        result.Add(antiNode2);
                        antiNode2 = new Coord(antiNode2.X + xDiff, antiNode2.Y + yDiff);
                    }
                }
            }
        }

        //foreach (var asdf in result)
        //{
        //    var newString = _input[asdf.Y].ToCharArray();
        //    newString[asdf.X] = '#';
        //    _input[asdf.Y] = new string(newString);
        //}

        //foreach (var line in _input)
        //{
        //    Console.WriteLine(line);
        //}

        return result.Count.ToString();
    }

    private Dictionary<char, List<Coord>> GetSateliteLocations()
    {
        Dictionary<char, List<Coord>> satLocations = [];

        for (var y = 0; y < _input.Length; y++)
        {
            for (var x = 0; x < _input.Length; x++)
            {
                var point = _input[y][x];
                if (point != '.')
                {
                    if (satLocations.TryGetValue(point, out var value))
                    {
                        value.Add(new Coord(x, y));
                    }
                    else
                    {
                        satLocations[point] = [new Coord(x, y)];
                    }
                }
            }
        }

        return satLocations;
    }

    private void GenerateSateliteCombinations(
        Coord[] satelites,
        Coord[] currentCombinations,
        int startIndex,
        int endIndex,
        int currentIndex,
        List<(Coord Satelite1, Coord Satelite2)> combinations
    )
    {
        if (currentIndex == 2)
        {
            combinations.Add((currentCombinations[0], currentCombinations[1]));
            return;
        }

        for (var i = startIndex; i <= endIndex && endIndex - i + 1 >= 2 - currentIndex; i++)
        {
            currentCombinations[currentIndex] = satelites[i];
            GenerateSateliteCombinations(satelites, currentCombinations, i + 1, endIndex, currentIndex + 1, combinations);
        }
    }
}
