using System.Text;

namespace AOC.Y2023;

class Day2(string inputName = "input.txt") : BaseDay(2023, 2, inputName)
{
    public override double Solve1()
    {
        int sum = 0;
        Dictionary<string, int> cubesMaxNum = new(){
            {"red", 12},
            {"green", 13},
            {"blue", 14},
        };

        foreach (string input in inputs)
        {
            int gameId = -1;
            bool isPossible = true;
            int cubeNumber = -1;
            StringBuilder sb = new();

            for (int i = 5; i < input.Length; i++)
            {
                if (input[i] == ':')
                {
                    gameId = int.Parse(sb.ToString());
                    sb.Clear();
                    i++;
                }
                else if (input[i] == ' ')
                {
                    cubeNumber = int.Parse(sb.ToString());
                    sb.Clear();
                }
                else if (input[i] == ',' || input[i] == ';' || i == input.Length - 1)
                {
                    if (i == input.Length - 1)
                    {
                        sb.Append(input[i]);
                    }

                    if (cubeNumber > cubesMaxNum[sb.ToString()])
                    {
                        isPossible = false;
                    }

                    // Reset
                    sb.Clear();
                    cubeNumber = -1;
                    i++;

                    if (!isPossible)
                    {
                        break;
                    }
                }
                else
                {
                    sb.Append(input[i]);
                }
            }


            if (isPossible)
            {
                sum += gameId;
            }
        }

        return sum;
    }

    public override double Solve2()
    {
        int sum = 0;
        foreach (string input in inputs)
        {
            int cubeNumber = -1;
            StringBuilder sb = new();
            Dictionary<string, int> fewestCubeNumber = new(){
                {"red", 0},
                {"green", 0},
                {"blue", 0},
            };

            for (int i = 5; i < input.Length; i++)
            {
                if (input[i] == ':')
                {
                    sb.Clear();
                    i++;
                }
                else if (input[i] == ' ')
                {
                    cubeNumber = int.Parse(sb.ToString());
                    sb.Clear();
                }
                else if (input[i] == ',' || input[i] == ';' || i == input.Length - 1)
                {
                    if (i == input.Length - 1)
                    {
                        sb.Append(input[i]);
                    }

                    var cubeColor = sb.ToString();
                    if (fewestCubeNumber[cubeColor] < cubeNumber)
                    {
                        fewestCubeNumber[cubeColor] = cubeNumber;
                    }


                    // Reset
                    sb.Clear();
                    cubeNumber = -1;
                    i++;
                }
                else
                {
                    sb.Append(input[i]);
                }
            }



            sum += fewestCubeNumber["red"] * fewestCubeNumber["green"] * fewestCubeNumber["blue"];
        }

        return sum;
    }

}