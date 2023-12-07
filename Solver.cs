using System.Diagnostics;

class Solver
{
    public static void Solve(BaseDay day)
    {
        Stopwatch stopWatch = new();
        stopWatch.Start();
        var answer = day.Solve1();
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        Console.Write(string.Format("Day{0} Part1: {1}, ", day.day, answer));
        Console.WriteLine("RunTime " + ts.TotalMilliseconds + "ms");

        stopWatch.Restart();
        answer = day.Solve2();
        stopWatch.Stop();
        ts = stopWatch.Elapsed;
        Console.Write(string.Format("Day{0} Part2: {1}, ", day.day, answer));
        Console.WriteLine("RunTime " + ts.TotalMilliseconds + "ms");
        Console.WriteLine("=====================================\n");
    }
}