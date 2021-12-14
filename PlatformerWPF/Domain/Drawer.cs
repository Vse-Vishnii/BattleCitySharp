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
            new Uri("pack://application:,,,/images/tank1.png"),
            //new Uri("pack://application:,,,/images/wall2.png"),
            //new Uri("pack://application:,,,/images/wall3.png"),
            //new Uri("pack://application:,,,/images/wall4.png")
        };

        public static void SetCanvas(Canvas c)
        {
            canvas = c;
        }

        public static Image DrawObject(Cell cell, ObjectType objectType)
        {
            var tank1 = new Image();
            tank1.Width = 70;
            tank1.Height = 70;
            Canvas.SetLeft(tank1, 1);
            Canvas.SetTop(tank1, 0);

            tank1.Source = BitmapFrame.Create(new Uri("pack://application:,,,/images/tank1.png"));
            canvas.Children.Add(tank1);
            return tank1;
        }

        public static void RotateObject(GameObject gameObject)
        {
            var tank1 = gameObject.ObjectGraphic;
            Application.Current.Dispatcher.Invoke(() =>
            {
                a += 90;
                RotateTransform rotateTransform1 = new RotateTransform(a);

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
            // var x = gameObject.Transform.Position.X;
            // var y = gameObject.Transform.Position.Y;
            // gameObject.Transform.Position = new Vector2(x + direction.X * speed, y + direction.Y * speed);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                Canvas.SetLeft(image, pos.X);
                Canvas.SetTop(image, pos.Y);
            });            
        }
    }
}