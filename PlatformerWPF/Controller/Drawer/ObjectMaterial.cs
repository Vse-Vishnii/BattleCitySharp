using System.Windows.Controls;

namespace BattleCitySharp.Controller
{
    public class ObjectMaterial
    {
        public Image Graphic { get; private set; }

        public ObjectMaterial(Image image)
        {
            Graphic = image;
        }
    }
}
