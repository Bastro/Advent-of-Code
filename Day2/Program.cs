
string[] lines = File.ReadAllLines(@"input.txt");

int partOne = lines
    .Select(line => RockPaperScissors.Play(line.Split(' ')))
    .Sum();

int partTwo = lines
    .Select(line =>
    {
        var round = Cheating.CreateRound(line.Split(' '));
        return RockPaperScissors.Play(round);
    })
    .Sum(); 

Console.WriteLine(partOne);
Console.WriteLine(partTwo);
Console.ReadLine();

public struct Round 
{
    public Hand Opponent { get; set; }
    public Hand My { get; set; }
}

public enum Hand
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public sealed class Cheating
{
    public static Round CreateRound(string[] participants)
    {
        var opponentHand = RockPaperScissors.CreateHand(participants[0]);
        var myHand = CreateMyHand(participants[1], opponentHand);
        return new Round()
        {
            Opponent = opponentHand,
            My = myHand
        };
    }

    private static Hand GetWinningHand(Hand opponent) => opponent switch
    {
        Hand.Rock => Hand.Paper,
        Hand.Paper => Hand.Scissors,
        Hand.Scissors => Hand.Rock,
        _ => throw new ArgumentOutOfRangeException()
    };

    private static Hand GetLosingHand(Hand opponent) => opponent switch
    {
        Hand.Rock => Hand.Scissors,
        Hand.Paper => Hand.Rock,
        Hand.Scissors => Hand.Paper,
        _ => throw new ArgumentOutOfRangeException()
    };

    public static Hand CreateMyHand(string strategy, Hand opponentHand)
    {
        bool souldWin = strategy == "X";
        bool souldLose = strategy == "Z";

        if (souldWin)
            return GetLosingHand(opponentHand);
        else if (souldLose)
            return GetWinningHand(opponentHand);
        else
            return opponentHand;
    }
}

public sealed class RockPaperScissors
{
    private static readonly int WINNER_POINTS = 6;
    private static readonly int DRAW_POINTS = 3;
    private static readonly int LOSING_POINTS = 0;

    public static int Play(string[] participants)
    {
        var round = CreateRound(participants);
        return Play(round);
    }

    public static int Play(Round round) => MyRoundPoints(round);

    public static Hand CreateHand(string handText)
    {
        if (handText == "A" || handText == "X")
            return Hand.Rock;
        else if (handText == "B" || handText == "Y")
            return Hand.Paper;
        else
            return Hand.Scissors;
    }

    private static int MyRoundPoints(Round round)
    {
        var myHand = round.My;
        var opponentHand = round.Opponent;
        int pointsMyHand = Convert.ToInt32(myHand);
      
        var equalRound = myHand == opponentHand;
        if (equalRound) return DRAW_POINTS + pointsMyHand;

        int roundRound = DiffrentHandsWonRound(myHand, opponentHand) ? WINNER_POINTS : LOSING_POINTS;
        return roundRound + pointsMyHand;
    }

    private static bool DiffrentHandsWonRound(Hand my, Hand opponent) => my switch
    {
        Hand.Rock => opponent == Hand.Scissors,
        Hand.Paper => opponent == Hand.Rock,
        Hand.Scissors => opponent == Hand.Paper,
        _ => false
    };

    private static Round CreateRound(string[] participants)
    {
        return new Round
        {
            Opponent = CreateHand(participants[0]),
            My = CreateHand(participants[1]),
        };
    }
}
