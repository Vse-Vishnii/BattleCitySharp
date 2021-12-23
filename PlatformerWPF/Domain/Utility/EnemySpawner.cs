using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class EnemySpawner : GameObject
    {
        private int generateCount = 2;
        private int cellSize = 70;
        private Cell Cell;

        public EnemySpawner()
        {
            //Cell cell = new Cell(-1, -1);
            GameObjectType = ObjectType.Manager;
        }

        public override void Start()
        {
            for (var i = 0; i < generateCount; i++)
            {
                var random = new Random();
                var x = random.Next(1, 10);
                var y = random.Next(1, 10);

                while (Grid.Map[x, y].X > 0 && Grid.Map[x, y].Y > 0)
                {
                    x = random.Next(1, 10);
                    y = random.Next(1, 10);
                }
                Core.Instantiate(new Enemy(), new Cell(Cell.X + x, Cell.Y + y), Direction.Up);
            }
        }

        // private void CheckSpawnPosition(int x, int y)
        // {
        //     while(x == e.X && y == e.Y)
        // }
    }
}
