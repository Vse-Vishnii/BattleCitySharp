using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCitySharp.Domain
{
    public class Input
    {
        //Переменные для отслеживания

        //Какая клавиша нажата

        //Стрельба на клаве

        private Key[] keys = new[] {Key.A, Key.W, Key.S, Key.D, Key.Space};

        private Dictionary<Key, bool> dict = new Dictionary<Key, bool>()
        {
            {Key.A, false},
            {Key.W, false},
            {Key.S, false},
            {Key.D, false},
            {Key.Space, false}
        };

        public void SetKeyEventArgs(KeyEventArgs e)
        {
            foreach (var key in keys)
            {
                if (e.Key == key)
                {
                    dict[key] = true;
                }
            }
        }
    }
}
