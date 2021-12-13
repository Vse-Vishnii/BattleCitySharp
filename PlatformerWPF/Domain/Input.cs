using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCitySharp
{
    public class Input
    {
        //Переменные для отслеживания

        //Какая клавиша нажата

        //Стрельба на клаве

        private Key[] keys = new[] {Key.A, Key.W, Key.S, Key.D, Key.Space};

        private Dictionary<Key, bool> keyboardPressed = new Dictionary<Key, bool>()
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
                    keyboardPressed[key] = true;
                    break;
                }
            }
        }

        public void ResetKeys()
        {
            foreach (var key in keyboardPressed.Keys)
                keyboardPressed[key] = false;
        }

        public bool GetPressedButton(Key key)
        {
            return keyboardPressed[key];
        }

        public sbyte GetAxis(string axis)
        {
            if (axis == "Horizontal")
                return (sbyte)(keyboardPressed[Key.A] ? -1 : keyboardPressed[Key.D] ? 1 : 0);
            if (axis == "Vertical")
                return (sbyte)(keyboardPressed[Key.S] ? -1 : keyboardPressed[Key.W] ? 1 : 0);
            throw new ArgumentException("Неправильно указано имя оси.");
        }
    }
}
