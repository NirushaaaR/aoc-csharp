using System.Text;

namespace AOC.Y2023;

class Day1(string inputName = "input.txt") : BaseDay(2023, 1, inputName)
{
    private readonly Dictionary<string, int> mapValue = new(){
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        {"0", 0},
    };

    private readonly Dictionary<string, int> mapDigit = new(){
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
    };

    public override double Solve1()
    {
        int sum = 0;

        foreach (string input in inputs)
        {

            int first = -1;
            int last = 0;

            foreach (char c in input)
            {
                if (mapValue.TryGetValue(c.ToString(), out int num))
                {
                    if (first == -1)
                    {
                        first = num;
                    }
                    last = num;
                }
            }

            sum += (first * 10) + last;
        }
        return sum;
    }

    public override double Solve2()
    {
        int sum = 0;

        foreach (string input in inputs)
        {
            int first = -1;
            int last = 0;

            for (int i = 0; i < input.Length; i++)
            {
                bool isExists = false;
                string currentString = input[i].ToString();
                isExists = mapValue.TryGetValue(currentString, out int num);

                if (!isExists)
                {
                    foreach (var digit in mapDigit.Keys)
                    {
                        if (digit.StartsWith(currentString) && i + digit.Length - 1 < input.Length)
                        {
                            StringBuilder sbDigit = new(currentString);
                            for (int j = 1; j < digit.Length; j++)
                            {
                                sbDigit.Append(input[i + j]);
                            }

                            if (mapDigit.TryGetValue(sbDigit.ToString(), out num))
                            {
                                isExists = true;
                                break;
                            }
                        }
                    }
                }

                if (isExists)
                {
                    if (first == -1)
                    {
                        first = num;
                    }
                    last = num;
                }
            }
            sum += (first * 10) + last;
        }
        return sum;
    }
}