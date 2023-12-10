namespace AdventOfCode2023;
internal class Day7b : DayBase
{
	internal class Card
	{
		internal char _sign;
		internal int _val;

		public Card(char sign)
		{
			_sign = sign;

			_val = _sign switch
			{
				'A' => 14,
				'K' => 13,
				'Q' => 12,
				'J' => 11,
				'T' => 10,
				'9' => 9,
				'8' => 8,
				'7' => 7,
				'6' => 6,
				'5' => 5,
				'4' => 4,
				'3' => 3,
				'2' => 2,
				_ => 0,
			};
		}
	}

	internal class Hand
	{
		internal int[] _cards;
		internal long _bid;
		internal long _value;
		internal string _strCards;
		internal int _jocker;
		public Hand(string val)
		{

			string[] tmp = val.Split(' ');
			_strCards = tmp[0];
			_cards = [.. tmp[0].Select(SignToVal)];
			_bid = long.Parse(tmp[1]);

			if (_strCards.Contains('J'))
			{
				long tmpValue = 0;
				for (int i = 1; i < 14; i++)
				{
					tmpValue = CalcValue(_cards.Select(x => x == 1 ? i : x).ToArray());
					if (tmpValue > _value)
					{
						_jocker = i;
						_value = tmpValue;
					}
				}
			}
			else
			{
				_value = CalcValue(_cards);
			}
		}

		private long CalcValue(int[] cards)
		{
			var map = cards.OrderByDescending(x => x).GroupBy(x => x).OrderByDescending(x => x.Count()).ToList();

			long result = 0;
			// This score is calculated from original score (to have jocker as min points)
			for (int i = 0; i < _cards.Length; i++)
			{
				result += _cards[i] * (long)Math.Pow(100, _cards.Length - i - 1);
			}

			if (map[0].Count() == 5)
				return 90000000000 + result;
			if (map[0].Count() == 4)
				return 80000000000 + result;
			if (map[0].Count() == 3 && map[1].Count() == 2)
				return 70000000000 + result;
			if (map[0].Count() == 3)
				return 60000000000 + result;
			if (map[0].Count() == 2 && map[1].Count() == 2)
				return 50000000000 + result;
			if (map[0].Count() == 2)
				return 40000000000 + result;
			return result;
		}

		private int SignToVal(char sign)
		{
			return sign switch
			{
				'A' => 13,
				'K' => 12,
				'Q' => 11,
				'J' => 1,
				'T' => 10,
				'9' => 9,
				'8' => 8,
				'7' => 7,
				'6' => 6,
				'5' => 5,
				'4' => 4,
				'3' => 3,
				'2' => 2,
				_ => 0,
			};
		}
	}

	internal Day7b()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		List<string> data = [.. GetData()];
		var hands = data.Select(data => new Hand(data)).OrderBy(x => x._value).GroupBy(x => x._value).ToArray();

		long result = 0;
		for (int i = 0; i < hands.Length; i++)
		{
			for (int j = 0; j < hands[i].Count(); j++)
			{
				var hand = hands[i].ToArray()[j];
				result += hand._bid * (i + 1);
				Console.WriteLine($"{j} - {hand._strCards} - {hand._value} = {hand._bid} * {i + 1} | Jocker:{hand._jocker}");
			}
		}

		Console.WriteLine($"Day 7b: {result}");
	}
}