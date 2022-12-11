using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    public static class Parser
    {
        public static IEnumerable<Monkey> Monkeys(string[] lines)
        {
            IEnumerable<IEnumerable<string>> monkyes = lines.ChunkByWhiteSpace();
            return monkyes.Select(CreateMonkey);
        }

        public static Monkey CreateMonkey(IEnumerable<string> monkey)
        {
            return new Monkey()
            {
                StartingItems = Utils.ExtractLongFromString(monkey.ElementAt(1)),
                Operations = monkey.ElementAt(2).Split(" ").TakeLast(3).ToList(),
                DivideBy = Utils.ExtractIntFromString(monkey.ElementAt(3)).First(),
                TrueThrowTo = Utils.ExtractIntFromString(monkey.ElementAt(4)).First(),
                FalseThrowTo = Utils.ExtractIntFromString(monkey.ElementAt(5)).First(),
            };
        }
    }
}
