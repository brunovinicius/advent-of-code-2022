global using System;
global using System.Linq;

Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 3 ------");

var result1 = FindMissplacedRucksackItems();
Console.WriteLine($"Day 3-1: {result1}");

var result2 = FindElfGroupBadgePriorities();
Console.WriteLine($"Day 3-2: {result2}");

int FindMissplacedRucksackItems()
{
    var lines = File.ReadAllLines("./input.txt");
    
    return lines
        .Select(s => s.Chunk(s.Length / 2))
        .Select(chunks => chunks.First().Intersect(chunks.Last()).SingleOrDefault())
        .Where(c => c != 0)
        .Select(c =>
            c >= 'a'
                ? c - 'a' + 1
                : c - 'A' + 27
        )
        .Sum();
}


int FindElfGroupBadgePriorities()
{
    var lines = File.ReadAllLines("./input.txt");

    return lines
        .Chunk(3)        
        .Select(chunks =>
            chunks
                .Skip(1)
                .Aggregate(
                    new HashSet<char>(chunks.First().ToCharArray()),
                    (h, e) => { h.IntersectWith(e); return h; }
                )
                .SingleOrDefault()
        )
        .Where(c => c != 0)
        .Select(c =>
            c >= 'a'
                ? c - 'a' + 1
                : c - 'A' + 27
        )
        .Sum();
}