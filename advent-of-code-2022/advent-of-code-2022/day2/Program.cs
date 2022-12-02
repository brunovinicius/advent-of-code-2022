using advent_of_code_2022.day2;

Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 2 ------");

var result1 = new RockPaperScisorsStrategyPointsCalculator().CalculateBasedOnSecondColumnBeingMyPlay();
Console.WriteLine($"Day 2-1: {result1}");

var result2 = new RockPaperScisorsStrategyPointsCalculator().CalculateBasedOnSecondColumnBeingExpectedResult();
Console.WriteLine($"Day 2-2: {result2}");

Console.ReadKey();