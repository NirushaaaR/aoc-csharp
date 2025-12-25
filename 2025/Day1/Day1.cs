namespace AOC.Y2025;

class Day1(string inputName = "input.txt") : BaseDay(2025, 1, inputName)
{
	public override double Solve1()
	{
		double count = 0;
		int dial = 50;

		foreach (var line in inputs)
		{
			// parse input
			int direction = line[0] switch
			{
				'L' => -1,
				'R' => 1,
				char invalid => throw new Exception("Unexpected input: " + invalid)
			};
			int distance = direction * int.Parse(line[1..]);

			dial += distance;

			// its just works...
			dial %= 100;
			if (dial == 0)
			{
				count += 1;
			}

		}
		return count;
	}

	public override double Solve2()
	{
		double count = 0;
		int dial = 50;

		foreach (var line in inputs)
		{
			// parse input
			int direction = line[0] switch
			{
				'L' => -1,
				'R' => 1,
				char invalid => throw new Exception("Unexpected input: " + invalid)
			};
			int distance = direction * int.Parse(line[1..]);

			
			int dial_long = dial + distance;

			count += Math.Abs(dial_long / 100);
			if (dial != 0 && dial_long <= 0)
			{
				count += 1;
			}

			// rem euclidian
			dial = dial_long % 100;
			if (dial < 0)
			{
				dial += 100;
			}
		}
		return count;
	}
}