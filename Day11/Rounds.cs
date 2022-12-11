public class Rounds
{
    private readonly int _times;
    private readonly List<Monkey> _monkeys;

    public Rounds(int times, List<Monkey> monkeys)
    {
        _times = times;
        _monkeys = monkeys;
    }

    public void Inspect(int? moduloFactor = null)
    {
        for (int i = 0; i < _times; i++)
        {
            foreach (var monkey in _monkeys)
            {
                while (monkey.HasItems())
                {
                    (long throwTo, long numberToThrow) = monkey.Inspect(moduloFactor);
                    _monkeys.ElementAt((int)throwTo).StartingItems.Enqueue(numberToThrow);
                }
            }
        }  
    }

    public long HeightsInspectionMonkeys()
    {
        IOrderedEnumerable<long> sortedInspectsCount = _monkeys.Select(m => m.InspectionCount).OrderDescending();
        return sortedInspectsCount.ElementAt(0) * sortedInspectsCount.ElementAt(1);
    }
}
