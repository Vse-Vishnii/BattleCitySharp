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
        public static T Instantiate<T>(T original, Grid.Cell cell, Direction rotation = Direction.Up)
            where T : GameObject
        {
            cell.ChangeObjectType(original);
            var point = new Vector2(cell.X, cell.Y);
            return Instantiate(original, point, rotation, Grid.CellSize);
        }

        public static T Instantiate<T>(T original, Vector2 point, Direction rotation = Direction.Up, int size = 70)
            where T : GameObject
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var objects = ObjectContainer.Objects;
                var shape = Drawer.DrawObject(point, original, size);
                original.CreateGameObjectProperties(point, rotation, size, shape);
                objects.Add(original);
                original.Collider.Collisions.AddRange(objects[0].Collider.Collisions);
                original.Collider.Triggers.AddRange(objects[0].Collider.Triggers);
                objects.ForEach(o => o.Collider.Collisions.Add(false));
                objects.ForEach(o => o.Collider.Triggers.Add(false));
                original.Start();
            });
            return original;
        }

        public static void Destroy(GameObject original, bool clearCell = true)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if(original is not MovingObject && clearCell)
                    Grid.ClearCell(original.Transform.Position);
                var objects = ObjectContainer.Objects;
                Drawer.DeleteObject(original);
                var i = objects.IndexOf(original);
                if (i != -1)
                {
                    objects.ForEach(o => o.Collider.Collisions.RemoveAt(i));
                    objects.ForEach(o => o.Collider.Triggers.RemoveAt(i));
                }                
                objects.Remove(original);
            });
        }
    }
}
