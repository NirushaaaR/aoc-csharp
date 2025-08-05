namespace AOC;

using System.Diagnostics;

class Solver
{
    public static void Solve(BaseDay day)
    {
        
        Stopwatch stopWatch = new();
        stopWatch.Start();
        double kbAtExecution = GC.GetTotalMemory(false) / 1024;
        var answer = day.Solve1();
        double kbAfter = GC.GetTotalMemory(false) / 1024;
        stopWatch.Stop();

        TimeSpan ts = stopWatch.Elapsed;
        Console.Write($"Day{day.day} Part1: {answer}, ");
        Console.WriteLine($"RunTime {ts.TotalMilliseconds} ms, MemoryUsage {kbAfter - kbAtExecution:F2} kB;");

        stopWatch.Restart();
        kbAtExecution = GC.GetTotalMemory(false) / 1024;
        answer = day.Solve2();
        kbAfter = GC.GetTotalMemory(false) / 1024;
        stopWatch.Stop();

        ts = stopWatch.Elapsed;
        Console.Write($"Day{day.day} Part2: {answer}, ");
        Console.WriteLine($"RunTime {ts.TotalMilliseconds} ms, MemoryUsage {kbAfter - kbAtExecution:F2} kB;");
    }
}