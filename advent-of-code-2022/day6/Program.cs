Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 6 ------");

var result1 = TunningProblemStartOfPacket();
Console.WriteLine($"Day 6-1: {result1}");

var result2 = TunningProblemStartOfMessage();
Console.WriteLine($"Day 6-2: {result2}");

int TunningProblemStartOfPacket()
{
    var input = File.ReadAllLines("./input.txt").Single().ToArray();

    int i = 3;
    var set = new HashSet<char>();
    for (; i < input.Length; i++)
    {
        set.Clear();
        set.Add(input[i]);
        set.Add(input[i - 1]);
        set.Add(input[i - 2]);
        set.Add(input[i - 3]);

        if (set.Count == 4) break;
    }

    return i + 1;
}

int TunningProblemStartOfMessage()
{
    var input = File.ReadAllLines("./input.txt").Single().ToArray();

    const int messageSize = 14;
    var i = Enumerable
        .Range(0, input.Length - messageSize)
        .TakeWhile(i =>
            input
                .Skip(i)
                .Take(messageSize)
                .Distinct()
                .Count() != messageSize
        )
        .Last();

    return i + messageSize + 1;
}