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

        public Box()
        {
            GameObjectType = ObjectType.Wall;
        }

        public override void Start()
        {
            var random = new Random();
            var stateNumber = random.Next(0, textures.Length);
            ObjectGraphic.Source = BitmapFrame.Create(textures[stateNumber]);
            if (stateNumber == 3)
                Collider.IsTrigger = true;
        }

        public override void ColliderEnter(Collider collider)
        {
            var gameObject = collider.GameObject;
            if (gameObject is Player)
            {
                var player = gameObject as Player;
                player.SlowDown();
            }
        }
    }
}
