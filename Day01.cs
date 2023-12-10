namespace AdventOfCode2023;
internal class Day01 : DayBase
{
	private readonly string[] _n = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
	internal Day01()
	{
		_name = GetType().Name;
	}
	internal override void Run()
	{
		string[] data = GetData();
		int sum = data.Sum(GetSum);
		Console.WriteLine($"Sum is {sum}");
	}

	private int GetFirst(string value)
	{
		string v = value;
		while (v.Length > 0)
		{
			if (char.IsDigit(v[0]))
				return v[0] - '0';
			for (int j = 0; j < _n.Length; j++)
			{
				if (v.StartsWith(_n[j]))
					return j + 1;
			}
			v = v[1..];
		}
		return 0;
	}

	private int GetLast(string value)
	{
		string v = value;
		while (v.Length > 0)
		{
			if (char.IsDigit(v[^1]))
				return v[^1] - '0';
			for (int j = 0; j < _n.Length; j++)
			{
				if (v.EndsWith(_n[j]))
					return j + 1;
			}
			v = v[..^1];
		}
		return 0;
	}

	private int GetSum(string value)
	{
		return (GetFirst(value) * 10) + GetLast(value);
	}
}
