
namespace AdventOfCode2023;
internal class Day8a : DayBase
{
	internal class Location(string line)
	{
		internal string _from = line[0..3];
		internal string _l = line[7..10];
		internal string _r = line[12..15];

		internal string Next(char dir)
		{
			return dir == 'R' ? _r : _l;
		}
	}
	internal Day8a()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		List<string> data = [.. GetData()];
		string directions = data[0];
		data.RemoveAt(0);
		data.RemoveAt(0);
		var locations = data.Select(x => new Location(x)).ToList();

		long step = 0;

		string curPos = "AAA";
		int dirPos = 0;

		while (curPos != "ZZZ")
		{
			var curLoc = locations.First(x => x._from == curPos);
			curPos = curLoc.Next(directions[dirPos++ % directions.Length]);
			step++;
		}

		Console.WriteLine($"Day 8a: {step}");
	}
}