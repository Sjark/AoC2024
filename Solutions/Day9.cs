namespace AoC2024.Solutions;

public class Day9 : ISolution
{
    private readonly string _input;

    public Day9()
    {
        _input = File.ReadAllText("Solutions/Day9Input.txt");
    }

    public string PartOne()
    {
        var fileSystem = DecompressFiles();
        var defragmentedDisk = DefragmentDisk(fileSystem);
        var checksum = CalculateChecksum(defragmentedDisk);

        return checksum.ToString();
    }

    public string PartTwo()
    {
        var fileSystem = DecompressFiles();
        var defragmentedDisk = DefragmentDiskWholeFiles(fileSystem);
        var checksum = CalculateChecksum(defragmentedDisk);

        return checksum.ToString();
    }

    private long CalculateChecksum(List<int?> filesystem)
    {
        var checksum = 0L;

        for (int i = 0; i < filesystem.Count; i++)
        {
            var fileId = filesystem[i];

            if (fileId == null)
            {
                continue;
            }

            checksum += i * (long)fileId;
        }

        return checksum;
    }

    private List<int?> DefragmentDisk(List<int?> fileSystem)
    {
        var endPointer = fileSystem.Count - 1;

        for (int i = 0; i < fileSystem.Count; i++)
        {
            if (fileSystem[i] == null)
            {
                for (int j = endPointer; j > i; j--)
                {
                    if (fileSystem[j] != null)
                    {
                        (fileSystem[i], fileSystem[j]) = (fileSystem[j], null);
                        endPointer = j - 1;
                        break;
                    }
                }
            }
        }

        return fileSystem;
    }

    private List<int?> DefragmentDiskWholeFiles(List<int?> fileSystem)
    {
        var holes = new List<(int Position, int HoleLength)>();

        for (int i = 0; i < fileSystem.Count; i++)
        {
            if (fileSystem[i] == null)
            {
                var holeLength = 1;

                while (fileSystem[i + holeLength] == null)
                {
                    holeLength++;
                }

                holes.Add((i, holeLength));

                i += holeLength;
            }
        }

        for (int i = fileSystem.Count - 1; i > 0; i--)
        {
            if (fileSystem[i] != null)
            {
                var fileId = fileSystem[i];
                var fileLength = 1;

                while (i - fileLength > 0 && fileSystem[i - fileLength] == fileId)
                {
                    fileLength++;
                }

                var matchingHole = holes
                    .Where(a => a.HoleLength >= fileLength && a.Position < i - fileLength)
                    .OrderBy(a => a.Position)
                    .FirstOrDefault();

                if (matchingHole.Position != 0 && matchingHole.HoleLength != 0)
                {
                    for (int j = 0; j < fileLength; j++)
                    {
                        (fileSystem[matchingHole.Position + j], fileSystem[i - j]) = (fileSystem[i - j], null);
                    }

                    holes.Add((i - fileLength, fileLength));

                    if (matchingHole.HoleLength > fileLength)
                    {
                        holes.Add((matchingHole.Position + fileLength, matchingHole.HoleLength - fileLength));
                    }

                    holes.Remove(matchingHole);
                }

                i -= (fileLength - 1);
            }
        }

        return fileSystem;
    }

    private List<int?> DecompressFiles()
    {
        List<int?> decompressed = [];
        var fileId = 0;
        var isFreeSpace = false;

        for (var i = 0; i < _input.Length; i++)
        {
            for (var j = 0; j < _input[i] - '0'; j++)
            {
                decompressed.Add(isFreeSpace ? null : fileId);
            }

            fileId += isFreeSpace ? 0 : 1;
            isFreeSpace = !isFreeSpace;
        }

        return decompressed;
    }
}
