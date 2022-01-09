using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BattleCitySharp.Controller
{
    public abstract class UIDrawer
    {
        private static Canvas canvas;
        private static Stack<Image> enemies = new Stack<Image>();
        
        private static Uri[] numbersIcons = new Uri[]
        {
            new Uri("pack://application:,,,/images/zero.png"),
            new Uri("pack://application:,,,/images/one.png"),
            new Uri("pack://application:,,,/images/two.png"),
            new Uri("pack://application:,,,/images/three.png"),
            new Uri("pack://application:,,,/images/four.png"),
            new Uri("pack://application:,,,/images/five.png"),
        };

        public static void SetCanvas(Canvas c)
        {
            canvas = c;
        }
        
        public static void DeleteObject()
        {
            Application.Current.Dispatcher.Invoke(() => { canvas.Children.Remove(enemies.Pop()); });
        }

        public static void CreateImage()
        {
            //enemies.Add(img);
        }

        public static void CreateEnemiesInfo(int enemyCount)
        {
            var topOffset = 0;
            
            for (var i = 0; i < enemyCount; i++)
            {
                var img = new Image() {Width = 45, Height = 45};
                img.Source = new BitmapImage(new Uri("pack://application:,,,/images/enemyIcon.png"));
                if (i > 0)
                {
                    Canvas.SetTop(img, topOffset);
                    Canvas.SetLeft(img, 55);
                    if (i % 2 == 0)
                    {
                        Canvas.SetTop(img, topOffset += 55);
                        Canvas.SetLeft(img, 0);
                    }
                }
                
                enemies.Push(img);
                canvas.Children.Add(img);
            }
        }

        public static void CreatePlayerInfo()
        {
            var playerNumberIcon = new Image() {Width = 100, Height = 45};
            playerNumberIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/playerNumberIcon.png"));
            var playerIcon = new Image() {Width = 45, Height = 45};
            playerIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/playerIcon.png"));

            Canvas.SetTop(playerNumberIcon, 200);
            Canvas.SetTop(playerIcon, 255);
            canvas.Children.Add(playerNumberIcon);
            canvas.Children.Add(playerIcon);
        }

        public static void UpdatePlayerInfo(int health)
        {
            var img = new Image() {Width = 45, Height = 45};
            img.Source = new BitmapImage(numbersIcons[health]);
            Canvas.SetLeft(img, 55);
            Canvas.SetTop(img, 255);
            canvas.Children.Add(img);
        }
    }
}