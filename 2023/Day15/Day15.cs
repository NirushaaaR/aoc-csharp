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

	static void PrintBox(Dictionary<int, List<string>> boxes)
	{
		foreach (var (key, value) in boxes)
		{
			Console.WriteLine($"{key}: {string.Join(',', value)}");
		}
		Console.WriteLine();
	}

	static int CalculateFocusingPower(Dictionary<int, List<string>> boxes, Dictionary<string, int> lensFocalLength)
	{
		int power = 0;
		foreach (var (boxIndex, box) in boxes)
		{
			for (int slot = 0; slot < box.Count; slot++)
			{
				string len = box[slot];
				int focalLength = lensFocalLength[len];
				power += (1 + boxIndex) * (slot + 1) * focalLength;
			}
		}
		return power;
	}

	public override double Solve2()
	{
		Dictionary<int, List<string>> boxes = [];
		Dictionary<string, int> lensFocalLength = [];

		foreach (var input in inputs[0].Split(","))
		{
			int signIndex = input.IndexOf('=');
			if (signIndex != -1)
			{
				// = opertaion
				int focalLength = Convert.ToInt32(input[(signIndex + 1)..]);
				string label = input[..signIndex];
				int boxIndex = Hash(label);

				if (!lensFocalLength.ContainsKey(label))
				{
					// add new one to the boxes
					if (!boxes.TryGetValue(boxIndex, out List<string> box))
					{
						box = [];
						boxes[boxIndex] = box;
					}
					box.Add(label);
				}

				lensFocalLength[label] = focalLength;
			}
			else
			{
				// - operation
				string label = input[..^1];
				if (lensFocalLength.Remove(label))
				{
					// if exists, remove the box
					int boxIndex = Hash(label);
					boxes[boxIndex].Remove(label);
				}
			}
		}
		return CalculateFocusingPower(boxes, lensFocalLength);
	}
}