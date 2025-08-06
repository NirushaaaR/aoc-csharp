using System.Collections;
using System.Text;

namespace AOC.Y2023;
// 35335
class Day14(string inputName = "input.txt") : BaseDay(2023, 14, inputName)
{
	char[,] lava;
	readonly List<string> lavaPattern = [];

	string LavaToString()
	{
		StringBuilder sb = new();
		int n = lava.GetLength(0);
		int m = lava.GetLength(1);
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++)
			{
				sb.Append(lava[i, j]);
			}
			if (i + 1 != n)
			{
				sb.Append('\n');
			}
		}
		return sb.ToString();
	}

	void PrintLava()
	{
		Console.WriteLine(LavaToString());
	}

	int CheckCycleRepeat()
	{
		string lavaString = LavaToString();
		int index = lavaPattern.IndexOf(lavaString);
		if (index == -1)
		{
			lavaPattern.Add(lavaString);
		}
		return index;
	}

	char[,] ParsedInput(string[] inputs)
	{
		int n = inputs.Length;
		int m = inputs[0].Length;
		lava = new char[n, m];
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++)
			{
				// Rotate the array
				lava[i, j] = inputs[i][j];
			}
		}
		return lava;
	}

	void TiltNort()
	{
		int n = lava.GetLength(0);
		int m = lava.GetLength(1);
		for (int j = 0; j < m; j++)
		{
			int slideToIndex = 0;
			for (int i = 0; i < n; i++)
			{
				switch (lava[i, j])
				{
					case '#':
						// new support
						slideToIndex = i + 1;
						break;
					case 'O':
						if (slideToIndex != i)
						{
							// slide the rock
							lava[slideToIndex, j] = 'O';
							lava[i, j] = '.';
						}
						slideToIndex++;
						break;
				}
			}
		}
	}

	void TiltWest()
	{
		int n = lava.GetLength(0);
		int m = lava.GetLength(1);
		for (int i = 0; i < n; i++)
		{
			int slideToIndex = 0;
			for (int j = 0; j < m; j++)
			{
				switch (lava[i, j])
				{
					case '#':
						// new support
						slideToIndex = j + 1;
						break;
					case 'O':
						if (slideToIndex != j)
						{
							// slide the rock
							lava[i, slideToIndex] = 'O';
							lava[i, j] = '.';
						}
						slideToIndex++;
						break;
				}
			}
		}
	}

	void TiltSouth()
	{
		int n = lava.GetLength(0);
		int m = lava.GetLength(1);
		for (int j = 0; j < m; j++)
		{
			int slideToIndex = n-1;
			for (int i = n - 1; i >= 0; i--)
			{
				switch (lava[i, j])
				{
					case '#':
						// new support
						slideToIndex = i - 1;
						break;
					case 'O':
						if (slideToIndex != i)
						{
							// slide the rock
							lava[slideToIndex, j] = 'O';
							lava[i, j] = '.';
						}
						slideToIndex--;
						break;
				}
			}
		}
	}

	void TiltEast()
	{
		int n = lava.GetLength(0);
		int m = lava.GetLength(1);
		for (int i = 0; i < n; i++)
		{
			int slideToIndex = m-1;
			for (int j = m-1; j >=0; j--)
			{
				switch (lava[i, j])
				{
					case '#':
						// new support
						slideToIndex = j - 1;
						break;
					case 'O':
						if (slideToIndex != j)
						{
							// slide the rock
							lava[i, slideToIndex] = 'O';
							lava[i, j] = '.';
						}
						slideToIndex--;
						break;
				}
			}
		}
	}


	int SumLoad()
	{
		int n = lava.GetLength(0);
		int m = lava.GetLength(1);
		int load = 0;
		for (int j = 0; j < m; j++)
		{
			for (int i = 0; i < n; i++)
			{
				if (lava[i, j] == 'O')
				{
					load += n - i;
				}
			}
		}
		return load;
	}

	public override double Solve1()
	{
		ParsedInput(inputs);
		TiltNort();
		return SumLoad();
	}

	public override double Solve2()
	{
		ParsedInput(inputs);
		int cycleRepeatIndex = -1;
		int initialCycleRepeat = -1;
		const int count = 1_000_000_000;

		for (int i = 0; i < count; i++)
		{
			// cycle always repeat 7 times after the first time it repeat
			if (initialCycleRepeat != -1)
			{
				if (count % i == 0)
				{
					break;
				}
				// increment the index instead of keep tilting the lava;
				if (++cycleRepeatIndex > initialCycleRepeat + 6)
				{
					cycleRepeatIndex = initialCycleRepeat;
				}
				continue;
			}

			TiltNort();
			TiltWest();
			TiltSouth();
			TiltEast();

			initialCycleRepeat = CheckCycleRepeat();
			if (initialCycleRepeat != -1)
			{
				cycleRepeatIndex = initialCycleRepeat;
			}
		}
		ParsedInput(lavaPattern[cycleRepeatIndex].Split("\n"));
		return SumLoad();
	}

}