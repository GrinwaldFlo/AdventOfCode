using System.Data;

namespace AdventOfCode2023.Days;

internal class Day10b : DayBase
{
	internal Day10b()
	{
		_name = GetType().Name[..^1];
	}

	internal override void Run()
	{
		string[] data = GetData();

		for (int y = 0; y < data.Length; y++)
		{
			int x = 0;
			Pos._map.Add([]);
			Pos._map[y] = data[y].Select(z => new Pos(z, x++, y)).ToList();
		}
		Pos._map.ForEach(a => a.ForEach(b => b.Attach()));
		var start = Find('S');
		start.AttachStart();
		Pos._map = Extend(Pos._map);
		FillHoles();
		Pos._map.ForEach(a => a.ForEach(b => b.Attach()));
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
		long distance = start._conn[1]!._distance / 4;
		Console.WriteLine($"Day 10a: {distance}");

		SetOutsideAround(Pos._map);

		int retry = 0;
		while (PropagateOutside(Pos._map) && retry++ < 5000)
		{
		}
		int cnt = CountInterne(Pos._map);
		Show(Pos._map);
		ShowDebug(Pos._map);

		Console.WriteLine($"Day 10b: {cnt}");
	}

	private static int CountInterne(List<List<Pos>> m)
	{
		int cnt = 0;
		for (int y = 0; y < m.Count; y++)
		{
			cnt += m[y].Count(x => !x._isOut && x._v == '.');
		}
		return cnt;
	}

	private static void ShowDebug(List<List<Pos>> m)
	{
		for (int y = 0; y < m.Count; y++)
		{
			Console.WriteLine(string.Join("", m[y].Select(x => x.Log)));
		}
	}

	private static void Show(List<List<Pos>> m)
	{
		for (int y = 0; y < m.Count; y++)
		{
			Console.WriteLine(string.Join("", m[y].Select(x => x._v)));
		}
	}

	private static bool PropagateOutside(List<List<Pos>> m)
	{
		bool hadPropagation = false;

		for (int y = 0; y < m.Count; y++)
		{
			for (int x = 0; x < m[y].Count; x++)
			{
				if (m[y][x]._isOut)
					hadPropagation |= m[y][x].Propagate();
			}
		}
		return hadPropagation;
	}

	private static void SetOutsideAround(List<List<Pos>> m)
	{
		m.First().Where(a => a.IsBlank).ToList().ForEach(a => a._isOut = true);
		m.Last().Where(a => a.IsBlank).ToList().ForEach(a => a._isOut = true);
		m.ForEach(x =>
		{
			if (x.First().IsBlank) x.First()._isOut = true;
			if (x.Last().IsBlank) x.Last()._isOut = true;
		});
	}

	private static Pos Find(char needle)
	{
		Pos? found = null;

		foreach (var a in Pos._map)
		{
			found = a.FirstOrDefault(b => b._v == needle);
			if (found != null) return found;
		}
		throw new Exception($"Needle {needle} not found");
	}

	internal static Pos[] Find(Pos from)
	{
		List<Pos> found = [];

		Pos._map.ForEach(a => found.AddRange(a.FindAll(b => b._conn.Any(c => c == from))));
		return [.. found];
	}

	private static void FillHoles()
	{
		for (int y = 0; y < Pos._map.Count; y++)
		{
			for (int x = 0; x < Pos._map[y].Count; x++)
			{
				if (Pos._map[y][x]._v == ' ')
				{
					var conn1 = Pos._map[y][x].GetFrom(-1, 0);
					var conn2 = Pos._map[y][x].GetFrom(1, 0);
					if (conn1 != null && conn2 != null && conn1.IsMutual(conn2))
					{
						Pos._map[y][x]._v = '-';
					}
					else
					{
						conn1 = Pos._map[y][x].GetFrom(0, -1);
						conn2 = Pos._map[y][x].GetFrom(0, 1);
						if (conn1 != null && conn2 != null && conn1.IsMutual(conn2))
							Pos._map[y][x]._v = '|';
					}
				}
			}
		}
	}

	private static List<List<Pos>> Extend(List<List<Pos>> data)
	{
		List<List<Pos>> result = [];

		for (int y = 0; y < data.Count; y++)
		{
			result.Add([]);
			result.Add([]);
			result[^1] = [];
			result[^2] = [];
			for (int x = 0; x < data[y].Count; x++)
			{
				result[^2].Add(data[y][x]);
				result[^2].Add(Pos.GetEmpty());
				result[^1].Add(Pos.GetEmpty());
				result[^1].Add(Pos.GetEmpty());
			}
		}
		for (int y = 0; y < result.Count; y++)
		{
			for (int x = 0; x < result[y].Count; x++)
			{
				result[y][x]._x = x;
				result[y][x]._y = y;
			}
		}
		return result;
	}

	internal class Pos(char value, int x, int y)
	{
		public override string ToString()
		{
			return $"{_v} - I:{_x};{_y} - D:{_distance} - [{_conn[0]?._x};{_conn[0]?._y},{_conn[1]?._x};{_conn[1]?._y}]";
		}

		internal static List<List<Pos>> _map = [];

		internal Pos?[] _conn = new Pos?[2];
		internal int _x = x;
		internal bool _isOut = false;
		internal int _y = y;
		internal bool _isStart;
		internal char _v = value;
		internal int _distance = 0;
		internal bool IsBlank => _v is ' ' or '.';

		internal char Log
		{
			get
			{
				if (_v == ' ' && _isOut)
					return '%';
				if (_v == '.' && _isOut)
					return '*';
				return _v;
			}
		}

		internal void Attach()
		{
			switch (_v)
			{
				case 'S':
					_isStart = true;
					break;

				case '|':
					_conn[0] = TryGet(_x, _y - 1);
					_conn[1] = TryGet(_x, _y + 1);
					break;

				case '-':
					_conn[0] = TryGet(_x - 1, _y);
					_conn[1] = TryGet(_x + 1, _y);
					break;

				case 'L':
					_conn[0] = TryGet(_x, _y - 1);
					_conn[1] = TryGet(_x + 1, _y);
					break;

				case 'J':
					_conn[0] = TryGet(_x, _y - 1);
					_conn[1] = TryGet(_x - 1, _y);
					break;

				case '7':
					_conn[0] = TryGet(_x, _y + 1);
					_conn[1] = TryGet(_x - 1, _y);
					break;

				case 'F':
					_conn[0] = TryGet(_x + 1, _y);
					_conn[1] = TryGet(_x, _y + 1);
					break;

				default:
					break;
			}
		}

		internal void AttachStart()
		{
			_conn = Find(this);
		}

		private static Pos? TryGet(int x, int y)
		{
			return x < 0 || y < 0 || x >= _map.Count || y >= _map[x].Count ? null : _map[y][x];
		}

		internal Pos MoveTo(Pos from)
		{
			_distance = from._distance + 1;
			return (_conn[0] == from ? _conn[1] : _conn[0])!;
		}

		internal static Pos GetEmpty()
		{
			return new Pos(' ', -1, -1);
		}

		internal Pos? GetFrom(int xDiff, int yDiff)
		{
			return TryGet(_x - xDiff, _y - yDiff);
		}

		internal bool IsMutual(Pos mutual)
		{
			return (this == mutual._conn[0] || this == mutual._conn[1])
				&&
				(mutual == _conn[0] || mutual == _conn[1]);
		}

		internal bool Propagate()
		{
			if (!_isOut)
				return false;
			bool hasChange = false;
			hasChange |= GetFrom(-1, 0)?.SetOutside() ?? false;
			hasChange |= GetFrom(0, 1)?.SetOutside() ?? false;
			hasChange |= GetFrom(1, 0)?.SetOutside() ?? false;
			hasChange |= GetFrom(0, -1)?.SetOutside() ?? false;
			return hasChange;
		}

		private bool SetOutside()
		{
			if (!IsBlank || _isOut)
				return false;
			_isOut = true;
			return true;
		}
	}
}