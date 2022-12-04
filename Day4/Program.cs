string[] input = File.ReadAllLines(@"input.txt");

var partOne = input
    .Select(line => Assignments.ListOverlap(line.Split(',')) ? 1 : 0)
    .Sum();

var partTwo = input
    .Select(line => Assignments.AnyOverlap(line.Split(',')) ? 1 : 0)
    .Sum();

Console.WriteLine(partOne);
Console.WriteLine(partTwo);

class Assignments
{
    public static bool ListOverlap(string[] pairElvesAssignments)
    {
        int[] elvOneAssigment = GetAssignments(pairElvesAssignments[0]);
        int[] elvTwoAssigment = GetAssignments(pairElvesAssignments[1]);

        var includedInAllOne = elvOneAssigment.Intersect(elvTwoAssigment).Sum() == elvOneAssigment.Sum();
        var includedInAllTwo = elvTwoAssigment.Intersect(elvOneAssigment).Sum() == elvTwoAssigment.Sum();
        return includedInAllOne || includedInAllTwo;
    }

    public static bool AnyOverlap(string[] pairElvesAssignments)
    {
        int[] elvOneAssigment = GetAssignments(pairElvesAssignments[0]);
        int[] elvTwoAssigment = GetAssignments(pairElvesAssignments[1]);

        bool includedInAnyOne = elvOneAssigment.Intersect(elvTwoAssigment).Any();
        bool includedInAnyTwo = elvTwoAssigment.Intersect(elvOneAssigment).Any();
        return includedInAnyOne || includedInAnyTwo;
    }

    private static int[] GetAssignments(string elv)
    {
        string[] elvOneTextNum = elv.Split('-');
        return RangeSequence(elvOneTextNum[0], elvOneTextNum[1]);
    }

    private static int[] RangeSequence(string start, string end)
    {
        int startInt = Convert.ToInt32(start);
        int endInt = Convert.ToInt32(end);
        return Enumerable.Range(startInt, endInt - startInt + 1).ToArray();
    }
}