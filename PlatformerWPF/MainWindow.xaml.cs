using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Random random = new Random();
        private int offset = 0;
        
        private Uri[] wallType = new Uri[]
        {
            new Uri("pack://application:,,,/Resourses/wall1.png"),
            new Uri("pack://application:,,,/Resourses/wall2.png"),
            new Uri("pack://application:,,,/Resourses/wall3.png"),
            new Uri("pack://application:,,,/Resourses/wall4.png")
        };
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            var wallIndex = random.Next(wallType.Length);
            Wall1.Source = BitmapFrame.Create(wallType[wallIndex]);
            offset++;
            Canvas.SetLeft(Tank1, offset);
            //Canvas1.UpdateLayout();
        }
    }
}