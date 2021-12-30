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
            for (var i = 0; i < generateCount; i++)
            {
                var x = random.Next(0, 13);
                var y = random.Next(0, 13);
                Core.Instantiate(new Box(), Grid.Instance[x, y]);
            }
        }
    }
}
