// See https://aka.ms/new-console-template for more information
public static class Extensions
{
    public static IEnumerable<IEnumerable<string>> ChunkByWhiteSpace(this IEnumerable<string> source)
    {

        while (source.Count() > 0)
        {
            var take = source.TakeWhile(line => !String.IsNullOrWhiteSpace(line)).ToList();
            yield return take;
            source = source.Skip(take.Count() + 1);
        }
    }
}