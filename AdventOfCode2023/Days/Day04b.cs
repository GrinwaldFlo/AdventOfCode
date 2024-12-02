using System.Text.RegularExpressions;
namespace AdventOfCode2023.Days;

internal partial class Day04b : DayBase
{
	internal partial class Card
	{
		internal int _id;
		internal List<int> _win = [];
		internal List<int> _num = [];

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
		}

		internal int CalcSum(Card[] cards)
		{
			int nbMatch = _num.Count(_win.Contains);

			var winCards = cards.Where(x => x._id > _id && x._id < _id + 1 + nbMatch);
			return 1 + winCards.Sum(x => x.CalcSum(cards));
		}

		[GeneratedRegex("Card (?<id>\\d+):(?<win> \\d+)+ \\|(?<num> \\d+)+")]
		private static partial Regex CardRegex();
	}

	internal Day04b()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		string[] data = GetData();

		var cards = data.Select(x => new Card(x)).ToArray();

		int sum = cards.Sum(x => x.CalcSum(cards));

		Console.WriteLine("Day 4b: " + sum);
	}
}
