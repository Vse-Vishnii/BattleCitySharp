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
    public class Drawer
    {
        private static Canvas canvas;
        private static float a = 0;

        private static Uri[] wallType = new Uri[]
        {
            new Uri("pack://application:,,,/images/wall1.png"),
            new Uri("pack://application:,,,/images/wall2.png"),
            new Uri("pack://application:,,,/images/wall3.png"),
            new Uri("pack://application:,,,/images/wall4.png")
        };
        
        private static Uri[] tankType = new Uri[]
        {
            new Uri("pack://application:,,,/images/tank1.png")
        };

        private static readonly Dictionary<Direction, int> directionToAngle = new Dictionary<Direction, int>
        {
            {Direction.Up,0 },
            {Direction.Right,90 },
            {Direction.Down,180 },
            {Direction.Left,270 }
        };

        private static readonly Dictionary<ObjectType, Uri> typeUri = new Dictionary<ObjectType, Uri>
        {
            {ObjectType.Player, new Uri("pack://application:,,,/images/tank1.png") },
            {ObjectType.Wall, new Uri("pack://application:,,,/images/wall1.png") }
        };

        public static void SetCanvas(Canvas c)
        {
            canvas = c;
        }

        public static Image DrawObject(Vector2 point, ObjectType objectType, int size = 70)
        {
            var tank1 = new Image();
            tank1.Width = size;
            tank1.Height = size;
            Canvas.SetLeft(tank1, point.X);
            Canvas.SetTop(tank1, point.Y);
            if (objectType != ObjectType.Manager)
                tank1.Source = BitmapFrame.Create(typeUri[objectType]);
            canvas.Children.Add(tank1);
            return tank1;
        }

        public static void RotateObject(GameObject gameObject)
        {
            var tank1 = gameObject.ObjectGraphic;
            var angle = directionToAngle[gameObject.Transform.Direction];
            Application.Current.Dispatcher.Invoke(() =>
            {
                RotateTransform rotateTransform1 = new RotateTransform(angle);

                //Центр вращения
                rotateTransform1.CenterX = 35;
                rotateTransform1.CenterY = 35;
                tank1.RenderTransform = rotateTransform1;
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