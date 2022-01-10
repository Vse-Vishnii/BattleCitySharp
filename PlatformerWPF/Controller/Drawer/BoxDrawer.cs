using System;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public abstract class BoxDrawer : Drawer
    {
        private static Uri[] wallTextures = new Uri[]
        {
            new Uri("pack://application:,,,/images/brick.png"),
            new Uri("pack://application:,,,/images/steel.png"),
            new Uri("pack://application:,,,/images/bush.png"),
            new Uri("pack://application:,,,/images/water.png")
        };

        public static void ChangeBoxMaterial(Box original, int stateNumber)
        {
            var image = gameObjectMaterials[original].Graphic;
            image.Source = BitmapFrame.Create(wallTextures[stateNumber]);
            if (stateNumber >= 2)
            {
                var z = stateNumber == 2 ? 2 : 0;
                SetPriority(image, z);
            }
        }
    }
}
