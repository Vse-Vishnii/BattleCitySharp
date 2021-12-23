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
            {ObjectType.Enemy, new Uri("pack://application:,,,/images/enemy1.png") }
        };

        public static void SetCanvas(Canvas c)
        {
            canvas = c;
        }

        private static Image CreateImage(int size)
        {
            return new Image() {Width = size, Height = size};
        }

        public static Image DrawObject(Vector2 point, ObjectType objectType, int size = 70)
        {
            var image = CreateImage(size);            
            Canvas.SetLeft(image, point.X);
            Canvas.SetTop(image, point.Y);
            if (objectType != ObjectType.Manager)
                image.Source = BitmapFrame.Create(typeUri[objectType]);
            canvas.Children.Add(image);
            return image;
        }

        public static void DeleteObject(Image image)
        {
            canvas.Children.Remove(image);
        }

        public static void RotateObject(GameObject gameObject, int axisX = 35, int axisY = 35)
        {
            var graphic = gameObject.ObjectGraphic;
            
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
            var image = gameObject.ObjectGraphic;

            var pos = gameObject.Transform.ChangePosition(direction, speed);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                Canvas.SetLeft(image, pos.X);
                Canvas.SetTop(image, pos.Y);
            });            
        }
    }
}