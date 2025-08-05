namespace AOC;

using System.Diagnostics;

class Solver
{
    public static void Solve(BaseDay day)
    {
        
        Stopwatch stopWatch = new();
        stopWatch.Start();
        long kbAtExecution = GC.GetTotalMemory(false);
        var answer = day.Solve1();
        long kbAfter = GC.GetTotalMemory(false);
        stopWatch.Stop();

        TimeSpan ts = stopWatch.Elapsed;
        Console.Write($"Day{day.day} Part1: {answer}, ");
        Console.WriteLine($"RunTime {ts.TotalMilliseconds} ms, MemoryUsage {(kbAfter - kbAtExecution)/1024.0:F2} kB;");

        stopWatch.Restart();
        kbAtExecution = GC.GetTotalMemory(false);
        answer = day.Solve2();
        kbAfter = GC.GetTotalMemory(false);
        stopWatch.Stop();

        ts = stopWatch.Elapsed;
        Console.Write($"Day{day.day} Part2: {answer}, ");
        Console.WriteLine($"RunTime {ts.TotalMilliseconds} ms, MemoryUsage {(kbAfter - kbAtExecution)/1024.0:F2} kB;");
    }
}