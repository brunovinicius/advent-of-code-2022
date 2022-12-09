Console.WriteLine("Advent of Code 2022");
Console.WriteLine("------ Day 9 ------");

var result1 = FindPositionsTailHasVisited();
Console.WriteLine($"Day 9-1: {result1}");

var result2 = FindPositionsTailHasVisited2();
Console.WriteLine($"Day 9-2: {result2}");

int FindPositionsTailHasVisited()
{
    var commands = File
        .ReadAllLines("./input.txt")
        .Select(l => l.Split(' '))
        .Select(p => (direction: p[0].Single(), distance: int.Parse(p[1])))
        .ToArray();

    var visitedPositions = new HashSet<Position>();
    var head = new Position();
    var tail = new Position();
    foreach (var command in commands)
    {
        Action moveHead = command.direction switch
        {
            'U' => () => head.MoveUp(),
            'L' => () => head.MoveLeft(),
            'R' => () => head.MoveRight(),
            'D' => () => head.MoveDown(),
            _ => throw new InvalidOperationException()
        };

        for (int i = 0; i < command.distance; i++)
        {
            moveHead();
            tail.Follow(head);

            visitedPositions.Add(tail);
        }
    }

    return visitedPositions.Count;
}


int FindPositionsTailHasVisited2()
{
    var commands = File
        .ReadAllLines("./input.txt")
        .Select(l => l.Split(' '))
        .Select(p => (direction: p[0].Single(), distance: int.Parse(p[1])))
        .ToArray();

    var visitedPositions = new HashSet<Position>();
    var head = new Position();
    var body1 = new Position();
    var body2 = new Position();
    var body3 = new Position();
    var body4 = new Position();
    var body5 = new Position();
    var body6 = new Position();
    var body7 = new Position();
    var body8 = new Position();
    var tail = new Position();
    foreach (var command in commands)
    {
        Action moveHead = command.direction switch
        {
            'U' => () => head.MoveUp(),
            'L' => () => head.MoveLeft(),
            'R' => () => head.MoveRight(),
            'D' => () => head.MoveDown(),
            _ => throw new InvalidOperationException()
        };

        for (int i = 0; i < command.distance; i++)
        {
            moveHead();
            body1.Follow(head);
            body2.Follow(body1);
            body3.Follow(body2);
            body4.Follow(body3);
            body5.Follow(body4);
            body6.Follow(body5);
            body7.Follow(body6);
            body8.Follow(body7);
            tail.Follow(body8);

            visitedPositions.Add(tail);
        }
    }

    return visitedPositions.Count;
}

struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public void MoveUp() => Y--;
    public void MoveLeft() => X--;
    public void MoveRight() => X++;
    public void MoveDown() => Y++;

    public void Follow(Position other)
    {
        var distance = other.Distance(this);

        if (this.IsAdjacent(other)) return;

        if (distance.Y == 0)
        {
            this.X += distance.X / 2;
        }
        else if(distance.X == 0)
        {
            this.Y += distance.Y / 2;
        }
        else
        {
            this.X += Math.Abs(distance.X) == 2 ? distance.X / 2 : distance.X;
            this.Y += Math.Abs(distance.Y) == 2 ? distance.Y / 2 : distance.Y;
        }

    }

    public bool IsAdjacent(Position other)
    {
        return this.IsAdjacent(this.Distance(other));
    }

    public bool IsAdjacent(Distance distanceToOther)
    {
        return Math.Abs(distanceToOther.X) <= 1
            && Math.Abs(distanceToOther.Y) <= 1;
    }

    public Distance Distance(Position other)
    {
        return new Distance(
            this.X - other.X,
            this.Y - other.Y
        );
    }

    public override bool Equals(object? obj)
    {
        return obj is Position position &&
               X == position.X &&
               Y == position.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

record Distance(int X, int Y);