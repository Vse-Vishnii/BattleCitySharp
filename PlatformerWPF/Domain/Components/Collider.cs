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
        public static string DebugTest = "";

        private GameObject gameObject { get; }
        public List<bool> Collisions { get; } = new List<bool>();

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
                        await Task.Run(()=> ExecuteCollider(graphic, obj.ObjectGraphic, obj, gameObjects));
                    }
                }
        }

        private void ExecuteCollider(Image graphic, Image other, GameObject obj, GameObject[] gameObjects)
        {
            var index = Array.IndexOf(gameObjects, obj);
            var collision = Collisions[index];
            Application.Current.Dispatcher.Invoke(() =>
            {
                var rect1 = SetRect1(graphic);
                var rect2 = new Rect(Canvas.GetLeft(other), Canvas.GetTop(other), other.Width, other.Height);
                if (rect1.IntersectsWith(rect2))
                    if (collision)
                        gameObject.ColliderStay(obj.Collider);
                    else
                    {
                        collision = true;
                        gameObject.ColliderEnter(obj.Collider);
                    }
                else if (collision)
                {
                    collision = false;
                    gameObject.ColliderExit(obj.Collider);
                }
                Collisions[index] = collision;
            });
        }

        private Rect SetRect1(Image graphic)
        {
            var rect1 = new Rect(Canvas.GetLeft(graphic), Canvas.GetTop(graphic), graphic.Width, graphic.Height);
            if (gameObject.GameObjectType == ObjectType.Player)
            {
                var player = gameObject as Player;
                var pos = player.Transform.Position;
                var size = player.Transform.Size;
                if (player.MoveDir.Y != 0)
                {
                    playerX = pos.X + size / 4;
                    playerY = player.MoveDir.Y > 0 ? pos.Y + size / 2 : pos.Y;
                }
                else if (player.MoveDir.X != 0)
                {
                    playerY = pos.Y + size / 4;
                    playerX = player.MoveDir.X > 0 ? pos.X + size / 2 : pos.X;
                }

                rect1 = new Rect(playerX, playerY, size / 2, size / 2);
            }

            return rect1;
        }
    }
}
