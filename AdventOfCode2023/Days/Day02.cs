using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

internal partial class Day02 : DayBase
{
	//Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
	internal partial class GameItem
	{
		internal GameItem(Capture x)
		{
			var parser = ParserGame();
			var match = parser.Match(x.Value);
			if (match.Success)
			{
				for (int i = 0; i < match.Groups[2].Captures.Count; i++)
				{
					switch (match.Groups[3].Captures[i].Value)
					{
						case "green":
							Green = int.Parse(match.Groups[2].Captures[i].Value);
							break;
						case "blue":
							Blue = int.Parse(match.Groups[2].Captures[i].Value);
							break;
						case "red":
							Red = int.Parse(match.Groups[2].Captures[i].Value);
							break;
						default:
							break;
					}
				}
			}
		}

		internal int Green { get; set; }
		internal int Blue { get; set; }
		internal int Red { get; set; }

		[GeneratedRegex("((\\d+)([a-z]+),?)+;?")]
		private static partial Regex ParserGame();
	}

	internal partial class Game
	{
		internal int Id { get; set; }
		internal List<GameItem> Set { get; set; } = [];

		internal Game(string str)
		{
			var parser = ParserFull();
			var match = parser.Match(str.Replace(" ", ""));
			if (match.Success)
			{
				Id = int.Parse(match.Groups["id"].Value);

				Set = match.Groups["game"].Captures.Select(x => new GameItem(x)).ToList();
			}
		}

		[GeneratedRegex("[A-Za-z]+(?<id>\\d+):(?<game>(\\d+[a-z]+,?)+;?)+")]
		private static partial Regex ParserFull();

		internal bool CanPlay(int blue, int red, int green)
		{
			return !Set.Any(x => x.Green > green || x.Blue > blue || x.Red > red);
		}

		internal int GetPower()
		{
			int greenMax = Set.Select(x => x.Green).Max();
			int redMax = Set.Select(x => x.Red).Max();
			int blueMax = Set.Select(x => x.Blue).Max();
			return greenMax * redMax * blueMax;
		}
	}

	internal Day02()
	{
		_name = GetType().Name;
	}
	internal override void Run()
	{
		string[] data = GetData();

		var games = data.Select(x => new Game(x));
		int sum = games.Where(x => x.CanPlay(14, 12, 13)).Sum(x => x.Id);
		Console.WriteLine($"Sum is {sum}");

		Console.WriteLine($"Sum power is {games.Select(x => x.GetPower()).Sum()}");
	}
}
