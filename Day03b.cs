namespace AdventOfCode2023;
internal class Day03b : DayBase
{
	private readonly List<int> _r = [];
	private readonly List<List<int>> _s = [];

	internal Day03b()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		char[][] data = GetData().Select(x => x.ToCharArray()).ToArray();
		int[][] mask = new int[data.Length][];
		_r.Add(0);
		_s.Add([]);

		for (int i = 0; i < data.Length; i++)
		{
			mask[i] = new int[data[i].Length];
		}

		for (int i = 0; i < data.Length; i++)
		{
			for (int j = 0; j < data[i].Length; j++)
			{
				if (IsSymbol(data[i][j]))
				{
					SetMask(mask, i, j);
				}
			}
		}

		int cur = 0;
		int maskId = 0;

		for (int i = 0; i < data.Length; i++)
		{
			for (int j = 0; j < data[i].Length; j++)
			{
				if (char.IsDigit(data[i][j]))
				{
					cur = (cur * 10) + data[i][j] - '0';
					if (mask[i][j] > 0)
					{
						maskId = mask[i][j];
					}
				}
				else
				{
					if (cur > 0 && maskId > 0)
					{
						_s[maskId].Add(cur);
					}
					cur = 0;
					maskId = 0;
				}
			}
			if (cur > 0 && maskId > 0)
			{
				_s[maskId].Add(cur);
			}
			cur = 0;
			maskId = 0;
		}

		long sum = _s.Where(x => x.Count == 2).Select(x => x[0] * x[1]).Sum();

		Console.WriteLine($"Day3 sum:{sum}");
	}

	private static bool IsSymbol(char value)
	{
		return value == '*';
	}

	private void SetMask(int[][] mask, int x, int y)
	{
		_r.Add(0);
		_s.Add([]);
		for (int i = x - 1; i < x + 2; i++)
		{
			for (int j = y - 1; j < y + 2; j++)
			{
				SetValue(mask, i, j, _r.Count - 1);
			}
		}
	}

	private static void SetValue(int[][] mask, int x, int y, int value)
	{
		if (x >= 0 && y >= 0 && x < mask.Length && y < mask[0].Length)
		{
			mask[x][y] = value;
		}
	}
}
