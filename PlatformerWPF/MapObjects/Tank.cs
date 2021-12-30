using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCitySharp
{
    public abstract class Tank : MovingObject
    {
        private int bulletSize = 12;
        private Dictionary<Direction, Func<Vector2>> shootPoint;

        protected Vector2 moveDir;
        protected float speed = 5;

        protected float cooldown = 1f;
        protected float currentCooldown;

        private float slowSpeed;

        public Tank()
        {
            slowSpeed = speed / 2;
            shootPoint = new Dictionary<Direction, Func<Vector2>>
            {
                {Direction.Up, ()=> GetShootPoint(0,-1) },
                {Direction.Right,()=> GetShootPoint(1,0) },
                {Direction.Down,()=> GetShootPoint(0,1) },
                {Direction.Left,()=> GetShootPoint(-1,0) }
            };
        }

        public void SlowDown()
        {
            speed = slowSpeed;
        }

        public void SpeedUp()
        {
            speed = slowSpeed * 2;
        }

        protected virtual void ProcessMoving()
        {
            Transform.Direction = GetDirection(moveDir);
            Move(moveDir, speed);
        }

        protected virtual void ProcessShooting()
        {
            Shoot();
        }

        protected void Shoot()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Core.Instantiate(new Bullet(TeamId), shootPoint[Transform.Direction](), Transform.Direction, bulletSize);
            });
        }

        private Direction GetDirection(Vector2 moveDir)
        {
            var x = moveDir.X;
            var y = moveDir.Y;
            if (y != 0)
                return y > 0 ? Direction.Down : Direction.Up;
            if (x != 0)
                return x > 0 ? Direction.Right : Direction.Left;
            return Transform.Direction;
        }

        private Vector2 GetShootPoint(int x, int y)
        {
            var pos = Transform.Position;
            var size = Transform.Size;
            var dir = Transform.Direction;
            int offsetX = SetOffset(x, dir);
            var offsetY = SetOffset(y, dir);
            x++;
            y++;
            var pointX = pos.X + x * size / 2 + offsetX;
            var pointY = pos.Y + y * size / 2 + offsetY;
            return new Vector2(pointX, pointY);
        }

        private int SetOffset(int x, Direction dir) => x == 0 ? -bulletSize / 2 : dir == Direction.Left ? -bulletSize - 1 : 1;
    }
}
