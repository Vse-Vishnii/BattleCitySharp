using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class Generator : GameObject
    {
        private int generateCount = 50;

        public Generator()
        {
            GameObjectType = ObjectType.Manager;
        }

        public override void Start()
        {
            var random = new Random();
            var map = new Cell[13, 13];
            for (var i = 0; i < generateCount; i++)
            {
                var x = random.Next(0, 13);
                var y = random.Next(0, 13);
                map[x, y] = new Cell(x, y);
                Core.Instantiate(new Box(), new Cell(x, y));
            }

            Grid.SetMap(map);
        }
    }
}
