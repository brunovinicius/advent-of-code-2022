using System.Collections.Concurrent;

Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 8 ------");

var result1 = FindTreesWithClearView();
Console.WriteLine($"Day 8-1: {result1}");

var result2 = FindTreeWithHighestScenicScore();
Console.WriteLine($"Day 8-2: {result2}");

int FindTreesWithClearView()
{
    var matrix = ReadMatrix();
    var coords = CreateCoordenates(matrix);

    return coords
        .AsParallel()
        .Select((coord) =>
        {
            return IsVisibleLeft(matrix, coord)
                || IsVisibleRight(matrix, coord)
                || IsVisibleAbove(matrix, coord)
                || IsVisibleBelow(matrix, coord)
                ? 1 : 0;
        })
        .Sum();
}

int FindTreeWithHighestScenicScore()
{
    var matrix = ReadMatrix();
    var coords = CreateCoordenates(matrix);

    var peteca = coords
        .AsParallel()
        .Select(coord =>
        {
            var above = CountTreesAbove(matrix, coord);
            var right = CountTreesRight(matrix, coord);
            var below = CountTreesBelow(matrix, coord);
            var left = CountTreesLeft(matrix, coord);

            return (
                coord,
                above,
                left,
                right,
                below,
                scenicScore: above * left * right * below
            );
        })
        .OrderByDescending(x => x.scenicScore)
        .ToArray();

    return peteca.First().scenicScore;
}

bool IsVisibleLeft(char[][] matrix, (int row, int column) coord)
{
    return CountTreesLeft(matrix, coord) == coord.column + 1;
}

int CountTreesLeft(char[][] matrix, (int row, int column) coord)
{
    return CountVisibleTrees(
        matrix,
        coord,
        0,
        coord.column,
        (matrix, i) => matrix[coord.row][coord.column - 1 - i]
    );
}

bool IsVisibleRight(char[][] matrix, (int row, int column) coord)
{
    return CountTreesRight(matrix, coord) + coord.column + 1 == matrix[coord.row].Length;   
}

int CountTreesRight(char[][] matrix, (int row, int column) coord)
{
    return CountVisibleTrees(
        matrix,
        coord,
        coord.column + 1,
        matrix[coord.row].Length,
        (matrix, i) => matrix[coord.row][i]
    );
}

bool IsVisibleAbove(char[][] matrix, (int row, int column) coord)
{
    return CountTreesAbove(matrix, coord) - coord.row == 0;
}

int CountTreesAbove(char[][] matrix, (int row, int column) coord)
{
    return CountVisibleTrees(
        matrix,
        coord,
        0,
        coord.row,
        (matrix, i) => matrix[coord.row - 1 - i][coord.column]
    );
}

bool IsVisibleBelow(char[][] matrix, (int row, int column) coord)
{
    return CountTreesBelow(matrix, coord) + coord.row + 1 == matrix.Length;
}

int CountTreesBelow(char[][] matrix, (int row, int column) coord)
{
    return CountVisibleTrees(
        matrix,
        coord,
        coord.row + 1,
        matrix.Length,
        (matrix, i) => matrix[i][coord.column]
    );
}

int CountVisibleTrees(char[][] matrix, (int row, int column) coord, int initial, int max, Func<char[][], int, char> selector)
{
    var iterations = 0;
    for (int i = initial; i < max; i++)
    {
        iterations++;

        if (matrix[coord.row][coord.column] <= selector(matrix, i))
        {
            break;
        }
    }

    return iterations;
}

static char[][] ReadMatrix()
{
    return File
        .ReadAllLines("./input.txt")
        .Select(l => l.ToArray())
        .ToArray();
}

static IEnumerable<(int row, int column)> CreateCoordenates(char[][] matrix)
{
    return Enumerable
        .Range(0, matrix.Length)
        .SelectMany(row =>
            Enumerable
                .Range(0, matrix[row].Length)
                .Select(column => (row, column))
        );
}

//Advent of Code 2022
//------Day 8------
//Day 8 - 1: 1029
//Day 8 - 2: 291840
// broke puzzle 1 implementation when refactoring for puzzle 2. RIP XD