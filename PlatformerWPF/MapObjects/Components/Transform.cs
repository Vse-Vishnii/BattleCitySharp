using System.Numerics;

namespace BattleCitySharp
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Direction Direction { get; set; }
        public Vector2 MoveDirection 
        {
            get
            {
                var x = Direction == Direction.Right ? 1 : Direction == Direction.Left ? -1 : 0;
                var y = Direction == Direction.Down ? 1 : Direction == Direction.Up ? -1 : 0;
                return new Vector2(x, y);
            } 
        }
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
