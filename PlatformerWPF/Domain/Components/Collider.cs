using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BattleCitySharp
{
    public class Collider
    {
        private GameObject gameObject { get; }
        public List<bool> Collisions { get; } = new List<bool>();
        public void CheckCollision(GameObject[] gameObjects)
        {
            var graphic = gameObject.ObjectGraphic;
            foreach(var obj in gameObjects)
            {
                int index = Array.IndexOf(gameObjects, obj);
                Collisions[index] = ExecuteCollider(graphic, obj.ObjectGraphic, obj, Collisions[index]);
            }
        }

        private bool ExecuteCollider(Image graphic, Image other, GameObject obj, bool collision)
        {
            var rect1 = new Rect(Canvas.GetLeft(graphic), Canvas.GetTop(graphic), graphic.Width, graphic.Height);
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
            return collision;
        }
    }
}
