using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public abstract class EnemyDrawer : Drawer
    {
        private static Uri[] enemyTextures = new Uri[]
        {
            new Uri("pack://application:,,,/images/enemy1.png"),
            new Uri("pack://application:,,,/images/enemy2.png")
        };

        public static void ChangeEnemyMaterial(Enemy original, int enemyNumber)
        {
            var image = gameObjectMaterials[original].Graphic;
            image.Source = BitmapFrame.Create(enemyTextures[enemyNumber]);
        }
    }
}
