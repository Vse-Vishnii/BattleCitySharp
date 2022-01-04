using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
