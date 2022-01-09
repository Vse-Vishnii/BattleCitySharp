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
        
        public static void RunObjects()
        {
            var objects = ObjectContainer.Objects;
            objects.ToList().ForEach(o => o.Collider.CheckCollision(objects.ToArray()));
            objects.ToList().ForEach(o => o.Update());
            Array.ForEach(Inputs, i => i.ResetKeys());          
        }

        public static void Start()
        {
            for (var i = 0; i < Inputs.Length; i++)
                Inputs[i] = new Input();
            Core.Instantiate(new Player(Inputs[0]), Grid.Instance[0, 0]);
            Core.Instantiate(new Base(), Grid.Instance[3, 3]);
            Core.Instantiate(new Generator(), Grid.Instance.EmptyCell);
            Core.Instantiate(new EnemySpawner(), Grid.Instance.EmptyCell);
            Core.Instantiate(new UIController(), Grid.Instance.EmptyCell);
        }
    }
}
