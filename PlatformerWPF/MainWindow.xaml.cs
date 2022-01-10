using System;
using System.Timers;
using System.Windows.Input;
using BattleCitySharp.Processing;

namespace BattleCitySharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        int frames = 0;
        int fps = 0;
        DateTime lastTime = DateTime.Now;
        public MainWindow()
        {
            InitializeComponent();
            GameManager.StartGame(Canvas1, Canvas2);
            var timer = new Timer(10);
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Runner.RunObjects();
        }
        
        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            Runner.Inputs[0].SetKeyEventArgs(e);
        }
    }
}
