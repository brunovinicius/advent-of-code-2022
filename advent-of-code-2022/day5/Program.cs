Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 5 ------");

var result1 = SortSuplyStacksCrateMOver9000();
Console.WriteLine($"Day 5-1: {result1}");

var result2 = SortSuplyStacksCrateMOver9001();
Console.WriteLine($"Day 5-2: {result2}");

Console.ReadKey();

string SortSuplyStacksCrateMOver9000() {
    var lines = File.ReadAllLines("./input.txt");

    var rawStacks = lines
        .TakeWhile(l => l != "")
        .Reverse()
        .Select(l =>
            l.Chunk(4)
                .Select(chunk => new string(chunk).Trim())
                .ToArray()
        )
        .Skip(1)
        .ToArray();
    
    var stacks = new Dictionary<int, Stack<string>>();
    for (int i = 0; i < rawStacks[0].Length; i++)
    {
        var stack = new Stack<string>();
        for (int j = 0; j < rawStacks.Length; j++)
        {
            var crate = rawStacks[j][i];
            if (crate != "")
            {
                stack.Push(crate);
            }
        }

        stacks.Add(i + 1, stack);
    }


    var procedures = lines
        .SkipWhile(l => l != "")
        .Skip(1)
        .Select(l => l.Replace("move ", "").Replace("from ", "").Replace("to ", ""))
        .Select(l => l.Split(' ').Select(s => int.Parse(s)).ToArray())
        .Select(s => new { Count = s[0], From = s[1], To = s[2] });


    foreach (var procedure in procedures)
    {
        var from = stacks[procedure.From];
        var to = stacks[procedure.To];

        for (int i = 0; i < procedure.Count; i++)
        {
            to.Push(from.Pop());
        }
    }

    var result = "";
    foreach(var stack in stacks.OrderBy(s => s.Key))
    {
        result += stack.Value.Peek();
    }

    return result.Replace("[", "").Replace("]", "");
}

string SortSuplyStacksCrateMOver9001()
{
    var lines = File.ReadAllLines("./input.txt");

    var rawStacks = lines
        .TakeWhile(l => l != "")
        .Reverse()
        .Select(l =>
            l.Chunk(4)
                .Select(chunk => new string(chunk).Trim())
                .ToArray()
        )
        .Skip(1)
        .ToArray();

    var stacks = new Dictionary<int, Stack<string>>();
    for (int i = 0; i < rawStacks[0].Length; i++)
    {
        var stack = new Stack<string>();
        for (int j = 0; j < rawStacks.Length; j++)
        {
            var crate = rawStacks[j][i];
            if (crate != "")
            {
                stack.Push(crate);
            }
        }

        stacks.Add(i + 1, stack);
    }


    var procedures = lines
        .SkipWhile(l => l != "")
        .Skip(1)
        .Select(l => l.Replace("move ", "").Replace("from ", "").Replace("to ", ""))
        .Select(l => l.Split(' ').Select(s => int.Parse(s)).ToArray())
        .Select(s => new { Count = s[0], From = s[1], To = s[2] });


    foreach (var procedure in procedures)
    {
        var from = stacks[procedure.From];
        var to = stacks[procedure.To];

        var crates = from.PopMany(procedure.Count);

        to.PushMany(crates.Reverse());
    }

    var result = "";
    foreach (var stack in stacks.OrderBy(s => s.Key))
    {
        result += stack.Value.Peek();
    }

    return result.Replace("[", "").Replace("]", "");
}

static class StackExtensions
{
    public static IEnumerable<T> PopMany<T>(this Stack<T> stack, int count)
    {
        return Enumerable.Range(0, count).Select(x => stack.Pop());
    }

    public static void PushMany<T>(this Stack<T> stack, IEnumerable<T> values)
    {
        foreach (var value in values) stack.Push(value);
    }
}