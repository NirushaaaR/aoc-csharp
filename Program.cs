using System.Reflection;

const string namespaceName = "AOC.Y2023";

var q = from t in Assembly.GetExecutingAssembly().GetTypes()
        where t.IsClass && t.Namespace == namespaceName && t.BaseType?.Name == "BaseDay"
        select t;

var BaseDayList = q.ToList();

BaseDayList.Sort((x, y) => -x.Name.CompareTo(y.Name));

// foreach (var type in BaseDayList)
// {
//     Solver.Solve((BaseDay)Activator.CreateInstance(type, ["input.txt"])!);
// }

Solver.Solve((BaseDay)Activator.CreateInstance(BaseDayList[0], ["input.txt"])!);

