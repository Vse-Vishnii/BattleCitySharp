using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BattleCitySharp
{
    public class Collider
    {
        public bool IsTrigger { get; set; } = false;

        public GameObject GameObject { get; }
        public List<bool> Collisions { get; } = new List<bool>();
        public List<bool> Triggers { get; } = new List<bool>();

        private float tankX = 0;
        private float tankY = 0;

        public Collider(GameObject gameObject)
        {
            this.GameObject = gameObject;
        }

        public bool CanMove()
        {
            foreach (var collision in Collisions.ToList())
            {
                var size = GameObject.Transform.Size;
                var rect = new Rect(tankX, tankY, size / 2, size / 2);
                var inBounds = CheckInBounds(rect);
                if (collision || !inBounds)
                    return false;
            }
            return true;
        }

        private static bool CheckInBounds(Rect rect)
        {
            var a = rect.Left >= 0;
            var b = rect.Right <= Grid.Map.GetLength(0) * Cell.CellSize;
            var c = rect.Top >= 0;
            var d = rect.Bottom <= Grid.Map.GetLength(1) * Cell.CellSize;
            return a && b && c && d;
        }

        public async void CheckCollision(GameObject[] gameObjects)
        {
            var graphic = GameObject.ObjectGraphic;
            if (Collisions.Count > 0)
                foreach(var obj in gameObjects)
                {
                    if (!GameObject.Equals(obj))
                    {
                        var i = Array.IndexOf(gameObjects, obj);
                        await Task.Run(()=> ExecuteCollider(graphic, obj.ObjectGraphic, obj, i));
                    }
                }
        }

        public bool[] OverlapSquare()
        {
            var collisions = new List<bool>();
            var graphic = GameObject.ObjectGraphic;
            var rect1 = new Rect(Canvas.GetLeft(graphic) + 1, Canvas.GetTop(graphic) + 1, graphic.Width - 2, graphic.Height - 2);
            foreach (var obj in Runner.objects.Where(o => o != GameObject))
            {
                var other = obj.ObjectGraphic;                
                var rect2 = new Rect(Canvas.GetLeft(other) + 1, Canvas.GetTop(other) + 1, other.Width - 2, other.Height - 2);
                collisions.Add(rect1.IntersectsWith(rect2) ? true : false);
            }
            return collisions.ToArray();
        }

        private void ExecuteCollider(Image graphic, Image other, GameObject obj, int i)
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
            Application.Current.Dispatcher.Invoke(() =>
            {
                var rect1 = SetRect1(graphic);
                var rect2 = new Rect(Canvas.GetLeft(other), Canvas.GetTop(other), other.Width, other.Height);
                if (rect1.IntersectsWith(rect2))
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
            });
        }

        private Rect SetRect1(Image graphic)
        {
            var rect1 = new Rect(Canvas.GetLeft(graphic), Canvas.GetTop(graphic), graphic.Width, graphic.Height);
            if (GameObject is MovingObject)
            {
                rect1 = SetTankCollider();
            }

            return rect1;
        }

        private Rect SetTankCollider()
        {
            var tank = GameObject as MovingObject;
            var pos = tank.Transform.Position;
            var size = tank.Transform.Size;
            var moveDir = tank.Transform.MoveDirection;
            if (moveDir.Y != 0)
            {
                tankX = pos.X + size / 4;
                tankY = moveDir.Y > 0 ? pos.Y + size / 2 : pos.Y;
            }
            else if (moveDir.X != 0)
            {
                tankY = pos.Y + size / 4;
                tankX = moveDir.X > 0 ? pos.X + size / 2 : pos.X;
            }

            var rect1 = new Rect(tankX, tankY, size / 2, size / 2);
            return rect1;
        }
    }
}
