namespace AOC.Y2023;

class Day10(string inputName = "input.txt") : BaseDay(2023, 10, inputName)
{

	enum Direction
	{
		TOP,
		DOWN,
		LEFT,
		RIGHT
	}

	public override double Solve1()
	{
		int rowSize = inputs.Length;
		int colSize = inputs[0].Length;
		int iStart = -1;
		int jStart = -1;
		int i, j;

		// find start position
		for (i = 0; i < rowSize; i++)
		{
			for (j = 0; j < colSize; j++)
			{
				if (inputs[i][j] == 'S')
				{
					iStart = i;
					jStart = j;
					break;
				}
			}

			if (iStart != -1)
			{
				break;
			}
		}

		// travel from the start position until it found the start position again
		i = iStart;
		j = jStart;
		Direction comeFrom = Direction.TOP;
		int distance = 0;
		do
		{
			var currentPipe = inputs[i][j];

			if (currentPipe == 'S')
			{
				if (i - 1 > 0 && "|F7".Contains(inputs[i - 1][j]))
				{
					i--;
					comeFrom = Direction.DOWN;
				}
				else if (i + 1 < rowSize && "|LJ".Contains(inputs[i + 1][j]))
				{
					i++;
					comeFrom = Direction.TOP;
				}
				else if (j - 1 > 0 && "-LF".Contains(inputs[i][j - 1]))
				{
					j--;
					comeFrom = Direction.RIGHT;
				}
				else if (j + 1 < colSize && "-7J".Contains(inputs[i][j + 1]))
				{
					j++;
					comeFrom = Direction.LEFT;
				}
			}
			else if (currentPipe == '|')
			{
				if (comeFrom == Direction.TOP)
				{
					i++;
				}
				else if (comeFrom == Direction.DOWN)
				{
					i--;
				}
			}
			else if (currentPipe == '-')
			{
				if (comeFrom == Direction.LEFT)
				{
					j++;
				}
				else if (comeFrom == Direction.RIGHT)
				{
					j--;
				}
			}
			else if (currentPipe == 'L')
			{
				if (comeFrom == Direction.TOP)
				{
					j++;
					comeFrom = Direction.LEFT;
				}
				else if (comeFrom == Direction.RIGHT)
				{
					i--;
					comeFrom = Direction.DOWN;
				}
			}
			else if (currentPipe == 'J')
			{
				if (comeFrom == Direction.TOP)
				{
					j--;
					comeFrom = Direction.RIGHT;
				}
				else if (comeFrom == Direction.LEFT)
				{
					i--;
					comeFrom = Direction.DOWN;
				}
			}
			else if (currentPipe == '7')
			{
				if (comeFrom == Direction.LEFT)
				{
					i++;
					comeFrom = Direction.TOP;
				}
				else if (comeFrom == Direction.DOWN)
				{
					j--;
					comeFrom = Direction.RIGHT;
				}
			}
			else if (currentPipe == 'F')
			{
				if (comeFrom == Direction.RIGHT)
				{
					i++;
					comeFrom = Direction.TOP;
				}
				else if (comeFrom == Direction.DOWN)
				{
					j++;
					comeFrom = Direction.LEFT;
				}
			}
			distance++;
		} while (i != iStart || j != jStart);

		return distance / 2;
	}

	public override double Solve2()
	{
		int rowSize = inputs.Length;
		int colSize = inputs[0].Length;
		int iStart = -1;
		int jStart = -1;
		int i, j;

		// find start position
		for (i = 0; i < rowSize; i++)
		{
			for (j = 0; j < colSize; j++)
			{
				if (inputs[i][j] == 'S')
				{
					iStart = i;
					jStart = j;
					break;
				}
			}

			if (iStart != -1)
			{
				break;
			}
		}


		HashSet<(int, int)> partsOfLoops = [];
		int distance = 0;
		i = iStart;
		j = jStart;
		Direction startDirection = Direction.TOP;
		Direction comeFrom = Direction.TOP;
		do
		{
			var currentPipe = inputs[i][j];
			partsOfLoops.Add((i, j));

			if (currentPipe == 'S')
			{
				if (i - 1 > 0 && "|F7".Contains(inputs[i - 1][j]))
				{
					i--;
					startDirection = Direction.TOP;
					comeFrom = Direction.DOWN;
				}
				else if (i + 1 < rowSize && "|LJ".Contains(inputs[i + 1][j]))
				{
					i++;
					startDirection = Direction.DOWN;
					comeFrom = Direction.TOP;
				}
				else if (j - 1 > 0 && "-LF".Contains(inputs[i][j - 1]))
				{
					j--;
					startDirection = Direction.LEFT;
					comeFrom = Direction.RIGHT;
				}
				else if (j + 1 < colSize && "-7J".Contains(inputs[i][j + 1]))
				{
					j++;
					startDirection = Direction.RIGHT;
					comeFrom = Direction.LEFT;
				}
			}
			else if (currentPipe == '|')
			{
				if (comeFrom == Direction.TOP)
				{
					i++;
				}
				else if (comeFrom == Direction.DOWN)
				{
					i--;
				}
			}
			else if (currentPipe == '-')
			{
				if (comeFrom == Direction.LEFT)
				{
					j++;
				}
				else if (comeFrom == Direction.RIGHT)
				{
					j--;
				}
			}
			else if (currentPipe == 'L')
			{
				if (comeFrom == Direction.TOP)
				{
					j++;
					comeFrom = Direction.LEFT;
				}
				else if (comeFrom == Direction.RIGHT)
				{
					i--;
					comeFrom = Direction.DOWN;
				}
			}
			else if (currentPipe == 'J')
			{
				if (comeFrom == Direction.TOP)
				{
					j--;
					comeFrom = Direction.RIGHT;
				}
				else if (comeFrom == Direction.LEFT)
				{
					i--;
					comeFrom = Direction.DOWN;
				}
			}
			else if (currentPipe == '7')
			{
				if (comeFrom == Direction.LEFT)
				{
					i++;
					comeFrom = Direction.TOP;
				}
				else if (comeFrom == Direction.DOWN)
				{
					j--;
					comeFrom = Direction.RIGHT;
				}
			}
			else if (currentPipe == 'F')
			{
				if (comeFrom == Direction.RIGHT)
				{
					i++;
					comeFrom = Direction.TOP;
				}
				else if (comeFrom == Direction.DOWN)
				{
					j++;
					comeFrom = Direction.LEFT;
				}
			}
			distance++;
		} while (i != iStart || j != jStart);

		// possible s value
		// start/end top down left right
		// top		 'x' '|'   'J' 'L' 
		// down      '|' 'x'   '7' 'F'
		// left      'J' '7'   'x' '-'
		// right     'L' 'F'   '-' 'x'
		char[,] possibleS = new char[,]{
			{'x', '|', 'J', 'L',},
			{'|', 'x', '7', 'F',},
			{'J', '7', 'x', '-',},
			{'L', 'F', '-', 'x',},
		};
		var sRealValue = possibleS[(int)startDirection, (int)comeFrom];


		// do a raycasting??
		// horizontally
		int verticesInsideLoop = 0;
		for (i = 0; i < rowSize; i++)
		{
			int wallCrossing = 0;
			char previousPipe = ' ';
			for (j = 0; j < colSize; j++)
			{
				if (partsOfLoops.Contains((i, j)))
				{
					// valid wall is just '|', 'L7', 'FJ'
					char pipe = inputs[i][j];
					if (pipe == 'S')
					{
						pipe = sRealValue;
					}

					if (pipe == '|')
					{
						wallCrossing++;
					}
					else if (pipe == 'L' || pipe == 'F')
					{
						previousPipe = pipe;
					}
					else if (pipe == '7')
					{
						if (previousPipe == 'L')
						{
							wallCrossing++;
						}
						previousPipe = ' ';
					}
					else if (pipe == 'J')
					{
						if (previousPipe == 'F')
						{
							wallCrossing++;
						}
						previousPipe = ' ';
					}
				}
				else if (wallCrossing % 2 != 0)
				{
					verticesInsideLoop++;
				}
			}
		}

		return verticesInsideLoop;
	}


}
