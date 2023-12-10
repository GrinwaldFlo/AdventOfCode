
namespace AdventOfCode2023;
internal class Day9a : DayBase
{

	internal Day9a()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		List<string> data = [.. GetData()];
		string directions = data[0];

		int step = 0;
		Console.WriteLine($"Day 9a: {step}");
	}
}