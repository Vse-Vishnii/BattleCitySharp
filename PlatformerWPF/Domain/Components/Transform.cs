using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Direction Direction { get; set; }
        public int Size { get; set; }

        public Vector2 ChangePosition (Vector2 direction, float speed)
        {
            var x = Position.X + direction.X * speed;
            var y = Position.Y + direction.Y * speed;
            Position = new Vector2(x, y);
            return Position;
        }
    }
}
