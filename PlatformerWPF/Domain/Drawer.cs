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

        public static void SetCanvas(Canvas c)
        {
            canvas = c;
        }

        public static void DrawObject()
        {
            var tank1 = new Image();
            tank1.Width = 70;
            tank1.Height = 70;
            Canvas.SetLeft(tank1, 1);
            Canvas.SetTop(tank1, 0);

            tank1.Source = BitmapFrame.Create(new Uri("pack://application:,,,/images/tank1.png"));
            canvas.Children.Add(tank1);
        }

        public static void RotateObject(Image tank1)
        {
            RotateTransform rotateTransform1 = new RotateTransform(0);

            //Центр вращения
            rotateTransform1.CenterX = 35;
            rotateTransform1.CenterY = 35;
            tank1.RenderTransform = rotateTransform1;
        }

        public static void Move(GameObject gameObject, Vector2 direction, float speed)
        {
            var image = gameObject.ObjectGraphic;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Canvas.SetLeft(image, direction.X * speed);
                Canvas.SetTop(image, direction.Y * speed);
            });            
        }
    }
}