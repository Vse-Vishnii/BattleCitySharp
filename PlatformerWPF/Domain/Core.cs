using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCitySharp
{
    public static class Core
    {
        public static T Instantiate<T>(T original, Cell cell, Direction rotation = Direction.Up)
            where T : GameObject
        {
            var size = 70;
            var point = new Vector2(cell.X * size, cell.Y * size);
            return Instantiate(original, point, rotation, size);
        }

        public static T Instantiate<T>(T original, Vector2 point, Direction rotation = Direction.Up, int size = 70)
            where T : GameObject
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var image = Drawer.DrawObject(point, original.GameObjectType, size);
                original.CreateGameObjectProperties(point, rotation, image, size);
                Runner.objects.Add(original);
                original.Collider.Collisions.AddRange(Runner.objects[0].Collider.Collisions);
                original.Collider.Triggers.AddRange(Runner.objects[0].Collider.Triggers);
                Runner.objects.ForEach(o => o.Collider.Collisions.Add(false));
                Runner.objects.ForEach(o => o.Collider.Triggers.Add(false));
                original.Start();
            });
            return original;
        }

        public static void Destroy(GameObject original)
        {
            Drawer.DeleteObject(original.ObjectGraphic);
            var i = Runner.objects.IndexOf(original);
            Runner.objects.ForEach(o => o.Collider.Collisions.RemoveAt(i));
            Runner.objects.ForEach(o => o.Collider.Triggers.RemoveAt(i));
            Runner.objects.Remove(original);

        }
    }
}
