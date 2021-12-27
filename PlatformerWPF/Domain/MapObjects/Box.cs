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

        private Uri[] textures = new Uri[]
        {
            new Uri("pack://application:,,,/images/brick.png"),
            new Uri("pack://application:,,,/images/steel.png"),
            new Uri("pack://application:,,,/images/bush.png"),
            new Uri("pack://application:,,,/images/water.png")
        };
        private int stateNumber = 0;

        public Box()
        {
            GameObjectType = ObjectType.Wall;
        }

        internal void StayBrick()
        {
            stateNumber = 0;
            ChangeBoxMaterial();
        }

        public override void Start()
        {
            var random = new Random();
            stateNumber = random.Next(0, textures.Length);
            ChangeBoxMaterial();
            CheckCubes();
        }

        private void ChangeBoxMaterial()
        {
            ObjectGraphic.Source = BitmapFrame.Create(textures[stateNumber]);
            if (stateNumber >= 2)
            {
                Collider.IsTrigger = true;
                var z = stateNumber == 2 ? 2 : 0;
                Drawer.SetPriority(ObjectGraphic, z);
            }
            else if (stateNumber == 0)
                Health = new Health(1, this);
            else
                Health = new Health(100, this);
        }

        private void CheckCubes()
        {
            if(Collider.OverlapSquare().Contains(true))
                Core.Destroy(this);              
        }

        public override void ColliderEnter(Collider collider)
        {
            ProcessCollider(collider, p => p.SlowDown());
        }

        public override void ColliderExit(Collider collider)
        {
            ProcessCollider(collider, p => p.SpeedUp());
        }

       private void ProcessCollider(Collider collider, Action<Player> action)
        {
            var gameObject = collider.GameObject;
            if (gameObject is Player)
            {
                var player = gameObject as Player;
                if (stateNumber == 3)
                    action(player);
            }
        }
    }
}
