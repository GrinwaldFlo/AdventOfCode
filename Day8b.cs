
namespace AdventOfCode2023;
internal class Day8b : DayBase
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

	internal Day8b()
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

		var curPos = locations.Where(x => x._from.EndsWith('A')).Select(x => x._from).ToList();
		int dirPos = 0;

		while (curPos.Any(x => !x.EndsWith('Z')))
		{
			var curLoc = curPos.Select(x => locations.First(y => y._from == x));
			curPos = curLoc.Select(x => x.Next(directions[dirPos])).ToList();
			dirPos = (dirPos + 1) % directions.Length;
			step++;
		}

		Console.WriteLine($"Day 8b: {step}");
	}
}