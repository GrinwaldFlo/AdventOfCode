namespace AdventOfCode2024.Days;

internal class Day01a : DayBase
{
	internal Day01a()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		string[] data = GetData();

		List<(long, long)> dataI = data.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(y => long.Parse(y)).ToList()).Select(z => (z[0], z[1])).ToList();
		List<long> firstList = [.. dataI.Select(x => x.Item1).OrderBy(x => x)];
		List<long> secondList = [.. dataI.Select(x => x.Item2).OrderBy(x => x)];

		long sum = firstList.Zip(secondList).Aggregate((long)0, (sum, next) => sum + Math.Abs(next.First - next.Second));

		Console.WriteLine($"Sum is {sum}");
	}
}