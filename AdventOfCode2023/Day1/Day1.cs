using System.Buffers;
using System.Runtime.InteropServices;

namespace AdventOfCode2023.Day1;

internal static class Day1
{
    private static readonly string Example1Path = Path.Combine("Day1", "example1.txt");
    private static readonly string Example2Path = Path.Combine("Day1", "example2.txt");
    private static readonly string InputPath = Path.Combine("Day1", "input.txt");

    internal static void Step1()
    {
        var sum = 0;
        using var reader = File.OpenText(InputPath);

        ReadOnlySpan<char> digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
        var search = SearchValues.Create(digits);

        for (ReadOnlySpan<char> line = reader.ReadLine(); !line.IsEmpty; line = reader.ReadLine())
        {
            var firstIndex = line.IndexOfAny(search);
            var lastIndex = line.LastIndexOfAny(search);

            var first = int.Parse(line.Slice(firstIndex, 1));
            var last = int.Parse(line.Slice(lastIndex, 1));
            //Console.WriteLine($"{first} | {last}");

            sum += 10 * first + last;
        }

        Console.WriteLine($"Day 1 | Step 1 | Answer: {sum}");
    }

    internal static void Step2()
    {
        var sum = 0;

        var strings = new List<string>
        {
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        }.AsReadOnly();

        using var reader = File.OpenText(InputPath);

        for (ReadOnlySpan<char> line = reader.ReadLine(); !line.IsEmpty; line = reader.ReadLine())
        {
            // index -> value
            var indexes = new SortedDictionary<int, int>();

            for (var i = 0; i < strings.Count; i++)
            {
                var idx1 = line.IndexOf(strings[i], StringComparison.InvariantCultureIgnoreCase);
                var idx2 = line.LastIndexOf(strings[i], StringComparison.InvariantCultureIgnoreCase);
                
                if (idx1 >= 0)
                {
                    indexes.Add(idx1, i + 1);
                }

                if (idx2 > idx1)
                {
                    indexes.Add(idx2, i + 1);
                }
            }

            for (var i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line.Slice(i, 1), out var parsed) && parsed != 0)
                {
                    indexes.Add(i, parsed);
                }
            }

            var firstNum = indexes.First().Value;
            var lastNum = indexes.Last().Value;

            var lineSum = 10 * firstNum + lastNum;
            //Console.WriteLine($"Line sum: {lineSum}");

            sum += lineSum;
        }

        Console.WriteLine($"Day 1 | Step 2 | Answer: {sum}");
    }
}