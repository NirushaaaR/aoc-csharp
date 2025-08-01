namespace AOC.Y2023;

using System.Linq;
using System.Text;

class Day12(string inputName = "input.txt") : BaseDay(2023, 12, inputName)
{

	Tuple<string, List<int>> ParseInput(string input)
	{
		string[] arg = input.Split(" ");
		List<int> pattern = [];
		foreach (string p in arg[1].Split(","))
		{
			pattern.Add(int.Parse(p));
		}
		return Tuple.Create(arg[0], pattern);	
	}

	bool isRowValidPattern(string row, List<int> pattern)
	{
		int countSharp = 0;
		int i = 0;
		foreach (char c in row)
		{
			if (c == '#')
			{
				countSharp++;
			}
			else if (countSharp > 0)
			{
				if (i+1 > pattern.Count)
				{
					return false;
				}
				if (pattern[i] != countSharp)
				{
					return false;
				}
				countSharp = 0;
				i++;
			}
		}
		return i + 1 >= pattern.Count;
	}

	double RecursiveSolve(string row, int currentIndex, List<int> pattern)
	{
		if (currentIndex >= row.Length)
		{
			return isRowValidPattern(row.ToString(), pattern) ? 1 : 0;
		}

		if (row[currentIndex] == '?')
		{
			StringBuilder sb = new(row);
			sb[currentIndex] = '.';
			double resultDot = RecursiveSolve(sb.ToString(), currentIndex + 1, pattern);
			sb[currentIndex] = '#';
			double resultHash = RecursiveSolve(sb.ToString(), currentIndex + 1, pattern);
			return resultDot + resultHash;
		}

		return RecursiveSolve(row, currentIndex + 1, pattern);
	}


	public override double Solve1()
	{
		foreach (var input in inputs)
		{
			var (row, pattern) = ParseInput(input);
			Console.WriteLine(RecursiveSolve(row, 0, pattern));
		}
		return 0;
	}

	public override double Solve2()
	{
		return 0;
	}

}