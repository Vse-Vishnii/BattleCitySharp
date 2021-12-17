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
            {ObjectType.Idle, new Uri("")},
            {ObjectType.Player, new Uri("pack://application:,,,/images/tank1.png") },
            {ObjectType.Wall, new Uri("pack://application:,,,/images/wall1.png") }
        };

        public static void SetCanvas(Canvas c)
        {
            canvas = c;
        }

        private static Image CreateImage((int,int) size)
        {
            return new Image() {Width = size.Item1, Height = size.Item2};
        }

        public static Image DrawObject(Cell cell, ObjectType objectType, int sizeX = 70, int sizeY = 70)
        {
            var image = CreateImage((sizeX, sizeY));
            //
            // image.Width = sizeX;
            // image.Height = sizeY;
            
            Canvas.SetLeft(image, cell.X * 70);
            Canvas.SetTop(image, cell.Y * 70);
            
            if (objectType != ObjectType.Manager)
                image.Source = BitmapFrame.Create(typeUri[objectType]);
            canvas.Children.Add(image);
            return image;
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