namespace AdventOfCode2024.Days;

internal abstract class DayBase
{
	internal abstract void Run();

	internal string _name = "";

	internal string[] GetData()
	{
		return File.ReadAllLines($"Ressources\\{_name}.txt");
	}
}