namespace AOC.Y2023;

using System.Linq;
using System.Text;

class Day12(string inputName = "input.txt") : BaseDay(2023, 12, inputName)
{
	readonly Dictionary<string, double> answerCache = [];

	static Tuple<string, List<int>> ParseInput(string input)
	{
		string[] arg = input.Split(" ");
		List<int> pattern = [];
		foreach (string p in arg[1].Split(","))
		{
			pattern.Add(int.Parse(p));
		}
		return Tuple.Create(arg[0], pattern);
	}

	static Tuple<string, List<int>> ParseInput2(string input)
	{
		var (row, _pattern) = ParseInput(input);
		List<int> pattern = [.. _pattern];
		StringBuilder sb = new(row);
		for (int i = 0; i < 4; i++)
		{
			sb.Append("?" + row);
			pattern.AddRange(_pattern);
		}
		return Tuple.Create(sb.ToString(), pattern);
	}

	double RecursiveSolve(string records, List<int> springs)
	{
		if (records == "")
		{
			return springs.Count == 0 ? 1 : 0;
		}

		if (springs.Count == 0)
		{
			return !records.Contains('#') ? 1 : 0;
		}

		char record = records[0];

		if (record == '.')
		{
			// ignore until next # or ?
			return RecursiveSolve(records[1..], springs);
		}

		// get the cached answer if already calculate this parts
		string cacheKey = records + "|" + string.Join(",", springs);
		if (answerCache.TryGetValue(cacheKey, out double result))
		{
			Console.WriteLine("Cache Hit!");
			Utils.PrintDict(answerCache);
			return result;
		}

		if (record == '#')
		{
			// count # if it's the same with current number of spring
			int spring = springs.First();
			result = 0;
			if (
				records.Length >= spring // current length is long enough for spring
				&& !records[..spring].Contains('.') // no . in between
				&& (
					records.Length == spring // at the ends of record
					|| records[spring] != '#' // next character is not spring
				)
			)
			{
				result = RecursiveSolve(records.Length > spring ? records[(spring + 1)..] : "", springs[1..]);
			}
			answerCache[cacheKey] = result;
			return result;
		}

		if (record == '?')
		{
			result = RecursiveSolve('.' + records[1..], springs) + RecursiveSolve('#' + records[1..], springs);
			answerCache[cacheKey] = result;
			return result;
		}

		// should not reach here...
		throw new Exception($"unhandle record: {record}");
	}

	public override double Solve1()
	{
		List<double> answer = [];
		foreach (var input in inputs)
		{
			var (row, pattern) = ParseInput(input);
			answerCache.Clear();
			answer.Add(RecursiveSolve(row, pattern));
		}
		return answer.Sum();
	}

	public override double Solve2()
	{
		List<double> answer = [];
		foreach (var input in inputs)
		{
			var (row, pattern) = ParseInput2(input);
			answerCache.Clear();
			answer.Add(RecursiveSolve(row, pattern));
		}
		return answer.Sum();
	}

}