namespace AdventOfCode2023.Day2;

file readonly ref struct Draw
{
    public int Blue { get; init; }
    public int Red { get; init; }
    public int Green { get; init; }
    public int Sum => Blue + Red + Green;

    private const int RedMax = 12;
    private const int GreenMax = 13;
    private const int BlueMax = 14;
    private const int MaxSum = BlueMax + RedMax + GreenMax;

    private Draw(int blue, int red, int green)
    {
        Blue = blue;
        Red = red;
        Green = green;
    }

    internal static Draw Create(ReadOnlySpan<char> input) // input: `3 blue, 4 red`
    {
        var comasCount = input.Count(',');
        Span<Range> balls = stackalloc Range[comasCount + 1];
        input.Split(balls, ',');

        var blue = 0;
        var red = 0;
        var green = 0;

        foreach (var ballRange in balls)
        {
            var (offset, length) = ballRange.GetOffsetAndLength(input.Length);
            var x = input.Slice(offset, length).Trim();

            var spaceIndex = x.IndexOf(' ');
            var number = int.Parse(x[0..spaceIndex]);
            var color = x[(spaceIndex + 1)..x.Length];

            if ("red".AsSpan().Equals(color, StringComparison.InvariantCultureIgnoreCase))
            {
                red = number;
            }
            else if ("blue".AsSpan().Equals(color, StringComparison.InvariantCultureIgnoreCase))
            {
                blue = number;
            }
            else if ("green".AsSpan().Equals(color, StringComparison.InvariantCultureIgnoreCase))
            {
                green = number;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(color));
            }
        }

        return new Draw(blue, red, green);
    }

    internal static bool IsValid(ReadOnlySpan<char> input) // input: `3 blue, 4 red`
    {
        var draw = Create(input);

        if (draw.Blue > BlueMax || draw.Red > RedMax || draw.Green > GreenMax || draw.Sum > MaxSum)
        {
            return false;
        }

        return true;
    }
}

internal readonly ref struct Game
{
    public static int Validate(ReadOnlySpan<char> input) // input: `Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green`
    {
        var colonIndex = input.IndexOf(':');

        var drawsPart = input.Slice(colonIndex + 1).Trim(); // drawsPart: `3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green`
        var semicolonsCount = drawsPart.Count(';');
        Span<Range> drawsSplitted = stackalloc Range[semicolonsCount + 1];
        drawsPart.Split(drawsSplitted, ';');

        foreach (var drawRange in drawsSplitted)
        {
            var (offset, length) = drawRange.GetOffsetAndLength(drawsPart.Length);
            var drawString = drawsPart.Slice(offset, length).Trim();

            if (!Draw.IsValid(drawString))
            {
                return 0;
            }
        }

        var gameNum = int.Parse(input[5..colonIndex]);
        return gameNum;
    }

    public static int GetPower(ReadOnlySpan<char> input) // input: `Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green`
    {
        var colonIndex = input.IndexOf(':');

        var drawsPart = input.Slice(colonIndex + 1).Trim(); // drawsPart: `3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green`
        var semicolonsCount = drawsPart.Count(';');
        Span<Range> drawsSplitted = stackalloc Range[semicolonsCount + 1];
        drawsPart.Split(drawsSplitted, ';');

        var maxRed = 0;
        var maxBlue = 0;
        var maxGreen = 0;

        foreach (var drawRange in drawsSplitted)
        {
            var (offset, length) = drawRange.GetOffsetAndLength(drawsPart.Length);
            var drawString = drawsPart.Slice(offset, length).Trim();

            var draw = Draw.Create(drawString);
            maxRed = Math.Max(maxRed, draw.Red);
            maxBlue = Math.Max(maxBlue, draw.Blue);
            maxGreen = Math.Max(maxGreen, draw.Green);
        }

        return maxGreen * maxBlue * maxRed;
    }
}