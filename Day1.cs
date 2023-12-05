namespace AdventOfCode2023;
internal class Day1 : DayBase
{
	private readonly string[] n = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
	internal Day1()
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
			for (int j = 0; j < n.Length; j++)
			{
				if (v.StartsWith(n[j]))
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
			for (int j = 0; j < n.Length; j++)
			{
				if (v.EndsWith(n[j]))
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
