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
        public static GameObject Instantiate(GameObject original, Cell cell, Direction rotation)
        {
            var image = Drawer.DrawObject(cell, original.GameObjectType);
            original.CreateGameObjectProperties(cell, rotation, image);
            Runner.objects.Add(original);
            original.Collider.Collisions.AddRange(Runner.objects[0].Collider.Collisions);
            Runner.objects.ForEach(o => o.Collider.Collisions.Add(false));
            return original;
        }
    }
}
