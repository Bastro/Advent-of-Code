string[] lines = File.ReadAllLines(@"input.txt");
var elvesInventories = new List<List<int>>()
{
    new List<int>()
};
int currentElf = 0;
foreach (string? line in lines)
{
    if (line == "")
    {
        currentElf += 1;
        elvesInventories.Add(new List<int>());
    }
    else
    {
        var elfInventory = elvesInventories[currentElf];
        int calories = Int32.Parse(line);
        elfInventory.Add(calories);
    }
}
IEnumerable<int>? eachElvesCalories = elvesInventories.Select(elfInventory => elfInventory.Sum());
int threeHighestCalorieCount = eachElvesCalories
    .OrderByDescending(elvCalories => elvCalories)
    .Take(3)
    .Sum();
    
Console.WriteLine(threeHighestCalorieCount);
Console.ReadLine();