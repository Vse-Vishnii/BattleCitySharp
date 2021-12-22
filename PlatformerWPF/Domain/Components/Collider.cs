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

        private GameObject gameObject { get; }
        public List<bool> Collisions { get; } = new List<bool>();
        public List<bool> Triggers { get; } = new List<bool>();

        private float playerX = 0;
        private float playerY = 0;

        public Collider(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public bool CanMove()
        {
            foreach (var c in Collisions.ToList())
            {
                if (c)
                    return false;
            }                
            return true;
        }

        public async void CheckCollision(GameObject[] gameObjects)
        {
            var graphic = gameObject.ObjectGraphic;
            if (Collisions.Count > 0)
                foreach(var obj in gameObjects)
                {
                    if (!gameObject.Equals(obj))
                    {
                        var i = Array.IndexOf(gameObjects, obj);
                        await Task.Run(()=> ExecuteCollider(graphic, obj.ObjectGraphic, obj, i));
                    }
                }
        }

        private void ExecuteCollider(Image graphic, Image other, GameObject obj, int i)
        {            
            var collision = Collisions[i];
            var trigger = Triggers[i];
            Application.Current.Dispatcher.Invoke(() =>
            {
                var rect1 = SetRect1(graphic);
                var rect2 = new Rect(Canvas.GetLeft(other), Canvas.GetTop(other), other.Width, other.Height);
                if (rect1.IntersectsWith(rect2))
                    if (trigger)
                        gameObject.ColliderStay(obj.Collider);
                    else
                    {
                        if (obj.Collider.IsTrigger)
                        {
                            trigger = true;
                            gameObject.ColliderEnter(obj.Collider);
                        }
                        collision = true;                        
                    }
                else if (trigger)
                {
                    trigger = false;                    
                    gameObject.ColliderExit(obj.Collider);
                }
                else
                    collision = false;
                Collisions[i] = collision;
            });
        }

        private Rect SetRect1(Image graphic)
        {
            var rect1 = new Rect(Canvas.GetLeft(graphic), Canvas.GetTop(graphic), graphic.Width, graphic.Height);
            if (gameObject.GameObjectType == ObjectType.Player)
            {
                rect1 = SetPlayerCollider();
            }

            return rect1;
        }

        private Rect SetPlayerCollider()
        {
            var player = gameObject as Player;
            var pos = player.Transform.Position;
            var size = player.Transform.Size;
            var moveDir = player.Transform.MoveDirection;
            if (moveDir.Y != 0)
            {
                playerX = pos.X + size / 4;
                playerY = moveDir.Y > 0 ? pos.Y + size / 2 : pos.Y;
            }
            else if (moveDir.X != 0)
            {
                playerY = pos.Y + size / 4;
                playerX = moveDir.X > 0 ? pos.X + size / 2 : pos.X;
            }

            var rect1 = new Rect(playerX, playerY, size / 2, size / 2);
            return rect1;
        }
    }
}
