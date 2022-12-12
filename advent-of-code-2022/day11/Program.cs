using System.Linq;

Console.WriteLine(" Advent of Code 2022");

Console.WriteLine("------- Day 11 -------");

var result1 = FindMonkeyBusinessLevel();
Console.WriteLine($"Day 11-1: {result1}");

var result2 = FindMonkeyBusinessLevel2();
Console.WriteLine($"Day 11-2: {result2}");

UInt128 FindMonkeyBusinessLevel()
{
    Monkey[] monkeys = ParseFile();

    return CalculateMonkeyBusines(monkeys, 20, worry => worry / 3);

}

UInt128 FindMonkeyBusinessLevel2()
{
    Monkey[] monkeys = ParseFile();

    var modulo = monkeys.Aggregate((UInt128)1, (a, m) => a * m.DivideBy);

    return CalculateMonkeyBusines(monkeys, 10_000, worry => worry % modulo);
}


static UInt128 CalculateMonkeyBusines(Monkey[] monkeys, int rounds, Func<UInt128, UInt128> reduceWorry)
{
    var monkeyMap = monkeys.ToDictionary(m => m.Id);
    for (int i = 0; i < rounds; i++)
    {
        // round
        foreach (var monkey in monkeys)
        {
            while (monkey.HasItems())
            {
                var worryLevel = monkey.Inspect();

                worryLevel = reduceWorry(worryLevel);

                var targetMonkey = monkeyMap[monkey.ChooseMonkeyToThrow(worryLevel)];

                targetMonkey.Grab(worryLevel);
            }
        }
    }

    monkeys = monkeys.OrderByDescending(m => m.InspectionsCount).ToArray();

    var results = monkeys.Take(2).Select(m => (UInt128)m.InspectionsCount);

    return results.First() * results.Last();
}

Monkey[] ParseFile()
{
    return File
        .ReadAllLines("./input.txt")
        .Chunk(7)
        .Select(lines =>
        {
            var monkeyId = UInt128.Parse(lines[0].Substring(7, 1));
            var startingItems = lines[1].Split(':')[1].Split(',').Select(UInt128.Parse).ToList();
            var operation = ParseOperation(lines[2]);
            var divideBy = GetNumber(lines[3]);
            var whenPassedThrowTo = GetNumber(lines[4]);
            var whenFailedThrowTo = GetNumber(lines[5]);

            return new Monkey
            (
                monkeyId,
                startingItems,
                operation,
                divideBy,
                whenPassedThrowTo,
                whenFailedThrowTo
            );
        })
        .ToArray();
}

Func<UInt128, UInt128> ParseOperation(string line)
{
    var lastOperator = line.Substring(line.LastIndexOf(' ') + 1);
    if (lastOperator == "old")
    {
        return line.Contains('+')
            ? (UInt128 worry) => worry + worry
            : (UInt128 worry) => worry * worry;
    }
    else
    {
        var value = UInt128.Parse(lastOperator);
        return line.Contains('+')
            ? (UInt128 worry) => worry + value
            : (UInt128 worry) => worry * value;
    }
}

UInt128 GetNumber(string line)
{
    return UInt128.Parse(line.Substring(line.LastIndexOf(' ') + 1));
}


class Monkey
{
    public Monkey(UInt128 id, IEnumerable<UInt128> items, Func<UInt128, UInt128> operation, UInt128 divideBy, UInt128 whenPassedThrowTo, UInt128 whenFailedThrowTo)
    {
        Id = id;
        Items = new Queue<UInt128>(items);
        Operation = operation;
        DivideBy = divideBy;
        WhenPassedThrowTo = whenPassedThrowTo;
        WhenFailedThrowTo = whenFailedThrowTo;
    }

    public UInt128 Id { get; private set; }
    public Queue<UInt128> Items { get; private set; }
    public Func<UInt128, UInt128> Operation { get; private set; }
    public UInt128 DivideBy { get; private set; }
    public UInt128 WhenPassedThrowTo { get; private set; }
    public UInt128 WhenFailedThrowTo { get; private set; }
    public UInt128 InspectionsCount { get; private set; }

    public void Grab(UInt128 itemWorryLevel) => Items.Enqueue(itemWorryLevel);

    public bool HasItems() => Items.Any();

    public UInt128 Inspect()
    {
        InspectionsCount++;

        var itemWorryLevel = Items.Dequeue();

        return Operation(itemWorryLevel);
    }

    public UInt128 ChooseMonkeyToThrow(UInt128 itemWorryLevel)
    {
        return itemWorryLevel % DivideBy == 0 ? WhenPassedThrowTo : WhenFailedThrowTo;
    }
}


// Advent of Code 2022
//-------Day 11--------
//Day 11 - 1: 54036
//Day 11 - 2: 13237873355