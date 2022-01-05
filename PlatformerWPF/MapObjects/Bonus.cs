using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public abstract class Bonus : GameObject
    {
        public Bonus()
        {
            GameObjectType = ObjectType.Bonus;
        }

        public override void Start()
        {
            Collider.IsTrigger = true;
        }
    }
}
