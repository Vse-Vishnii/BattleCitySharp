using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class Bullet : MovingObject
    {
        private float speed = 10f;

        public Bullet()
        {
            IsTrigger = true;
            GameObjectType = ObjectType.Bullet;
        }

        public override void Update()
        {
            Move(Transform.MoveDirection, speed, Transform.Size / 2);
        }
    }
}
