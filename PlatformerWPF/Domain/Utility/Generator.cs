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
        private int generateCount = 20;
        private int cellSize = 70;
        private Cell Cell;

        public Generator()
        {
            Cell = new Cell(0, 0);
            GameObjectType = ObjectType.Manager;
        }

        public override void Start()
        {
            var random = new Random();
            var map = new Cell[13, 13];
            for (var i = 0; i < generateCount; i++)
            {
                var x = random.Next(1, 10);
                var y = random.Next(1, 10);
                map[x, y] = new Cell(Cell.X + x, Cell.Y + y);
                Core.Instantiate(new Box(), new Cell(Cell.X + x, Cell.Y + y));
            }

            Grid.SetMap(map);
        }
    }
}
