namespace AdventOfCode2023.Days;
internal class Day03 : DayBase
{
	internal Day03()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		char[][] data = GetData().Select(x => x.ToCharArray()).ToArray();
		char[][] mask = new char[data.Length][];

		for (int i = 0; i < data.Length; i++)
		{
			mask[i] = new char[data[i].Length];
		}

		for (int i = 0; i < data.Length; i++)
		{
			for (int j = 0; j < data[i].Length; j++)
			{
				if (IsSymbol(data[i][j]))
					SetMask(mask, i, j);
			}
		}

		int cur = 0;
		bool isOk = false;
		int sum = 0;
		for (int i = 0; i < data.Length; i++)
		{
			for (int j = 0; j < data[i].Length; j++)
			{
				if (char.IsDigit(data[i][j]))
				{
					cur = (cur * 10) + data[i][j] - '0';
					if (mask[i][j] == '1')
						isOk = true;
				}
				else
				{
					if (cur > 0 && isOk)
						sum += cur;
					cur = 0;
					isOk = false;
				}
			}
			if (cur > 0 && isOk)
				sum += cur;
			cur = 0;
			isOk = false;
		}
		Console.WriteLine($"Day3 sum:{sum}");
	}

	private static bool IsSymbol(char value)
	{
		return !char.IsNumber(value) && value != '.';
	}

	private static void SetMask(char[][] mask, int x, int y)
	{
		for (int i = x - 1; i < x + 2; i++)
		{
			for (int j = y - 1; j < y + 2; j++)
			{
				SetValue(mask, i, j, '1');
			}
		}
	}

	private static void SetValue(char[][] mask, int x, int y, char value)
	{
		if (x >= 0 && y >= 0 && x < mask.Length && y < mask[0].Length)
			mask[x][y] = value;
	}
}
