using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BattleCitySharp
{
    public class Drawer
    {
        private Uri[] wallType = new Uri[]
        {
            new Uri("pack://application:,,,/images/wall1.png"),
            new Uri("pack://application:,,,/images/wall2.png"),
            new Uri("pack://application:,,,/images/wall3.png"),
            new Uri("pack://application:,,,/images/wall4.png")
        };

        private static Image DrawObject() { throw new NotImplementedException(); }

        private static void RotateObject(Image tank1) { }
    }
}
