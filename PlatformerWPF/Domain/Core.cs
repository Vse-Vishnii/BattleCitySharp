using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCitySharp
{
    public static class Core
    {
        public static object Instantiate(GameObject original, Cell cell, Direction rotation)
        {
            original.CreateGameObjectProperties(cell, rotation);
            Runner.objects.Add(original);
            Runner.objects.Select(o => { o.Collider.Collisions.Add(false); return o; });
            Drawer.DrawObject();
            return original;
        }
    }
}
