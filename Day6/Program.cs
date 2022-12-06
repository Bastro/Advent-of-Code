string input = File.ReadAllText(@"input.txt");

Device deviceStart = new(4);
deviceStart.IdentifyStart(input);
Console.WriteLine($"Part 1: {deviceStart.StartOfPacketMarker}");

Device deviceMessage = new(14);
deviceMessage.IdentifyStart(input);
Console.WriteLine($"Part 2: {deviceMessage.StartOfPacketMarker}");


public class Device
{
    private int _lengthOfMarker = 0;
    public Device(int lengthOfMarker)
    {
        _lengthOfMarker = lengthOfMarker;
    }

    public int StartOfPacketMarker { get; set; } = -1;
    public int Counter { get; set; } = 0;
    public void IdentifyStart(string signal)
    {
        var potentialMarker = signal
            .Take(_lengthOfMarker)
            .Distinct();

        if (potentialMarker.Count() == _lengthOfMarker)
        {
            StartOfPacketMarker = Counter + _lengthOfMarker;
            return;
        }
        Counter++;
        var newSignal = signal.Substring(1);
        IdentifyStart(newSignal);
    }
}
