namespace AOC.Y2023;

class Day8(string inputName = "input.txt") : BaseDay(2023, 8, inputName)
{
    public override double Solve1()
    {
        string leftRightInstructions = inputs[0];
        string currentPosition = "AAA";
        Dictionary<string, string[]> networks =
        (from input in inputs[2..]
         select input.Split(" = ")).ToDictionary(
            (t) => t[0], (t) => t[1][(t[1].IndexOf('(') + 1)..t[1].IndexOf(')')].Split(", ")
        );

        bool finish = false;
        int count = 0;
        while (!finish)
        {
            foreach (char instruction in leftRightInstructions)
            {
                count++;
                int index = instruction == 'L' ? 0 : 1;
                currentPosition = networks[currentPosition][index];
                if (currentPosition == "ZZZ")
                {
                    finish = true;
                    break;
                }
            }
        }
        return count;
    }

    public override double Solve2()
    {
        string leftRightInstructions = inputs[0];
        List<string> currentPositions = [];
        Dictionary<string, string[]> networks =
        (from input in inputs[2..]
         select input.Split(" = ")).ToDictionary(
            (t) =>
            {
                if (t[0].EndsWith('A'))
                {
                    currentPositions.Add(t[0]);
                }
                return t[0];
            }, (t) => t[1][(t[1].IndexOf('(') + 1)..t[1].IndexOf(')')].Split(", ")
        );

        double count = 0;
        bool finish = false;
        List<double> stepsTakeToFinish = [];
        while (!finish)
        {
            finish = true;
            foreach (char instruction in leftRightInstructions)
            {
                count++;
                int direction = instruction == 'L' ? 0 : 1;
                for (int i = 0; i < currentPositions.Count; i++)
                {
                    if (!currentPositions[i].EndsWith('Z'))
                    {
                        currentPositions[i] = networks[currentPositions[i]][direction];
                        finish = false;
                        if (currentPositions[i].EndsWith('Z'))
                        {
                            stepsTakeToFinish.Add(count);
                        }
                    }

                }
            }
        }
        return stepsTakeToFinish.Aggregate(LCM);
    }

    private static double LCM(double a, double b)
    {
        return a * b / GCD(a, b);
    }

    private static double GCD(double a, double b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

}
