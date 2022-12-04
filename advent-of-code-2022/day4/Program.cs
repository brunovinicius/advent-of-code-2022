Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 4 ------");

var result1 = FindFullyOverlapingAssignmentPairs();
Console.WriteLine($"Day 4-1: {result1}");

var result2 = FindOverlapingAssignmentPairs();
Console.WriteLine($"Day 4-2: {result2}");


int FindFullyOverlapingAssignmentPairs()
{
    var lines = File.ReadAllLines("./input.txt");

    return lines
        .Select(l => l.Split(','))
        .Select(pair => pair
            .Select(s => s.Split('-'))
            .Select(n => new Range(int.Parse(n[0]), int.Parse(n[1])))
            .ToArray()
        )
        .Select(ranges => new { first = ranges[0], second = ranges[1] })
        .Select(assignmentPair =>
            assignmentPair.first.Contains(assignmentPair.second)
                || assignmentPair.second.Contains(assignmentPair.first)
        )
        .Where(result => result)
        .Count();
}

int FindOverlapingAssignmentPairs()
{
    var lines = File.ReadAllLines("./input.txt");

    return lines
        .Select(l => l.Split(','))
        .Select(pair => pair
            .Select(s => s.Split('-'))
            .Select(n => new Range(int.Parse(n[0]), int.Parse(n[1])))
            .ToArray()
        )
        .Select(ranges => new { first = ranges[0], second = ranges[1] })
        .Select(assignmentPair =>
            assignmentPair.first.Overlap(assignmentPair.second)
                || assignmentPair.second.Overlap(assignmentPair.first)
        )
        .Where(result => result)
        .Count();
}

class Range
{
    public readonly int Low, High;

    public Range(int low, int high)
    {
        Low = low;
        High = high;
    }

    public bool Contains(Range range) => Contains(range.Low) && Contains(range.High);

    public bool Contains(int number) => number >= Low && number <= High;

    public bool Overlap(Range range) => Contains(range.Low) || Contains(range.High);
}