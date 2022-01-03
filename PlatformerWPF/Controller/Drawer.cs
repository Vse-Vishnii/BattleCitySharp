using BattleCitySharp.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BattleCitySharp
{
    public static class Drawer
    {
        private static Canvas canvas;

        private static readonly Dictionary<Direction, int> directionToAngle = new Dictionary<Direction, int>
        {
            {Direction.Up,0 },
            {Direction.Right,90 },
            {Direction.Down,180 },
            {Direction.Left,270 }
        };

        private static readonly Dictionary<ObjectType, Uri> typeUri = new Dictionary<ObjectType, Uri>
        {
            {ObjectType.Manager, new Uri("pack://application:,,,/images/empty.png")},
            {ObjectType.Player, new Uri("pack://application:,,,/images/tank1.png") },
            {ObjectType.Wall, new Uri("pack://application:,,,/images/brick.png") },
            {ObjectType.Bullet, new Uri("pack://application:,,,/images/projectile.png") },
            {ObjectType.Enemy, new Uri("pack://application:,,,/images/enemy1.png") },
            {ObjectType.Base, new Uri("pack://application:,,,/images/base.png") }
        };

        private static Uri[] wallTextures = new Uri[]
        {
            new Uri("pack://application:,,,/images/brick.png"),
            new Uri("pack://application:,,,/images/steel.png"),
            new Uri("pack://application:,,,/images/bush.png"),
            new Uri("pack://application:,,,/images/water.png")
        };

        private static Dictionary<GameObject, ObjectMaterial> gameObjectMaterials = new Dictionary<GameObject, ObjectMaterial>();

        public static void SetCanvas(Canvas c)
        {
            canvas = c;
        }

        public static void DeleteObject(GameObject gameobject)
        {
            var image = gameObjectMaterials[gameobject].Graphic;
            canvas.Children.Remove(image);
            gameObjectMaterials.Remove(gameobject);
        }

        private static Image CreateImage(int size)
        {
            return new Image() {Width = size, Height = size};
        }

        public static ColliderShape DrawObject(Vector2 point, GameObject original, int size = 70)
        {
            var image = CreateImage(size);           
            Canvas.SetLeft(image, point.X);
            Canvas.SetTop(image, point.Y);
            var objectType = original.GameObjectType;
            if (objectType != ObjectType.Manager)
                image.Source = BitmapFrame.Create(typeUri[objectType]);
            canvas.Children.Add(image);
            SetPriority(image, 1);
            gameObjectMaterials.Add(original, new ObjectMaterial(image));
            return new ColliderShape(Canvas.GetLeft(image), Canvas.GetTop(image), Canvas.GetRight(image), Canvas.GetBottom(image));
        }

        public static void RotateObject(GameObject gameObject, int axisX = 35, int axisY = 35)
        {
            var graphic = gameObjectMaterials[gameObject].Graphic;
            
            var angle = directionToAngle[gameObject.Transform.Direction];
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                RotateTransform rotateTransform1 = new RotateTransform(angle);

                //Центр вращения
                rotateTransform1.CenterX = axisX;
                rotateTransform1.CenterY = axisY;
                graphic.RenderTransform = rotateTransform1;
            });  
        }

        public static void Move(GameObject gameObject, Vector2 direction, float speed)
        {
            var image = gameObjectMaterials[gameObject].Graphic;

            var pos = gameObject.Transform.ChangePosition(direction, speed);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                Canvas.SetLeft(image, pos.X);
                Canvas.SetTop(image, pos.Y);
            });            
        }

        public static void SetPriority(Image image,int priority)
        {
            Canvas.SetZIndex(image, priority);
        }

        public static void ChangeBoxMaterial(Box original, int stateNumber)
        {
            var health = original.GetComponent<Health>();
            var image = gameObjectMaterials[original].Graphic;
            image.Source = BitmapFrame.Create(wallTextures[stateNumber]);
            if (stateNumber >= 2)
            {
                original.Collider.IsTrigger = true;
                var z = stateNumber == 2 ? 2 : 0;
                Drawer.SetPriority(image, z);
            }
            else if (stateNumber == 0)
                health = new Health(1, original);
            else
                health = new Health(100, original);
        }
    }
}