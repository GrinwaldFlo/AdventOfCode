namespace AdventOfCode2024.Days;

internal class Day01b : DayBase
{
	internal Day01b()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		string[] data = GetData();
		List<(long, long)> dataI = data.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(y => long.Parse(y)).ToList()).Select(z => (z[0], z[1])).ToList();
		List<long> secondList = [.. dataI.Select(x => x.Item2).OrderBy(x => x)];

		long sum = dataI.Aggregate((long)0, (sum, next) => sum + (next.Item1 * secondList.Count(x => x == next.Item1)));
		Console.WriteLine($"Sum is {sum}");
	}
}