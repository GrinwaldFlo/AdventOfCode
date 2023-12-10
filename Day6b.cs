namespace AdventOfCode2023;
internal class Day6b : DayBase
{
	internal class Race
	{
		internal long _winDist;
		internal long _time;

		internal long CountWins()
		{
			long cntWin = 0;
			for (long i = 1; i < _time; i++)
			{
				if (CalcDist(i) > _winDist)
					cntWin++;
			}
			return cntWin;
		}

		private long CalcDist(long pushTime)
		{
			long dist = (_time - pushTime) * pushTime;
			return dist;
		}
	}
	internal Day6b()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		List<string> data = [.. GetData()];

		string[] times = data[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
		string[] dists = data[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

		List<Race> races = [];
		long result = 1;
		for (long i = 1; i < times.Length; i++)
		{
			races.Add(new Race { _winDist = long.Parse(dists[i]), _time = long.Parse(times[i]) });

			long nbWins = races.Last().CountWins();
			Console.WriteLine($"Nb wins for time: {races.Last()._time} = {nbWins}");
			result *= nbWins;
		}

		Console.WriteLine($"Day 6b: {result}");
	}
}