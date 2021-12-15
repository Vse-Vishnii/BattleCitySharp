using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public static class Runner
    {
        public static Input[] Inputs { get; private set; } = new Input[2];
        public static List<GameObject> objects { get; } = new List<GameObject>();
        public static void RunObjects()
        {
            objects.ForEach(o => o.Update());
            objects.ForEach(o => o.Collider.CheckCollision(objects.ToArray()));
            objects.ForEach(o => o.LateUpdate());
            //foreach (var o in objects)
            //    o.Update();
            //foreach (var o in objects)
            //    o.Collider.CheckCollision(objects.ToArray());
            //foreach (var o in objects)
            //    o.LateUpdate();
            //objects.Select(o => { o.Update(); return o; })
            //    .Select(o => { o.Collider.CheckCollision(objects.ToArray()); return o; })
            //    .Select(o => { o.LateUpdate(); return o; });
            for (var i = 0; i < Inputs.Length; i++)
                Inputs[i].ResetKeys();            
        }

        public static void Start()
        {
            for (var i = 0; i < Inputs.Length; i++)
                Inputs[i] = new Input();
            Core.Instantiate(new Player(Inputs[0]), new Cell(0, 0), Direction.Up);
        }
    }
}
