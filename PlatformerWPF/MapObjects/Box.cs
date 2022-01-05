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
        private bool checkedBox = true;

        public Box()
        {
            GameObjectType = ObjectType.Wall;
            Health = new Health(100, this);
        }

        internal void StayBrick()
        {
            if (!checkedBox)
                return;
            stateNumber = 0;
            ChangeBoxMaterial();
        }

        public override void Start()
        {
            var random = new Random();
            stateNumber = random.Next(0, stateCount);
            ChangeBoxMaterial();
            CheckCubes();
        }       

        private void CheckCubes()
        {
            if (Collider.OverlapSquare().Contains(true))
            {
                checkedBox = false;
                Core.Destroy(this);
            }                            
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

        private void ChangeBoxMaterial()
        {
            BoxDrawer.ChangeBoxMaterial(this, stateNumber);
            Collider.IsTrigger = false;
            if (stateNumber >= 2)
            {
                Collider.IsTrigger = true;
            }
            else if (stateNumber == 0)
                Health = new Health(1, this);
            else
                Health = new Health(100, this);
        }
    }
}
