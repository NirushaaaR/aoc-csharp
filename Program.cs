using AOC;
using System.Diagnostics;

int argsLength = args.Length;
Debug.Assert(argsLength == 2 || argsLength == 3, "plase run: dotnet run [YYYY] [D] [inputFileName (optional default is input.txt)]");
string year = args[0];
string day = args[1];
string className = $"AOC.Y{year}.Day{day}";

Type? type = Type.GetType(className);
Debug.Assert(type != null, $"className {className} is not valid!");

if (argsLength == 2)
{
        Solver.Solve((BaseDay)Activator.CreateInstance(type, ["input.txt"])!);
}
else
{
        Solver.Solve((BaseDay)Activator.CreateInstance(type, [args[2]])!);
}