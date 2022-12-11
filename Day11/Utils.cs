// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Text.RegularExpressions;

public static partial class Utils
{
    public static Queue<long> ExtractLongFromString(string input)
    {
        MatchCollection matches = Regex.Matches(input, @"\d+");
        IEnumerable<long> numbers = matches.Cast<Match>().Select(m => long.Parse(m.Value));
        return new Queue<long>(numbers);
    }

    public static List<int> ExtractIntFromString(string input)
    {
        MatchCollection matches = Regex.Matches(input, @"\d+");
        List<int> numbers = matches.Cast<Match>().Select(m => int.Parse(m.Value)).ToList();
        return numbers;
    }

    public static bool IsDivisible(long dividend, long divisor) => divisor != 0 ? dividend % divisor == 0 : false;
}
