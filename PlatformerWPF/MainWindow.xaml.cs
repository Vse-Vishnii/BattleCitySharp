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
    public partial class MainWindow : Window
    {
        private double x = 0;
        private int frames = 0;
        private Timer timer;
        Stopwatch watch = new Stopwatch();
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
            frames++;
            var a = 0;
            if (watch.ElapsedMilliseconds >= 1000)
            {
                watch.Stop();
                a = frames;
            }
                
            this.Dispatcher.Invoke(() =>
            {
                x ++;
                Canvas.SetLeft(tank, x);
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
    }
}
