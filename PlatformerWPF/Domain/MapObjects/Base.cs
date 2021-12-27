using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class Base : MovingObject
    {
        public Base()
        {
            GameObjectType = ObjectType.Base;
            TeamId = 1;
        }

        public override void Start()
        {
            var cell = Transform.Position / 70;
            var cellX = (int)cell.X;
            var cellY = (int)cell.Y;
            for (var x = cellX - 1; x <= cellX + 1; x++)
                for (var y = cellY - 1; y <= cellY + 1; y++)
                {
                    Core.Instantiate(new Box(), new Cell(x, y), Transform.Direction).To<Box>().StayBrick();
                }
        }
    }
}
