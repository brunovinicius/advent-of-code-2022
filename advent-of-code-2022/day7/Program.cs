Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 7 ------");

var result1 = FindDirectoryWithAtMost100kInSize();
Console.WriteLine($"Day 7-1: {result1}");

var result2 = FindOptimalDirectoryForDeletion();
Console.WriteLine($"Day 7-2: {result2}");

int FindDirectoryWithAtMost100kInSize()
{
    var directorySizes = CalculateDirectorySizes();

    return directorySizes
        .Where(pair => pair.Value <= 100000)
        .Sum(pair => pair.Value);
}

int FindOptimalDirectoryForDeletion()
{   
    var directorySizes = CalculateDirectorySizes();

    var freeSpace = 70_000_000 - directorySizes["/"];
    var neededSpace = 30_000_000 - freeSpace;
    return directorySizes
        .OrderBy(pair => pair.Value)
        .First(pair => pair.Value >= neededSpace).Value;   
}

Dictionary<string, int> CalculateDirectorySizes() {
    var pwd = "";
    var pwdSize = 0;
    var directorySizes = new Dictionary<string, int>();

    foreach (var line in File.ReadLines("./input.txt"))
    {
        if (line == "$ cd ..") {
            UpdateUpperDirectorySizes(pwd, pwdSize, directorySizes);

            pwd = UpperDirectory(pwd);

            pwdSize = 0;
        }
        else if (line.StartsWith("$ cd"))
        {
            UpdateUpperDirectorySizes(pwd, pwdSize, directorySizes);

            var path = line.Substring(5);
            pwd += path.StartsWith('/') ? path : "/" + path;

            if (!directorySizes.ContainsKey(pwd))
                directorySizes[pwd] = 0;

            pwdSize = 0;
        } 
        else if (Char.IsDigit(line.First())) 
        {
            pwdSize += int.Parse(line.Split(' ').First());
        }
    } 

    UpdateUpperDirectorySizes(pwd, pwdSize, directorySizes);

    return directorySizes;
}

void UpdateUpperDirectorySizes(string pwd, int pwdSize, Dictionary<string, int> directorySizes)
{
    if (pwdSize <= 0 || !directorySizes.ContainsKey(pwd))
        return;

    directorySizes[pwd] += pwdSize;
    var tmp = UpperDirectory(pwd);
    while (tmp.Length >= 1)
    {
        directorySizes[tmp] += pwdSize;
        tmp = UpperDirectory(tmp);
    }
}

string UpperDirectory(string path) => path.Substring(0, path.LastIndexOf('/'));

// Advent of Code 2022
// ------ Day 7 ------
// Day 7-1: 1555642
// Day 7-2: 5974547