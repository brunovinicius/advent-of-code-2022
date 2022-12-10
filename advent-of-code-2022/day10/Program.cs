using System.Diagnostics;

Console.WriteLine(" Advent of Code 2022");
Console.WriteLine("------- Day 10 -------");

var (result1, result2) = puzzle1();
Console.WriteLine($"Day 10-1: {result1}");
Console.WriteLine($"Day 10-2: \n{result2}");

(int signal, string sprite) puzzle1()
{
    var instructions = File
        .ReadAllLines("./input.txt")
        .Select(l => l.Split(' '))
        .Select(p =>
            new Instruction() {
                Command = p[0],
                Value = p.Length == 2 ? int.Parse(p[1]) : 0
            }
        )
        .ToArray();

    var cycleNumber = 0;
    var context = new Context() { X = 1 };
    var processorFactory = new InstructionProcessorFactory();
    var results = new HashSet<( int cycle, int x, int partialResult )>();
    var render = new List<char>();

    foreach (var instruction in instructions)
    {
        var processor = processorFactory.CreateFor(instruction.Command);
        var processed = false;
        do
        {
            var adjustedCycle = cycleNumber % 40;
            var draw = context.X <= adjustedCycle + 1
                    && context.X >= adjustedCycle - 1;
            render.Add(draw ? '#' : '.');

            cycleNumber++;

            if ((cycleNumber + 20) % 40 == 0)
            {
                results.Add((
                    cycle: cycleNumber,
                    x: context.X,
                    partialResult: cycleNumber * context.X)
                );
            }


            processed = processor.Cycle(instruction, context);
        } while (!processed);
    }


    var sprite = render
        .Chunk(40)
        .Select(chunk => new String(chunk) + "\n")
        .Aggregate("", (accumulator, line) => accumulator + line);
    
    var signal = results.Sum(r => r.partialResult);

    return (signal, sprite);
}

class Context
{
    public int X { get; set; }
}

class Instruction
{
    public string Command { get; set; }
    public int? Value { get; set; }
}

interface IInstructionProcessor
{
    bool Cycle(Instruction instruction, Context ctx);
}

class NoopProcessor : IInstructionProcessor
{
    public bool Cycle(Instruction instruction, Context ctx) => true;
}

class AddXProcessor : IInstructionProcessor
{
    private int spentCycles = 0;
    public bool Cycle(Instruction instruction, Context ctx)
    {
        spentCycles++;

        if (spentCycles != 2)
            return false;

        ctx.X += instruction.Value ?? 0;

        return true;
    }
}

class InstructionProcessorFactory
{
    private static Dictionary<string, Func<IInstructionProcessor>> instructionProcessors =
        new Dictionary<string, Func<IInstructionProcessor>>()
        {
            { "addx", () => new AddXProcessor() },
            { "noop", () => new NoopProcessor() }
        };

    public IInstructionProcessor CreateFor(string command)
    {
        if (!instructionProcessors.ContainsKey(command))
        {
            throw new InvalidOperationException();
        }

        return instructionProcessors[command]();
    }
}


// Advent of Code 2022
//------- Day 10 --------
//Day 10 - 1: 13760
//Day 10 - 2: 
//###..####.#..#.####..##..###..####.####.
//#..#.#....#.#.....#.#..#.#..#.#....#....
//#..#.###..##.....#..#....#..#.###..###..
//###..#....#.#...#...#....###..#....#....
//#.#..#....#.#..#....#..#.#....#....#....
//#..#.#....#..#.####..##..#....####.#....