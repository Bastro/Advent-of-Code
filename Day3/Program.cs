string[] lines = File.ReadAllLines(@"input.txt");

int partOne = lines
    .Select(line => Rucksack.GetPrioritySingle(line))
    .Sum();

int partTwo = lines
    .ChunkForEveryNumber(3)
    .Select(group => Rucksack.GetPriorityGroup(group))
    .Sum();

Console.WriteLine(partOne);
Console.WriteLine(partTwo);

public sealed class Rucksack
{
    public static int GetPriorityGroup(IEnumerable<string> group)
    {
        char priorityItem = GetPriorityItemGroup(group.ToArray());
        int priority = ValueForPriorityItem(priorityItem);
        return priority;
    }

    public static int GetPrioritySingle(string line)
    {
        char priorityItem = GetPriorityItemSingle(line);
        int priority = ValueForPriorityItem(priorityItem);
        return priority;
    }

    private static int ValueForPriorityItem(char priorityItem)
    {
        char[] lowerAlphabet = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();
        char[] upperAlphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
        char[] bothCharchersAlphabet = lowerAlphabet.Concat(upperAlphabet).ToArray();
        return Array.IndexOf(bothCharchersAlphabet, priorityItem) + 1;
    }

    private static char GetPriorityItemGroup(string[] group)
    {
        var firstTwo = group[0].Intersect(group[1]);
        var charedBetweenGroups = firstTwo.Intersect(group[2]).FirstOrDefault();
        return charedBetweenGroups;
    }
    private static char GetPriorityItemSingle(string line)
    {
        string firstHalf = line.Substring(0, line.Length / 2);
        string secondHalf = line.Substring(line.Length / 2, (line.Length / 2));
        return firstHalf.Intersect(secondHalf).FirstOrDefault();
    }
}

static class Extensions
{
    public static IEnumerable<IEnumerable<T>> ChunkForEveryNumber<T>(this IEnumerable<T> source, int lengthForEveryArray) =>
        source.Select((item, index) => new { item, index })
            .GroupBy((group) => group.index / lengthForEveryArray)
            .Select((group) => group.Select(g => g.item).ToArray());
}