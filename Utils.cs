namespace AOC;

class Utils
{
	public static void PrintDict<T, V>(Dictionary<T, V> dict) where T : notnull
	{
		Console.Write('{');
		foreach (var kvp in dict)
		{
			Console.Write($"[{kvp.Key}]:[{kvp.Value}], ");
		}
		Console.WriteLine("}");
	}
}