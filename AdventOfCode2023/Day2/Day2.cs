namespace AdventOfCode2023.Day2;

internal static class Day2
{
    private static readonly string Example1Path = Path.Combine("Day2", "example1.txt");
    private static readonly string InputPath = Path.Combine("Day2", "input.txt");

    internal static void Step1()
    {
        using var reader = File.OpenText(InputPath);
        var sum = 0;

        for (ReadOnlySpan<char> line = reader.ReadLine(); !line.IsEmpty; line = reader.ReadLine())
        {
            sum += Game.Validate(line);
        }

        Console.WriteLine($"Day 2 | Step 1 | Answer: {sum}");
    }

    internal static void Step2()
    {
        using var reader = File.OpenText(InputPath);
        var sum = 0;

        for(ReadOnlySpan<char> line = reader.ReadLine(); !line.IsEmpty; line = reader.ReadLine())
        {
            sum += Game.GetPower(line);
        }

        Console.WriteLine($"Day 2 | Step 2 | Answer: {sum}");
    }
}