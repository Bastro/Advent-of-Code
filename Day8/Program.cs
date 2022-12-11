using Microsoft.VisualBasic;

string[] input = File.ReadAllLines(@"input.txt");
IEnumerable<IEnumerable<int>> grid = input.Select(line => line.Select(textNum => int.Parse($"{textNum}")));
Quadcopter quadcopter = new(grid);
Console.WriteLine($"Part 1: {quadcopter.CountTrees()}");

public class Quadcopter
{
    private IEnumerable<IEnumerable<int>> _grid;
    private int _visibleTrees  = 0;

    public Quadcopter(IEnumerable<IEnumerable<int>> grid)
    {
        _grid = grid;
    }

    public int CountTrees()
    {
        CountInterior();
        CountEdges();
        return _visibleTrees;
    }

    private void CountInterior()
    {
        for (int y = 1; y < _grid.Count() - 1; y++)
        {
            var row = _grid.ElementAt(y);
            for (int x = 1; x < row.Count() - 1; x++)
            {
                if (x == 2 || y == 6)
                {
                    int ok = 0;
                }

                var treeHeight = row.ElementAt(x);
                bool visibleInX = IsVisibleX(treeHeight, x, row);
                bool visibleInY = IsVisibleY(treeHeight, x, y);

                if (visibleInX || visibleInY)
                {
                    Console.WriteLine($"{x}, {y}");
                    _visibleTrees++;
                }
            }
        }
    }

    private void CountEdges()
    {
        int allHorizontal = _grid.ElementAt(0).Count() * 2;
        int allVertical = _grid.Count() * 2;
        int countedTwiceCorners = 4;
        _visibleTrees = _visibleTrees + allHorizontal + allVertical - countedTwiceCorners;
    }

    private bool IsVisibleY(int treeHeight, int x, int y)
    {
        var xItemInEvery = _grid.Select(row => row.ElementAt(x));

        var top = xItemInEvery.Take(y);
        int highestTop = top.Max();
        var bottom = xItemInEvery.TakeLast(xItemInEvery.Count() - y - 1);
        int highestBottom = bottom.Max();
        return treeHeight > highestTop || treeHeight > highestBottom;
    }

    private bool IsVisibleX(int treeHeight, int x, IEnumerable<int> row)
    {
        int highestLeft = row.Max();
        int highestRight = row.TakeLast(row.Count() - x - 1).Max();
        return treeHeight > highestLeft || treeHeight > highestRight;
    }
}
