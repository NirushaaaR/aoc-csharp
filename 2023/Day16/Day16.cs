using System.Diagnostics;

namespace AOC.Y2023;

class Day16(string inputName = "input.txt") : BaseDay(2023, 16, inputName)
{
	enum Direction
	{
		UP, RIGHT, DOWN, LEFT
	}

	class Light(Direction direction, int x, int y)
	{
		public Direction direction = direction;
		public int x = x;
		public int y = y;


		public void Move()
		{
			switch (direction)
			{
				case Direction.UP:
					--y;
					break;
				case Direction.RIGHT:
					++x;
					break;
				case Direction.DOWN:
					++y;
					break;
				case Direction.LEFT:
					--x;
					break;
			}
		}

		// react to space
		// if the light is splited return another splited Light
		// else return null
		public Light ReactToSpace(char space)
		{
			if (space == '/')
			{
				direction = direction switch
				{
					Direction.UP => Direction.RIGHT,
					Direction.RIGHT => Direction.UP,
					Direction.DOWN => Direction.LEFT,
					Direction.LEFT => Direction.DOWN,
					_ => throw new NotImplementedException(),
				};
			}
			else if (space == '\\')
			{
				direction = direction switch
				{
					Direction.UP => Direction.LEFT,
					Direction.RIGHT => Direction.DOWN,
					Direction.DOWN => Direction.RIGHT,
					Direction.LEFT => Direction.UP,
					_ => throw new NotImplementedException(),
				};
			}
			else if (space == '-' && (direction == Direction.UP || direction == Direction.DOWN))
			{
				direction = Direction.LEFT;
				return new(Direction.RIGHT, x, y);
			}
			else if (space == '|' && (direction == Direction.LEFT || direction == Direction.RIGHT))
			{
				direction = Direction.UP;
				return new(Direction.DOWN, x, y);
			}

			return null;
		}

		public bool IsOutOfBound(int rightBound, int bottomBound)
		{
			if (x < 0 || x >= rightBound)
			{
				return true;
			}
			if (y < 0 || y >= bottomBound)
			{
				return true;
			}
			return false;
		}
	}

	char[,] ParseInput()
	{
		int m = inputs.Length;
		int n = inputs[0].Length;
		char[,] maps = new char[m, n];
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				maps[i, j] = inputs[i][j];
			}
		}
		return maps;
	}

	static void PrintMap(char[,] maps, Light[] lights)
	{
		int m = maps.GetLength(0);
		int n = maps.GetLength(1);
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				bool isPrinted = false;
				foreach (var light in lights)
				{
					if (light.x == j && light.y == i)
					{
						char lightMark = light.direction switch
						{
							Direction.RIGHT => '>',
							Direction.LEFT => '<',
							Direction.UP => '^',
							Direction.DOWN => 'v',
							_ => throw new NotImplementedException(),
						};
						isPrinted = true;
						Console.Write(lightMark);
					}
				}
				if (!isPrinted) Console.Write(maps[i, j]);
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}

	static int LightTraversing(char[,] maps, Light startingLight)
	{
		int bottomBound = maps.GetLength(0);
		int rightBound = maps.GetLength(1);
		HashSet<(Direction, int, int)> visited = [(startingLight.direction, startingLight.x, startingLight.y)]; // has visited this x,y space with this direction before
		HashSet<(int, int)> energizedTile = [(startingLight.x, startingLight.y)];

		Queue<Light> lights = [];
		// start with one light
		lights.Enqueue(startingLight);

		while (lights.Count > 0)
		{
			Light light = lights.Dequeue();
			// react to the current space (change direction? splited light?)
			Light splitedLight = light.ReactToSpace(maps[light.y, light.x]);
			if (splitedLight != null)
			{
				lights.Enqueue(splitedLight);
			}

			// Move the light and check out bound!
			light.Move();
			if (light.IsOutOfBound(rightBound, bottomBound))
			{
				continue;
			}

			// check if already visited with this pattern before, discard the lgiht
			var pattern = (light.direction, light.x, light.y);
			if (visited.Contains(pattern))
			{
				continue;
			}
			visited.Add(pattern);
			energizedTile.Add((light.x, light.y));
			// enqueu the light to move next round
			lights.Enqueue(light);
		}
		return energizedTile.Count;
	}


	public override double Solve1()
	{
		char[,] maps = ParseInput();
		return LightTraversing(maps, new(Direction.RIGHT, 0, 0));
	}

	public override double Solve2()
	{
		char[,] maps = ParseInput();
		int bottomBound = maps.GetLength(0);
		int rightBound = maps.GetLength(1);
		int energizedTile = -1;
		// start from every corner!!
		for (int x = 0; x < rightBound; x++)
		{
			energizedTile = Math.Max(energizedTile, LightTraversing(maps, new(Direction.DOWN, x, 0)));
			energizedTile = Math.Max(energizedTile, LightTraversing(maps, new(Direction.UP, x, bottomBound - 1)));
		}
		for (int y = 0; y < bottomBound; y++)
		{
			energizedTile = Math.Max(energizedTile, LightTraversing(maps, new(Direction.RIGHT, 0, y)));
			energizedTile = Math.Max(energizedTile, LightTraversing(maps, new(Direction.LEFT, rightBound - 1, y)));
		}

		return energizedTile;
	}
}