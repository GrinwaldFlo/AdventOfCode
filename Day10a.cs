using System.Data;

namespace AdventOfCode2023;
internal class Day10a : DayBase
{
	internal Day10a()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		string[] data = GetData();
		Pos._w = data.First().Length;

		string s = string.Join("", data);
		int index = 0;
		Pos._map = s.Select(s => new Pos(s, index++)).ToList();
		Pos._map.ForEach(x => x.Attach());

		var start = Pos._map.First(x => x._v == 'S');
		start.AttachStart();
		start._distance = 1;
		var from = start;
		var cur = start._conn[0]!;

		Pos next;

		while (cur != start)
		{
			next = cur.MoveTo(from);
			from = cur;
			cur = next;
		}
		long distance = start._conn[1]!._distance / 2;
		Console.WriteLine($"Day 10a: {distance}");
	}

	internal class Pos(char value, int index)
	{
		public override string ToString()
		{
			return $"{_v} - I:{_index} - D:{_distance} - [{_conn[0]?._index},{_conn[1]?._index}]";
		}
		internal static List<Pos> _map = [];
		internal static int _w;

		internal Pos?[] _conn = new Pos?[2];
		internal int _index = index;
		internal bool _isStart;
		internal char _v = value;
		internal int _distance = 0;
		internal void Attach()
		{
			switch (_v)
			{
				case 'S':
					_isStart = true;
					break;
				case '|':
					_conn[0] = TryGet(_index, -_w);
					_conn[1] = TryGet(_index, _w);
					break;
				case '-':
					_conn[0] = TryGet(_index, -1);
					_conn[1] = TryGet(_index, 1);
					break;
				case 'L':
					_conn[0] = TryGet(_index, -_w);
					_conn[1] = TryGet(_index, 1);
					break;
				case 'J':
					_conn[0] = TryGet(_index, -_w);
					_conn[1] = TryGet(_index, -1);
					break;
				case '7':
					_conn[0] = TryGet(_index, _w);
					_conn[1] = TryGet(_index, -1);
					break;
				case 'F':
					_conn[0] = TryGet(_index, _w);
					_conn[1] = TryGet(_index, 1);
					break;
				default:
					break;
			}
		}

		internal void AttachStart()
		{
			_conn = _map.Where(x => x._conn.Any(y => y == this)).ToArray();
		}

		private static Pos? TryGet(int i, int offset)
		{
			if (i % _w == 0 && offset is -1)
				return null;
			if (i % _w == _w - 1 && offset is 1)
				return null;
			if (offset == _w && i + offset >= _map.Count)
				return null;
			if (offset == -_w && i + offset < 0)
				return null;

			return _map[i + offset];
		}

		internal Pos MoveTo(Pos from)
		{
			_distance = from._distance + 1;
			return (_conn[0] == from ? _conn[1] : _conn[0])!;
		}
	}
}