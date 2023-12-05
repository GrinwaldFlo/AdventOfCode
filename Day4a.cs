using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023;
internal class Day4a : DayBase
{
	internal class Card
	{
		internal int Id;
		internal List<int> Win = new();
		internal List<int> Num = new();
		internal int Sum = 0;
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
			CalcSum();
		}

		internal void CalcSum()
		{
			int nbMatch = Num.Where(x => Win.Contains(x)).Select(x => 1).Sum();
			if (nbMatch <= 1)
				Sum = nbMatch;
			else
				Sum = (int)Math.Pow(2, nbMatch - 1);
		}
	}

	internal Day4a()
	{
		_name = GetType().Name;
	}

	internal override void Run()
	{
		string[] data = GetData();

		var cards = data.Select(x => new Card(x)).ToArray();

		Console.WriteLine("Day 4a: " + cards.Sum(x => x.Sum));
	}
}
