using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCitySharp
{
    public class Collider
    {
        public bool IsTrigger { get; set; } = false;

        public GameObject GameObject { get; }
        public List<bool> Collisions { get; } = new List<bool>();
        public List<bool> Triggers { get; } = new List<bool>();

        private ColliderShape shape;

        private float x;
        private float y;

        public Collider(GameObject gameObject, ColliderShape shape)
        {
            this.GameObject = gameObject;
            this.shape = shape;
        }

        public bool CanMove()
        {
            foreach (var collision in Collisions.ToList())
            {
                var size = GameObject.Transform.Size;
                var rect = new Rect(x, y, size / 2, size / 2);
                var inBounds = CheckInBounds(rect);
                if (collision || !inBounds)
                    return false;
            }
            return true;
        }

        private static bool CheckInBounds(Rect rect)
        {
            var a = rect.Left >= 0;
            var b = rect.Right <= Grid.Instance.SizeX * Grid.CellSize;
            var c = rect.Top >= 0;
            var d = rect.Bottom <= Grid.Instance.SizeY * Grid.CellSize;
            return a && b && c && d;
        }

        public async void CheckCollision(GameObject[] gameObjects)
        {
            var shape = GameObject.Collider.shape;
            if (Collisions.Count > 0)
                foreach(var obj in gameObjects)
                {
                    if (!GameObject.Equals(obj))
                    {
                        var i = Array.IndexOf(gameObjects, obj);
                        await Task.Run(()=> ExecuteCollider(shape, obj.Collider.shape, obj, i));
                    }
                }
        }

        public bool[] OverlapSquare()
        {
            var collisions = new List<bool>();
            var shape = GameObject.Collider.shape;
            var rect1 = SetStandartRect(shape, 1);
            foreach (var obj in ObjectContainer.Objects.Where(o => o != GameObject))
            {
                var other = obj.Collider.shape;
                var rect2 = SetStandartRect(other, 1);
                collisions.Add(rect1.IntersectsWith(rect2) ? true : false);
            }
            return collisions.ToArray();
        }

        private void ExecuteCollider(ColliderShape shape, ColliderShape other, GameObject obj, int i)
        {
            var collision = false;
            var trigger = false;
            if (i < Collisions.Count)
            {
                collision = Collisions[i];
                trigger = Triggers[i];
            }
            if (!(GameObject is MovingObject) && !(obj is MovingObject))
                return;
            Application.Current.Dispatcher.Invoke(() => Collide(shape, other, obj, i, ref collision, ref trigger));
        }

        private void Collide(ColliderShape shape, ColliderShape other, GameObject obj, int i, ref bool collision, ref bool trigger)
        {
            var rect1 = SetRect1(shape);
            var rect2 = SetStandartRect(other);
            if (rect1.IntersectsWith(rect2))
                ProcessIntersection(obj, ref collision, ref trigger);
            else if (trigger)
            {
                trigger = false;
                GameObject.ColliderExit(obj.Collider);
            }
            else
                collision = false;
            if (i < Collisions.Count)
            {
                Collisions[i] = collision;
                Triggers[i] = trigger;
            }
        }

        private void ProcessIntersection(GameObject obj, ref bool collision, ref bool trigger)
        {
            if (trigger)
                GameObject.ColliderStay(obj.Collider);
            else
            {
                if (IsTrigger || obj.Collider.IsTrigger)
                {
                    trigger = true;
                    GameObject.ColliderEnter(obj.Collider);
                }
                else
                    collision = true;
            }
        }

        private Rect SetRect1(ColliderShape shape)
        {
            var rect1 = SetStandartRect(shape);
            if (GameObject is MovingObject)
            {
                rect1 = SetMovingCollider();
            }

            return rect1;
        }

        private static Rect SetStandartRect(ColliderShape shape)
        {
            return new Rect(shape.Left, shape.Top, shape.Width, shape.Height);
        }

        private static Rect SetStandartRect(ColliderShape shape, int offset)
        {
            return new Rect(shape.Left + offset, shape.Top + offset, shape.Width - (offset + 1), shape.Height - (offset + 1));
        }

        private Rect SetMovingCollider()
        {
            var tank = GameObject as MovingObject;
            var pos = tank.Transform.Position;
            var size = tank.Transform.Size;
            var moveDir = tank.Transform.MoveDirection;
            if (moveDir.Y != 0)
            {
                x = pos.X + size / 4;
                y = moveDir.Y > 0 ? pos.Y + size / 2 : pos.Y;
            }
            else if (moveDir.X != 0)
            {
                y = pos.Y + size / 4;
                x = moveDir.X > 0 ? pos.X + size / 2 : pos.X;
            }

            var rect1 = new Rect(x, y, size / 2, size / 2);
            return rect1;
        }
    }
}
