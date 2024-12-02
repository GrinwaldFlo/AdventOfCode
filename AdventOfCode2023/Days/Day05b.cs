namespace AdventOfCode2023.Days;
internal class Day05b : DayBase
{
	private readonly List<Map> _maps = [];

	internal Day05b()
	{
		_name = GetType().Name[..^1];
	}

	internal long Convert(long v)
	{
		var map = _maps.FirstOrDefault(x => x._from == "seed");
		long seedNew = v;
		while (map != null)
		{
			seedNew = map.Convert(seedNew);
			//	Console.WriteLine($"{map._from} -> {map._to} : {seed} -> {seedNew}");
			map = _maps.FirstOrDefault(x => x._from == map._to);
		}
		return seedNew;
	}

	internal override void Run()
	{
		List<string> data = [.. GetData()];

		long[] tmpSeeds = data[0]["seeds: ".Length..].Split(' ').Select(long.Parse).ToArray();
		List<long> allseeds = [];
		data.RemoveAt(0);

		List<string> buffer = [];
		while (data.Count != 0)
		{
			if (string.IsNullOrEmpty(data[0]))
			{
				if (buffer.Count != 0)
					_maps.Add(new([.. buffer]));
				buffer.Clear();
				data.RemoveAt(0);
			}
			else
			{
				buffer.Add(data[0]);
				data.RemoveAt(0);
			}
		}

		long minSeed = 0;
		long minLocation = long.MaxValue;

		for (int i = 0; i < tmpSeeds.Length; i++)
		{
			for (int j = 0; j < tmpSeeds[i + 1]; j++)
			{
				long seed = tmpSeeds[i] + j;

				long seedNew = Convert(seed);
				//Console.WriteLine($" ");
				if (seedNew < minLocation)
				{
					minSeed = seed;
					minLocation = seedNew;
				}
			}
			Console.WriteLine($"Seed lot {i}");
			i++;
		}

		Console.WriteLine($"Day 5a: seed:{minSeed} at location {minLocation}");
	}

	internal class Map
	{
		internal string _from;

		internal List<MapRange> _ranges = [];

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
			var found = _ranges.Where(x => x._from <= v).LastOrDefault();
			return found == null ? v : found.Convert(v);
		}

		internal class MapRange
		{
			internal long _from;
			internal long _range;
			internal long _to;
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
	}
}
//Day 5a: seed:385349830 at location 84931146 