using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public static class Runner
    {
        public static Input[] Inputs { get; private set; } = new Input[2];
        public static List<GameObject> objects { get; } = new List<GameObject>();
        public static void RunObjects()
        {
            objects.ToList().ForEach(o => o.Collider.CheckCollision(objects.ToArray()));
            objects.ToList().ForEach(o => o.Update());
            objects.ForEach(o => o.LateUpdate());
            Array.ForEach(Inputs, i => i.ResetKeys());          
        }

        public static void Start()
        {
            for (var i = 0; i < Inputs.Length; i++)
                Inputs[i] = new Input();
            Core.Instantiate(new Player(Inputs[0]), new Cell(0, 0));
            Core.Instantiate(new Base(), new Cell(3, 3));
            Core.Instantiate(new Generator(), new Cell(-1, -1));
            Core.Instantiate(new EnemySpawner(), new Cell(-1, -1));
        }
    }
}
