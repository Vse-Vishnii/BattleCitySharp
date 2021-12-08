using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public static class Runner
    {
        public static List<GameObject> objects { get; } = new List<GameObject>();
        public static void RunObjects()
        {
            objects.Select(o => { o.Update(); return o; })
                .Select(o => { o.Collider.CheckCollision(objects.ToArray()); return o; })
                .Select(o => { o.LateUpdate(); return o; });
            //foreach (var obj in objects)
            //    obj.Update();
        }

        public static void Start()
        {

        }
    }
}
