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

        public override void Start()
        {
            var random = new Random();
            stateNumber = random.Next(0, textures.Length);
            ObjectGraphic.Source = BitmapFrame.Create(textures[stateNumber]);
            if (stateNumber == 3 || stateNumber == 2)
                Collider.IsTrigger = true;
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
