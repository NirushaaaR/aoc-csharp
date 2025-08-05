namespace AOC;

class BaseDay(int year, int day, string inputName = "input.txt")
{
    public int year = year;
    public int day = day;

    public string[] inputs = File.ReadAllLines(Path.Combine(string.Format("{0}/Day{1}", year, day), inputName));

    public virtual double Solve1()
    {
        double sum = 0;
        foreach (var input in inputs)
        {
            Console.WriteLine(input);
        }
        return sum;
    }

    public virtual double Solve2()
    {
        double sum = 0;
        foreach (var input in inputs)
        {
            Console.WriteLine(input);
        }
        return sum;
    }
}