using System.Text;

namespace AOC.Y2023;

class Day4(string inputName = "input.txt") : BaseDay(2023, 4, inputName)
{
    public override double Solve1()
    {
        int sum = 0;
        foreach (var input in inputs)
        {
            HashSet<int> winningNum = [];
            StringBuilder sb = new();
            int points = 1;

            for (int i = input.IndexOf(':') + 2; i < input.IndexOf('|'); i++)
            {
                if (input[i] == ' ')
                {
                    if (int.TryParse(sb.ToString(), out int num))
                    {
                        winningNum.Add(num);
                        sb.Clear();
                    }
                }
                else
                {
                    sb.Append(input[i]);
                }
            }

            for (int i = input.IndexOf('|') + 2; i < input.Length; i++)
            {
                sb.Append(input[i]);

                if (input[i] == ' ' || i == input.Length - 1)
                {
                    if (int.TryParse(sb.ToString(), out int num))
                    {
                        if (winningNum.Contains(num))
                        {
                            points <<= 1;
                        }
                    }
                    sb.Clear();
                }
            }

            sum += points >> 1;
        }
        return sum;
    }

    public override double Solve2()
    {
        int sum = 0;
        int[] copiesOfCards = new int[inputs.Length];
        Array.Fill(copiesOfCards, 1);

        for (int cardNumber = 0; cardNumber < copiesOfCards.Length; cardNumber++)
        {
            var input = inputs[cardNumber];
            int winningCopy = 0;
            HashSet<int> winningNum = [];
            StringBuilder sb = new();

            for (int i = input.IndexOf(':') + 2; i < input.IndexOf('|'); i++)
            {
                if (input[i] == ' ')
                {
                    if (int.TryParse(sb.ToString(), out int num))
                    {
                        winningNum.Add(num);
                        sb.Clear();
                    }
                }
                else
                {
                    sb.Append(input[i]);
                }
            }

            for (int i = input.IndexOf('|') + 2; i < input.Length; i++)
            {
                sb.Append(input[i]);

                if (input[i] == ' ' || i == input.Length - 1)
                {
                    if (int.TryParse(sb.ToString(), out int num))
                    {
                        if (winningNum.Contains(num))
                        {
                            winningCopy++;
                        }
                    }
                    sb.Clear();
                }
            }

            sum += copiesOfCards[cardNumber];
            for (int i = cardNumber + 1; i <= cardNumber + winningCopy && i < copiesOfCards.Length; i++)
            {
                copiesOfCards[i] += copiesOfCards[cardNumber];
            }
        }

        return sum;
    }

}