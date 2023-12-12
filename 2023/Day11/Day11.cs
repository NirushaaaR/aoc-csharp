namespace AOC.Y2023;

class Day11(string inputName = "input.txt") : BaseDay(2023, 11, inputName)
{
	public override double Solve1()
	{
		HashSet<int> verticalExpanedAt = [];
		for (int i = 0; i < inputs.Length; i++)
		{
			bool galaxyExists = false;
			for (int j = 0; j < inputs[i].Length; j++)
			{
				if (inputs[i][j] == '#')
				{
					galaxyExists = true;
				}
			}

			if (!galaxyExists)
			{
				verticalExpanedAt.Add(i);
			}
		}

		HashSet<int> horizontalExpanedAt = [];
		for (int i = 0; i < inputs[0].Length; i++)
		{
			bool galaxyExists = false;
			for (int j = 0; j < inputs.Length; j++)
			{
				if (inputs[j][i] == '#')
				{
					galaxyExists = true;
				}
			}

			if (!galaxyExists)
			{
				horizontalExpanedAt.Add(i);
			}
		}

		List<(int, int)> galaxyPositions = [];
		int horizontalExpaned = 0;
		int verticalExpaned = 0;
		for (int i = 0; i < inputs.Length; i++)
		{

			if (verticalExpanedAt.Contains(i))
			{
				verticalExpaned++;
				continue;
			}

			for (int j = 0; j < inputs[i].Length; j++)
			{
				if (horizontalExpanedAt.Contains(j))
				{
					horizontalExpaned++;
					continue;
				}

				if (inputs[i][j] == '#')
				{
					galaxyPositions.Add((i + verticalExpaned, j + horizontalExpaned));
				}
			}
			horizontalExpaned = 0;
		}

		int sum = 0;
		(int, int)[] galaxyPositionsArr = [.. galaxyPositions];
		for (int i = 0; i < galaxyPositionsArr.Length - 1; i++)
		{
			for (int j = i + 1; j < galaxyPositionsArr.Length; j++)
			{
				var pointA = galaxyPositions[i];
				var pointB = galaxyPositions[j];
				int shortesstPath = Math.Abs(pointA.Item1 - pointB.Item1) + Math.Abs(pointA.Item2 - pointB.Item2);
				sum += shortesstPath;
			}
		}

		return sum;
	}

	public override double Solve2()
	{
		HashSet<int> verticalExpanedAt = [];
		for (int i = 0; i < inputs.Length; i++)
		{
			bool galaxyExists = false;
			for (int j = 0; j < inputs[i].Length; j++)
			{
				if (inputs[i][j] == '#')
				{
					galaxyExists = true;
					break;
				}
			}

			if (!galaxyExists)
			{
				verticalExpanedAt.Add(i);
			}
		}

		HashSet<int> horizontalExpanedAt = [];
		for (int i = 0; i < inputs[0].Length; i++)
		{
			bool galaxyExists = false;
			for (int j = 0; j < inputs.Length; j++)
			{
				if (inputs[j][i] == '#')
				{
					galaxyExists = true;
					break;
				}
			}

			if (!galaxyExists)
			{
				horizontalExpanedAt.Add(i);
			}
		}

		List<(long, long)> galaxyPositions = [];
		long horizontalExpaned = 0;
		long verticalExpaned = 0;
		long expanedSize = 1000000;
		for (int i = 0; i < inputs.Length; i++)
		{

			if (verticalExpanedAt.Contains(i))
			{
				verticalExpaned += expanedSize - 1;
				continue;
			}

			for (int j = 0; j < inputs[i].Length; j++)
			{
				if (horizontalExpanedAt.Contains(j))
				{
					horizontalExpaned += expanedSize - 1;
					continue;
				}

				if (inputs[i][j] == '#')
				{
					galaxyPositions.Add((i + verticalExpaned, j + horizontalExpaned));
				}
			}
			horizontalExpaned = 0;
		}

		long sum = 0;
		(long, long)[] galaxyPositionsArr = [.. galaxyPositions];
		for (int i = 0; i < galaxyPositionsArr.Length - 1; i++)
		{
			for (int j = i + 1; j < galaxyPositionsArr.Length; j++)
			{
				var (Ax, Ay) = galaxyPositions[i];
				var (Bx, By) = galaxyPositions[j];
				long shortesstPath = Math.Abs(Ax - Bx) + Math.Abs(Ay - By);
				sum += shortesstPath;
			}
		}

		return sum;
	}

}