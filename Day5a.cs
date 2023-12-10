namespace AdventOfCode2023;
internal class Day5a : DayBase
{
	internal class Map
	{
		internal class MapRange
		{
			internal long _from;
			internal long _to;
			internal long _range;

			internal MapRange(string data)
			{
				string[] tmp = data.Split(' ');
				_from = long.Parse(tmp[1]);
				_to = long.Parse(tmp[0]);
				_range = long.Parse(tmp[2]);
			}

			internal long Convert(long v)
			{
				return v >= _from && v < _from + _range ? _to + (v - _from) : v;
			}
		}

		internal List<MapRange> _ranges = [];

		internal string _from;
		internal string _to;

		internal Map(string[] data)
		{
			string[] tmp = data[0].Split(' ', '-');
			_from = tmp[0];
			_to = tmp[2];

			_ranges = [.. data.Skip(1).Select(x => new MapRange(x)).OrderBy(x => x._from)];
		}

		internal long Convert(long v)
		{
			var found = _ranges.Where(x => x._from < v).LastOrDefault();
			return found == null ? v : found.Convert(v);
		}
	}

	internal Day5a()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		List<string> data = [.. GetData()];

		long[] seeds = data[0]["seeds: ".Length..].Split(' ').Select(long.Parse).ToArray();
		data.RemoveAt(0);

		List<Map> maps = [];
		List<string> buffer = [];
		while (data.Count != 0)
		{
			if (string.IsNullOrEmpty(data[0]))
			{
				if (buffer.Count != 0)
				{
					maps.Add(new([.. buffer]));
				}
				buffer.Clear();
				data.RemoveAt(0);
			}
			else
			{
				buffer.Add(data[0]);
				data.RemoveAt(0);
			}
		}

		var map = maps.FirstOrDefault(x => x._from == "seed");
		long minSeed = 0;
		long minLocation = long.MaxValue;
		foreach (long seed in seeds)
		{
			long seedNew = seed;
			while (map != null)
			{
				seedNew = map.Convert(seedNew);
				//Console.WriteLine($"{map._from} -> {map._to} : {seed} -> {seedNew}");
				map = maps.FirstOrDefault(x => x._from == map._to);
			}

			if (seedNew < minLocation)
			{
				minSeed = seed;
				minLocation = seedNew;
			}
			map = maps.FirstOrDefault(x => x._from == "seed");
		}

		Console.WriteLine($"Day 5a: seed:{minSeed} at location {minLocation}");
	}
}
//Day 5a: seed:385349830 at location 84931146