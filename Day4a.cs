using System.Text.RegularExpressions;

namespace AdventOfCode2023;
internal partial class Day4a : DayBase
{
	internal partial class Card
	{
		internal int _id;
		internal List<int> _win = [];
		internal List<int> _num = [];
		internal int _sum = 0;
		internal Card(string v)
		{
			var r = CardRegex();
			var result = r.Match(v.Replace("  ", " ").Replace("  ", " "));
			if (result.Success)
			{
				_id = int.Parse(result.Groups["id"].Value);
				_win = result.Groups["win"].Captures.Select(x => int.Parse(x.Value)).ToList();
				_num = result.Groups["num"].Captures.Select(x => int.Parse(x.Value)).ToList();
			}
			CalcSum();
		}

		internal void CalcSum()
		{
			int nbMatch = _num.Where(x => _win.Contains(x)).Select(x => 1).Sum();
			_sum = nbMatch <= 1 ? nbMatch : (int)Math.Pow(2, nbMatch - 1);
		}

		[GeneratedRegex("Card (?<id>\\d+):(?<win> \\d+)+ \\|(?<num> \\d+)+")]
		private static partial Regex CardRegex();
	}

	internal Day4a()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		string[] data = GetData();

		var cards = data.Select(x => new Card(x)).ToArray();

		Console.WriteLine("Day 4a: " + cards.Sum(x => x._sum));
	}
}
