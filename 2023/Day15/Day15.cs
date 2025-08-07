using System.Text;

namespace AOC.Y2023;

class Day15(string inputName = "input.txt") : BaseDay(2023, 15, inputName)
{
	static int Hash(string text)
	{
		int value = 0;
		foreach (char c in text)
		{
			value = (value + c) * 17 % 256;
		}
		return value;
	}

	public override double Solve1()
	{
		int sum = 0;
		foreach (var input in inputs[0].Split(","))
		{
			sum += Hash(input);
		}
		return sum;
	}

	public override double Solve2()
	{
		return 0;
	}
}