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
using BattleCitySharp;

namespace BattleCitySharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Runner.Start();
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
