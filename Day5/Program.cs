using System.Linq;
using System.Text;

string[] input = File.ReadAllLines(@"input.txt");

var (movesLines, stacks, cargosLines) = Parser.UsableItems(input);
var moves = Instructions.GetMoves(movesLines);

var supplies1 = new Supplies(stacks);
supplies1.AddCargo(cargosLines);
var cargoCrane1 = new CargoCrane(supplies1);
cargoCrane1.WorkSingleCargo(moves);
Console.WriteLine($"Part 1: ${supplies1.TopCargo()}");

var supplies2 = new Supplies(stacks);
supplies2.AddCargo(cargosLines);
var cargoCrane2 = new CargoCrane(supplies2);
cargoCrane2.WorkManyCargo(moves);
Console.WriteLine($"Part 2: ${supplies2.TopCargo()}");

public class Parser
{
    public static (IEnumerable<string> moves, int stacks, IEnumerable<string> cargos) UsableItems(string[] lines)
    {
        var moves = lines
        .SkipWhile(line => !line.Contains("move"));

        int stacks = lines
            .Select(line => line.Replace(" ", ""))
            .Where(line => line.All(char.IsDigit))
            .First()
            .Count();

        var cargos = lines
            .TakeWhile(line =>
            {
                var ok = line.Replace(" ", "").Replace("[", "").Replace("]", "");
                return !ok.All(char.IsDigit);
            });

        return (moves, stacks, cargos);
    }
}

class Instructions
{
    public static IEnumerable<Move> GetMoves(IEnumerable<string> moves) => moves.Select(CreateMove);
    private static Move CreateMove(string line)
    {
        var onlyNums = line.Where(Char.IsDigit).ToArray();
        var onlyIntNums = Array.ConvertAll(onlyNums, c => (int)Char.GetNumericValue(c));
        if (onlyIntNums.Length == 4)
        {
            int doubleDigits = Int32.Parse($"{onlyIntNums[0]}{onlyIntNums[1]}");
            return new Move(doubleDigits, onlyIntNums[2], onlyIntNums[3]);
        }
        return new Move(onlyIntNums[0], onlyIntNums[1], onlyIntNums[2]);
    }
}

class CargoCrane
{
    private Supplies _supplies;

    public CargoCrane(Supplies supplies)
    {
        _supplies = supplies;
    }

    public void WorkSingleCargo(IEnumerable<Move> moves)
    {
        foreach (var move in moves)
        {
            MakeAMoveSingle(move);
        }
    }

    public void WorkManyCargo(IEnumerable<Move> moves)
    {
        foreach (var move in moves)
        {
            MakeAMoveMany(move);
        }
    }

    private void MakeAMoveMany(Move move)
    {
        var takeFromStack = _supplies.Stacks.ElementAt(move.From - 1);
        var addToStack = _supplies.Stacks.ElementAt(move.To - 1);
        var cargeToMove = takeFromStack.TakeLast(move.Stack);
        addToStack.AddRange(cargeToMove);
        takeFromStack.RemoveRange(takeFromStack.Count - move.Stack, move.Stack);
    }

    private void MakeAMoveSingle(Move move)
    {
        var takeFromStack = _supplies.Stacks.ElementAt(move.From - 1);
        var addToStack = _supplies.Stacks.ElementAt(move.To - 1);
        for (int i = 0; i < move.Stack; i++)
        {
            if (takeFromStack.Count() > 0)
            {
                var lastElement = takeFromStack.ElementAt(takeFromStack.Count() - 1);
                addToStack.Add(lastElement);
                takeFromStack.RemoveAt(takeFromStack.Count() - 1);
            }
        }
    }
}

class Supplies
{
    public List<List<char>> Stacks { get; set; } = new List<List<char>>();

    public Supplies(int stacks)
    {
        for (int i = 0; i < stacks; i++)
        {
            Stacks.Add(new List<char>());
        }
    }

    public void AddCargo(IEnumerable<string> lines)
    {
        var reverseLines = lines.Reverse();
        foreach (var line in reverseLines)
        {
            AddCargo(line);
        }
    }

    public string TopCargo()
    {
        var sb = new StringBuilder();
        foreach(var stack in Stacks)
        {
            if (stack.Count() > 0)
            {
                var last = stack.ElementAt(stack.Count() - 1);
                sb.Append(last);
            }
        }
        return sb.ToString();
    }

    private void AddCargo(string line)
    {
        var stacks = line
            .Chunk(4)
            .ToList();

        for (int i = 0; i < stacks.Count(); i++)
        {
            var cargo = stacks[i].Where(Char.IsLetter);
            var hasCargo = cargo.Count() > 0;
            if (hasCargo)
            {
                var stack = Stacks.ElementAt(i);
                char cargoChar = cargo.First();
                stack.Add(cargoChar);
            }
        }
    }
}

public struct Move
{
    public Move(int stack, int from, int to) : this()
    {
        Stack = stack;
        From = from;
        To = to;
    }

    public int Stack { get; private set; } = -1;
    public int From { get; private set; } = -1;
    public int To { get; private set; } = -1;
}