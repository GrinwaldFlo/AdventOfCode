
namespace AdventOfCode2023;
internal class Day09b : DayBase
{
	internal class Data
	{
		private readonly long[] _data;

		internal long _prediction = 0;
		internal List<Derivative> _derivatives = [];
		internal Data(string line)
		{
			_data = line.Split(' ').Select(long.Parse).ToArray();

			_derivatives.Add(new Derivative(_data));

			while (!_derivatives.Last()._isLast)
			{
				_derivatives.Add(new Derivative(_derivatives.Last()._d));
			}

			for (int i = _derivatives.Count - 2; i >= 0; i--)
			{
				_derivatives[i]._previous = _derivatives[i]._d.First() - _derivatives[i + 1]._previous;
			}
			_prediction = _data.First() - _derivatives[0]._previous;
		}
	}

	internal class Derivative
	{
		internal bool _isLast;
		internal long[] _d;
		internal long _previous;
		internal Derivative(long[] data)
		{
			_d = new long[data.Length - 1];
			for (int i = 1; i < data.Length; i++)
			{
				_d[i - 1] = data[i] - data[i - 1];
			}
			_isLast = !_d.Any(x => x != 0);
		}
	}

	internal Day09b()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		var data = GetData().Select(x => new Data(x)).ToList();

		long sum = data.Sum(x => x._prediction);
		Console.WriteLine($"Day 9a: {sum}");
	}
}