namespace AOC.Y2023;

class Day9(string inputName = "input.txt") : BaseDay(2023, 9, inputName)
{
	public override double Solve1()
	{
		double sum = 0;
		foreach (string input in inputs)
		{
			List<int[]> sequences = [input.Split(' ').Select(int.Parse).ToArray()];
			while (true)
			{
				List<int> differences = [];
				bool allDiffIsZeros = true;
				var nums = sequences.Last();
				for (int i = 1; i < nums.Length; i++)
				{
					int diff = nums[i] - nums[i - 1];
					differences.Add(diff);
					if (diff != 0)
					{
						allDiffIsZeros = false;
					}
				}
				if (allDiffIsZeros) {
					break;
				}
				sequences.Add([.. differences]);
			}

			var sequencesArr = sequences.ToArray();

			int nextNum = 0;
			for (int i = sequencesArr.Length - 1; i >= 0; i--)
			{
				nextNum = sequencesArr[i][^1] + nextNum;
			}

			sum += nextNum;
		}
		return sum;
	}

	public override double Solve2()
	{
		double sum = 0;
		foreach (string input in inputs)
		{
			List<int[]> sequences = [input.Split(' ').Select(int.Parse).ToArray()];
			while (true)
			{
				List<int> differences = [];
				bool allDiffIsZeros = true;
				var nums = sequences.Last();
				for (int i = 1; i < nums.Length; i++)
				{
					int diff = nums[i] - nums[i - 1];
					differences.Add(diff);
					if (diff != 0)
					{
						allDiffIsZeros = false;
					}
				}
				if (allDiffIsZeros) {
					break;
				}
				sequences.Add([.. differences]);
			}

			var sequencesArr = sequences.ToArray();

			int nextNum = 0;
			for (int i = sequencesArr.Length - 1; i >= 0; i--)
			{
				nextNum = sequencesArr[i][0] - nextNum;
			}

			sum += nextNum;
		}
		return sum;
	}
}
