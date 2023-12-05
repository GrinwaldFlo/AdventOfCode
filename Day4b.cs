using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023;
internal class Day4b : DayBase
{
	internal class Card
	{
		internal int Id;
		internal List<int> Win = new();
		internal List<int> Num = new();

		internal Card(string v)
		{
			Regex r = new Regex("Card (?<id>\\d+):(?<win> \\d+)+ \\|(?<num> \\d+)+");
			var result = r.Match(v.Replace("  ", " ").Replace("  ", " "));
			if (result.Success)
			{
				Id = int.Parse(result.Groups["id"].Value);
				Win = result.Groups["win"].Captures.Select(x => int.Parse(x.Value)).ToList();
				Num = result.Groups["num"].Captures.Select(x => int.Parse(x.Value)).ToList();
			}
		}

		internal int CalcSum(Card[] cards)
		{
			int nbMatch = Num.Count(x => Win.Contains(x));

			var winCards = cards.Where(x => x.Id > Id && x.Id < Id + 1 + nbMatch);
			return 1 + winCards.Sum(x => x.CalcSum(cards));
		}
	}

	internal Day4b()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		string[] data = GetData();

		var cards = data.Select(x => new Card(x)).ToArray();

		int sum = cards.Sum(x => x.CalcSum(cards));

		Console.WriteLine("Day 4b: " + sum);
	}
}
