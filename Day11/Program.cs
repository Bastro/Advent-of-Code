// See https://aka.ms/new-console-template for more information
using Day11;
using System.Linq;
using System.Linq.Expressions;

string[] lines = File.ReadAllLines(@"input.txt");
List<Monkey> firstMonkeys = Parser.Monkeys(lines).ToList();
Rounds firstRounds = new Rounds(20, firstMonkeys);
firstRounds.Inspect();
Console.WriteLine($"Part 1: {firstRounds.HeightsInspectionMonkeys()}");

List<Monkey> monkeysSecond = Parser.Monkeys(lines).ToList();
var factor = monkeysSecond.Aggregate(1, (c, m) => c * m.DivideBy);
Rounds secondRounds = new Rounds(10000, monkeysSecond);
secondRounds.Inspect(factor);
Console.WriteLine($"Part 2: {secondRounds.HeightsInspectionMonkeys()}");


