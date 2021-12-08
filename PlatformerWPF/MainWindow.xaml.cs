using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleCitySharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Random random = new Random();
        private int offset = 0;
        private double x = 0;
        private int frames = 0;
        private Timer timer;
        Stopwatch watch = new Stopwatch();

        private Uri[] wallType = new Uri[]
        {
            new Uri("pack://application:,,,/images/wall1.png"),
            new Uri("pack://application:,,,/images/wall2.png"),
            new Uri("pack://application:,,,/images/wall3.png"),
            new Uri("pack://application:,,,/images/wall4.png")
        };

        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(10);
            timer.Enabled = true;
            watch.Start();
            timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);

        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Runner.RunObjects();
            frames++;
            var a = 0;
            if (watch.ElapsedMilliseconds >= 1000)
            {
                watch.Stop();
                a = frames;
            }

            this.Dispatcher.Invoke(() =>
            {
                x++;
                Canvas.SetLeft(Tank1, x);
            });
        }

        static long lastTime = 0;
        static double GetDeltaTime()
        {
            long now = DateTime.Now.Millisecond;
            double dT = (now - lastTime) / 1000; // / 1000
            lastTime = now;
            Console.WriteLine(dT);
            return dT;
        }

        private async void Canvas1_KeyDown(object sender, KeyEventArgs e)
        {
            offset+=50;
            Canvas.SetLeft(Tank1, offset);
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            /*var wallIndex = random.Next(wallType.Length);
            Wall1.Source = BitmapFrame.Create(wallType[wallIndex]);
            offset++;
            Canvas.SetLeft(Tank1, offset);*/
            //Canvas1.UpdateLayout();

            //Инициализация объекта (Дефолтные размеры и смещения, иначе они будут NaN)
            Image tank1 = DrawObject();

            RotateObject(tank1);

            //Добавление объекта на холст
            Canvas1.Children.Add(tank1);

            //Назовем их колаайдерами
            var rect1 = new Rect(Canvas.GetLeft(tank1), Canvas.GetTop(tank1), tank1.Width, tank1.Height);
            var rect2 = new Rect(Canvas.GetLeft(Wall1), Canvas.GetTop(Wall1), Wall1.Width, Wall1.Height);

            //Проверка на коллижн
            if (rect1.IntersectsWith(rect2))
            {
                
            }
        }

        private static void RotateObject(Image tank1)
        {
            //Поворот объекта
            RotateTransform rotateTransform1 = new RotateTransform(0);

            //Центр вращения
            rotateTransform1.CenterX = 35;
            rotateTransform1.CenterY = 35;
            tank1.RenderTransform = rotateTransform1;
        }

        private static Image DrawObject()
        {
            var tank1 = new Image();
            tank1.Width = 70;
            tank1.Height = 70;
            Canvas.SetLeft(tank1, 1);
            Canvas.SetTop(tank1, 0);

            tank1.Source = BitmapFrame.Create(new Uri("pack://application:,,,/images/tank1.png"));
            return tank1;
        }
    }
}
