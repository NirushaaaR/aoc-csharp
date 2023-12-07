namespace AOC.Y2023;

class Day6(string inputName = "input.txt") : BaseDay(2023, 6, inputName)
{
    public override double Solve1()
    {
        double product = 1;

        var times = inputs[0][(inputs[0].IndexOf(':') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var distances = inputs[1][(inputs[1].IndexOf(':') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < times.Length; i++)
        {
            var time = int.Parse(times[i]);
            var distance = int.Parse(distances[i]);

            var lowerBound = (time - Math.Sqrt(time * time - 4 * distance)) / 2;
            var upperBound = (time + Math.Sqrt(time * time - 4 * distance)) / 2;
            var range = Math.Ceiling(upperBound) - Math.Floor(lowerBound) - 1;
            product *= range;
        }

        return product;
    }

    public override double Solve2()
    {
        double product;

        var time = double.Parse(string.Join(string.Empty, inputs[0][(inputs[0].IndexOf(':') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries)));
        var distance = double.Parse(string.Join(string.Empty, inputs[1][(inputs[1].IndexOf(':') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries)));

        var lowerBound = (time - Math.Sqrt(time * time - 4 * distance)) / 2;
        var upperBound = (time + Math.Sqrt(time * time - 4 * distance)) / 2;
        product = Math.Ceiling(upperBound) - Math.Floor(lowerBound) - 1;

        return product;
    }

}