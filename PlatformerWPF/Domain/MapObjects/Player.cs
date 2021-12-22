using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public class Player : MovingObject
    {
        private int bulletSize = 12;
        private float speed = 5;
        private float slowSpeed;

        private Input input;

        private float cooldown = 1f;
        private float currentCooldown;

        private Dictionary<Direction, Func<Vector2>> shootPoint;

        public Player(Input input)
        {
            this.input = input;
            slowSpeed = speed / 2;
            currentCooldown = 0;
            GameObjectType = ObjectType.Player;
            shootPoint = new Dictionary<Direction, Func<Vector2>>
            {
                {Direction.Up, ()=> GetShootPoint(0,-1) },
                {Direction.Right,()=> GetShootPoint(1,0) },
                {Direction.Down,()=> GetShootPoint(0,1) },
                {Direction.Left,()=> GetShootPoint(-1,0) }
            };
        }

        public override void Update()
        {
            ProcessMoving();
            ProcessShooting();
        }

        private void ProcessShooting()
        {
            currentCooldown -= Time.DeltaTime;
            if (input.GetPressedButton(Key.Space))
            {
                if (currentCooldown <= 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Core.Instantiate(new Bullet(), shootPoint[Transform.Direction](), Transform.Direction, bulletSize);
                    });
                    currentCooldown = cooldown;
                }
            }
        }

        private void ProcessMoving()
        {
            var vertical = input.GetAxis("Vertical");
            var horizontal = input.GetAxis("Horizontal");
            var moveDir = new Vector2(horizontal, vertical);
            Transform.Direction = GetDirection(moveDir);
            Move(moveDir, speed);
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
            var offsetX = x == 0 ? -bulletSize / 2 : dir == Direction.Left ? -bulletSize : 0;
            var offsetY = y == 0 ? -bulletSize / 2 : dir == Direction.Up ? -bulletSize : 0;
            x++;
            y++;                    
            var pointX = pos.X + x * size / 2 + offsetX;
            var pointY = pos.Y + y * size / 2 + offsetY;
            return new Vector2(pointX, pointY);
        }

        public void SlowDown()
        {
            speed = slowSpeed;
        }

        public void SpeedUp()
        {
            speed = slowSpeed * 2;
        }

        
    }
}
