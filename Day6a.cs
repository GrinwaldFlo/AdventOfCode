namespace AdventOfCode2023;
internal class Day6a : DayBase
{
	internal class Race
	{
		internal int _winDist;
		internal int _time;

		internal int CountWins()
		{
			int cntWin = 0;
			for (int i = 1; i < _time; i++)
			{
				if (CalcDist(i) > _winDist)
					cntWin++;
			}
			return cntWin;
		}

		private int CalcDist(int pushTime)
		{
			return (_time - pushTime) * pushTime;
		}
	}
	internal Day6a()
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
		for (int i = 1; i < times.Length; i++)
		{
			races.Add(new Race { _winDist = int.Parse(dists[i]), _time = int.Parse(times[i]) });

			int nbWins = races.Last().CountWins();
			Console.WriteLine($"Nb wins for time: {races.Last()._time} = {nbWins}");
			result *= nbWins;
		}

		Console.WriteLine($"Day 6a: {result}");
	}
}