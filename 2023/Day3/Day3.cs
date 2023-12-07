using System.Text;

namespace AOC.Y2023;

class Day3(string inputName = "input.txt") : BaseDay(2023, 3, inputName)
{
    public override double Solve1()
    {
        int sum = 0;
        StringBuilder sb = new();

        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = 0; j < inputs[i].Length; j++)
            {
                var currentChar = inputs[i][j];

                if (char.IsDigit(currentChar))
                {
                    sb.Append(currentChar);
                }

                if ((!char.IsDigit(currentChar) || j == inputs[i].Length - 1) && sb.Length > 0)
                {
                    int num = int.Parse(sb.ToString());
                    // check neighbor
                    for (int k = j; k >= j - sb.Length - 1 && k >= 0; k--)
                    {
                        bool selfIsSymbol = !char.IsDigit(inputs[i][k]) && inputs[i][k] != '.';
                        bool upperIsSymbol = i - 1 >= 0 && !char.IsDigit(inputs[i - 1][k]) && inputs[i - 1][k] != '.';
                        bool lowerIsSymbol = i + 1 < inputs.Length && !char.IsDigit(inputs[i + 1][k]) && inputs[i + 1][k] != '.';
                        if (selfIsSymbol || upperIsSymbol || lowerIsSymbol)
                        {
                            sum += num;
                            break;
                        }
                    }
                    sb.Clear();

                }

            }

        }
        return sum;
    }

    public override double Solve2()
    {
        int sum = 0;
        StringBuilder sb = new();
        Dictionary<string, int[]> memGearParts = [];

        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = 0; j < inputs[i].Length; j++)
            {
                var currentChar = inputs[i][j];

                if (char.IsDigit(currentChar))
                {
                    sb.Append(currentChar);
                }

                if ((!char.IsDigit(currentChar) || j == inputs[i].Length - 1) && sb.Length > 0)
                {
                    int num = int.Parse(sb.ToString());
                    // check neighbor
                    for (int k = j; k >= j - sb.Length - 1 && k >= 0; k--)
                    {
                        string? key = null;
                        if (inputs[i][k] == '*')
                        {
                            key = string.Format("{0},{1}", i, k);
                        }
                        else if (i - 1 >= 0 && inputs[i - 1][k] == '*')
                        {
                            key = string.Format("{0},{1}", i - 1, k);
                        }
                        else if (i + 1 < inputs.Length && inputs[i + 1][k] == '*')
                        {
                            key = string.Format("{0},{1}", i + 1, k);
                        }

                        if (key != null)
                        {
                            if (memGearParts.TryGetValue(key, out int[]? parts))
                            {
                                if (parts[1] == 0)
                                {
                                    parts[1] = num;
                                }
                                else
                                {
                                    // has more then 2 parts
                                    memGearParts.Remove(key);
                                }
                            }
                            else
                            {
                                memGearParts.Add(key, [num, 0]);
                            }
                            break;
                        }
                    }
                    sb.Clear();
                }
            }
        }

        foreach (var parts in memGearParts.Values)
        {
            sum += parts[0] * parts[1];
        }

        return sum;
    }

}