using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public class Box : GameObject
    {
        public Health Health { get; private set; }
        
        private int stateNumber = 0;
        private int stateCount = 4;

        public Box()
        {
            GameObjectType = ObjectType.Wall;
        }

        internal void StayBrick()
        {
            stateNumber = 0;
            Drawer.ChangeBoxMaterial(this, stateNumber);
        }

        public override void Start()
        {
            var random = new Random();
            stateNumber = random.Next(0, stateCount);
            Drawer.ChangeBoxMaterial(this, stateNumber);
            CheckCubes();
        }       

        private void CheckCubes()
        {
            if(Collider.OverlapSquare().Contains(true))
                Core.Destroy(this);              
        }

        public override void ColliderEnter(Collider collider)
        {
            if (stateNumber == 3)
                WaterState(collider, p => p.SlowDown());
        }

        public override void ColliderExit(Collider collider)
        {
            if (stateNumber == 3)
                WaterState(collider, p => p.SpeedUp());
        }

       private void WaterState(Collider collider, Action<Tank> action)
        {
            var gameObject = collider.GameObject;
            if (gameObject is Tank)
            {
                var player = gameObject as Tank;
                action(player);
            }
        }
    }
}
