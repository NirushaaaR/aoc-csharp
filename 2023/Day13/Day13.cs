using System.Collections;
using System.Text;

namespace AOC.Y2023;
// 35335
class Day13(string inputName = "input.txt") : BaseDay(2023, 13, inputName)
{
	class ParsedInput(string[] inputs): IEnumerable<string[]>
	{
		public IEnumerator<string[]> GetEnumerator()
		{
			List<string> paths = [];
			for (int i = 0; i < inputs.Length; i++)
			{
				if (inputs[i] == "")
				{
					yield return paths.ToArray();
					paths.Clear();
				}
				else
				{
					paths.Add(inputs[i]);
				}
			}
			yield return paths.ToArray();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	static bool CheckVerticalCharEqual(string[] input, int i1, int i2)
	{
		for (int j = 0; j < input.Length; j++)
		{
			if (input[j][i1] != input[j][i2])
			{
				return false;
			}
		}
		return true;
	}

	static int CheckVerticalBitwise(string[] input, int i1, int i2)
	{
		int v1= 0,  v2 =0;
		for (int j = 0; j < input.Length; j++)
		{
			v1 <<= 1;
			if (input[j][i1] == '#')
			{
				++v1;
			}

			v2 <<= 1;
			if (input[j][i2] == '#')
			{
				++v2;
			}
		}

		return v1 ^ v2;	// if equal should be zero
	}

	static int CheckHorizontalBitwise(string[] input, int i1, int i2)
	{
		int v1= 0,  v2 =0;
		for (int i = 0; i < input[i1].Length; i++)
		{
			v1 <<= 1;
			if (input[i1][i] == '#')
			{
				++v1;
			}

			v2 <<= 1;
			if (input[i2][i] == '#')
			{
				++v2;
			}

		}
		return v1 ^ v2;	// if equal should be zero
	}

	static bool CheckIfSmudge(int n)
	{
		// check if bit only has 1 bit positon
		// n & (n-1) is 0
		// example: n = 8 (1000) = 1000 & (0111) == 0
		return (n & (n - 1)) == 0;
	}

	public override double Solve1()
	{
		List<int> verticalMirror = [];
		List<int> horizontalMirror = [];
		foreach (var input in new ParsedInput(inputs))
		{
			// check vertical
			int verticalMirrorIndex = -1;
			for (int i = 0; i < input[0].Length - 1; i++)
			{
				bool isMirror = true;
				for (int j = i, k = i + 1; j >= 0 && k < input[0].Length; j--, k++)
				{
					if (!CheckVerticalCharEqual(input, j, k))
					{
						// not really
						isMirror = false;
						break;
					}
				}
				if (isMirror)
				{
					verticalMirrorIndex = i;
					break;
				}
			}
			if (verticalMirrorIndex != -1)
			{
				verticalMirror.Add(verticalMirrorIndex + 1);
				continue;
			}

			// check horizontal
			int horizontalMirrorIndex = -1;
			for (int i = 0; i < input.Length - 1; i++)
			{
				bool isMirror = true;
				for (int j = i, k = i + 1; j >= 0 && k < input.Length; j--, k++)
				{
					if (input[j] != input[k])
					{
						// not really
						isMirror = false;
						break;
					}
				}
				if (isMirror)
				{
					horizontalMirrorIndex = i;
					break;
				}
			}
			if (horizontalMirrorIndex != -1)
			{
				horizontalMirror.Add(horizontalMirrorIndex + 1);
			}
		}
		return verticalMirror.Sum() + 100 * horizontalMirror.Sum();
	}

	public override double Solve2()
	{
		List<int> verticalMirror = [];
		List<int> horizontalMirror = [];
		foreach (var input in new ParsedInput(inputs))
		{
			// check vertical
			int verticalMirrorIndex = -1;
			for (int i = 0; i < input[0].Length - 1; i++)
			{
				bool hasSmudge = false;
				bool isMirror = true;
				for (int j = i, k = i + 1; j >= 0 && k < input[0].Length; j--, k++)
				{
					int n = CheckVerticalBitwise(input, j, k);
					if (n != 0)
					{
						if (!hasSmudge && CheckIfSmudge(n))
						{
							hasSmudge = true;
							continue;
						}
						isMirror = false;
						break;
					}
				}
				if (isMirror && hasSmudge)
				{
					verticalMirrorIndex = i;
					break;
				}
			}

			if (verticalMirrorIndex != -1)
			{
				verticalMirror.Add(verticalMirrorIndex + 1);
				continue;
			}

			// check horizontal
			int horizontalMirrorIndex = -1;
			for (int i = 0; i < input.Length - 1; i++)
			{
				bool hasSmudge = false;
				bool isMirror = true;
				for (int j = i, k = i + 1; j >= 0 && k < input.Length; j--, k++)
				{
					int n = CheckHorizontalBitwise(input, j, k);
					if (n != 0)
					{
						if (!hasSmudge && CheckIfSmudge(n))
						{
							hasSmudge = true;
							continue;
						}
						isMirror = false;
						break;
					}
				}
				if (isMirror && hasSmudge)
				{
					horizontalMirrorIndex = i;
					break;
				}
			}

			if (horizontalMirrorIndex != -1)
			{
				horizontalMirror.Add(horizontalMirrorIndex + 1);
			}
		}
		return verticalMirror.Sum() + 100 * horizontalMirror.Sum();
	}

}