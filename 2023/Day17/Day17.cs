using System.Collections;

namespace AOC.Y2023;

class Day17(string inputName = "input.txt") : BaseDay(2023, 17, inputName)
{
	static int rightBound;
	static int bottomBound;

	int[,] ParseInput()
	{
		int m = inputs.Length;
		int n = inputs[0].Length;
		rightBound = n;
		bottomBound = m;
		int[,] maps = new int[m, n];
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				maps[i, j] = inputs[i][j] - '0';
			}
		}
		return maps;
	}

	static void PrintMap(int[,] maps, (int, int)[] crucibles)
	{
		for (int i = 0; i < bottomBound; i++)
		{
			for (int j = 0; j < rightBound; j++)
			{
				bool isPointPrinted = false;
				foreach (var crucible in crucibles)
				{
					if (crucible.Item1 == j && crucible.Item2 == i)
					{
						Console.Write('#');
						isPointPrinted = true;
						break;
					}
				}
				if (!isPointPrinted)
				{
					Console.Write(maps[i,j]);
					// Console.Write('O');
				}
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}

	enum Direction
	{
		UP, RIGHT, DOWN, LEFT
	}

	record Crucible(int x, int y, int heatLoss, int energy, Direction direction)
	{
		public readonly int x = x;
		public readonly int y = y;
		public readonly int heatLoss = heatLoss;
		public readonly int energy = energy;
		public readonly Direction direction = direction;
	}
	
	// check if crucible out of boud...
	static public bool IsValidPoint(int x, int y)
	{
		return x >= 0 && x < rightBound && y >= 0 && y < bottomBound;
	}

	class CrucibleTraversingEnumerator(Crucible crucible, int[,] maps) : IEnumerable<Crucible>
	{
		private readonly int[,] maps = maps;
		private readonly Crucible crucible = crucible;

		public IEnumerator<Crucible> GetEnumerator()
		{
			if (crucible.energy > 0)
			{
				var (x, y) = crucible.direction switch
				{
					Direction.UP => (crucible.x, crucible.y - 1),
					Direction.RIGHT => (crucible.x + 1, crucible.y),
					Direction.DOWN => (crucible.x, crucible.y + 1),
					Direction.LEFT => (crucible.x - 1, crucible.y),
					_ => throw new NotImplementedException(),
				};

				if (IsValidPoint(x, y))
				{

					yield return new(x, y, crucible.heatLoss + maps[y, x], crucible.energy - 1, crucible.direction);
				}
			}

			// turn 90 degree in either of the 2 direction? and reload energy
			switch (crucible.direction)
			{
				case Direction.UP:
				case Direction.DOWN:
					var (x, y) = (crucible.x - 1, crucible.y);
					if (IsValidPoint(x, y))
					{
						yield return new(x, y, crucible.heatLoss + maps[y, x], 2, Direction.LEFT);
					}
					(x, y) = (crucible.x + 1, crucible.y);
					if (IsValidPoint(x, y))
					{
						yield return new(x, y, crucible.heatLoss + maps[y, x], 2, Direction.RIGHT);
					}
					break;
				case Direction.LEFT:
				case Direction.RIGHT:
					(x, y) = (crucible.x, crucible.y - 1);
					if (IsValidPoint(x, y))
					{
						yield return new(x, y, crucible.heatLoss + maps[y, x], 2, Direction.UP);
					}
					(x, y) = (crucible.x, crucible.y + 1);
					if (IsValidPoint(x, y))
					{
						yield return new(x, y, crucible.heatLoss + maps[y, x], 2, Direction.DOWN);
					}
					break;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}


	public override double Solve1()
	{
		int[,] maps = ParseInput();

		// use priority queue to ensure that crucibles with the min heatloss got moved first
		PriorityQueue<Crucible, int> crucibles = new();
		HashSet<(int, int, int, Direction)> visited = [];

		// initialise
		crucibles.Enqueue(new(0, 0, 0, 3, Direction.RIGHT), 0);
		visited.Add((0, 0, 3, Direction.RIGHT));
		int minHeatLoss = int.MaxValue;
		var endPoint = (rightBound - 1, bottomBound - 1);

		while (crucibles.Count > 0)
		{
			Crucible crucible = crucibles.Dequeue();

			foreach (Crucible nextCrucible in new CrucibleTraversingEnumerator(crucible, maps))
			{
				if (nextCrucible.heatLoss >= minHeatLoss)
				{
					// current heatloss larger then the min heatloss? drop it
					continue;
				}

				var point = (nextCrucible.x, nextCrucible.y, nextCrucible.energy, nextCrucible.direction);

				if ((point.x, point.y) == endPoint)
				{
					// reach the end!
					minHeatLoss = Math.Min(minHeatLoss, nextCrucible.heatLoss);
					continue;
				}

				if (visited.Contains(point))
				{
					continue;
				}
				visited.Add(point);

				// keep going...
				crucibles.Enqueue(nextCrucible, nextCrucible.heatLoss);
			}
		}
		return minHeatLoss;
	}

	public override double Solve2()
	{
		return 0;
	}
}