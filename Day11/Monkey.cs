// See https://aka.ms/new-console-template for more information
public class Monkey
{
    public Queue<long> StartingItems { get; set; }
    public long InspectionCount { get; set; } = 0;
    public List<string> Operations { get; set; } = new List<string>();
    public int DivideBy { get; set; }
    public int TrueThrowTo { get; set; }
    public int FalseThrowTo { get; set; }

    public bool HasItems()
    {
        return StartingItems.Count() > 0;
    }

    public (long, long) Inspect(int? moduloFactor)
    {

        var item = StartingItems.Dequeue();
       
        //var newOperations = Utils.ReplaceValuesInList(Operations, item);
        long calc = Calc(Nums(Operations[0], item), Operations[1], Nums(Operations[2], item));
        long devidedByThree = 0;
        if (moduloFactor == null)
        {
            devidedByThree = calc / 3;
        } else
        {
            devidedByThree = calc % (long)moduloFactor;
        }
        InspectionCount++;
        int throwTo = Utils.IsDivisible(devidedByThree, DivideBy) ? TrueThrowTo : FalseThrowTo;
        return (throwTo, devidedByThree);
    }

    private static long Calc(long first, string calc, long second) => calc switch
    {
        "+" => first + second,
        "*" => first * second,
        "-" => first - second,
        "/" => first / second,
        _ => throw new ArgumentOutOfRangeException()
    };

    private static long Nums(string num, long startingItem)
    {
        if (num == "old")
        {
            return startingItem;
        }
        return Int32.Parse(num);
    }
}
