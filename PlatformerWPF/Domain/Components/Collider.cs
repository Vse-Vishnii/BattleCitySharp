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

        public Collider(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public bool CanMove(Image graphic)
        {
            foreach (var c in Collisions)
                if (c)
                    return false;
            return true;
        }

        public void CheckCollision(GameObject[] gameObjects)
        {
            var graphic = gameObject.ObjectGraphic;
            if (Collisions.Count > 0)
                foreach(var obj in gameObjects)
                {
                    if (!gameObject.Equals(obj))
                    {
                        int index = Array.IndexOf(gameObjects, obj);
                        Collisions[index] = ExecuteCollider(graphic, obj.ObjectGraphic, obj, Collisions[index]);
                    }                    
                }
        }

        private bool ExecuteCollider(Image graphic, Image other, GameObject obj, bool collision)
        {            
            Application.Current.Dispatcher.Invoke(() =>
            {
                var rect1 = new Rect(Canvas.GetLeft(graphic), Canvas.GetTop(graphic), graphic.Width, graphic.Height);
                var rect2 = new Rect(Canvas.GetLeft(other), Canvas.GetTop(other), other.Width, other.Height);
                if (rect1.IntersectsWith(rect2))
                {
                    //
                    var a = 0;
                    if (gameObject.GameObjectType == ObjectType.Player)
                        a++;
                    //
                    if (collision)
                        gameObject.ColliderStay(obj.Collider);
                    else
                    {
                        collision = true;
                        gameObject.ColliderEnter(obj.Collider);
                    }
                }
                    
                else if (collision)
                {
                    collision = false;
                    gameObject.ColliderExit(obj.Collider);
                }
                return collision;
            });
            return false;
        }
    }
}
