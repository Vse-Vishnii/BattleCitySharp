using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public static class Runner
    {
        public static List<MonoBehavior> objects { get; } = new List<MonoBehavior>();
        public static void RunObjects()
        {
            objects.Select(o => { o.Update(); return o; })
                .Select(o => { o.LateUpdate(); return o; });
            //foreach (var obj in objects)
            //    obj.Update();
        }
    }
}
