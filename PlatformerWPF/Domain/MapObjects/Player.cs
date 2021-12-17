using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public class Player : GameObject
    {
        public bool CanMove;
        private float speed = 5;

        public Vector2 MoveDir { get; private set; }
        private float slowSpeed;

        private Input input;

        public Player(Input input)
        {
            this.input = input;
            slowSpeed = speed / 2;
            GameObjectType = ObjectType.Player;            
        }

        public override void Update()
        {
            var vertical = input.GetAxis("Vertical");
            var horizontal = input.GetAxis("Horizontal");
            MoveDir = new Vector2(horizontal, vertical);
            Transform.Direction = GetDirection(MoveDir);
            Move(MoveDir, speed);
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

        private void Move(Vector2 moveDir, float speed)
        {
            if (moveDir.X != 0 || moveDir.Y != 0)
            {
                Drawer.RotateObject(this);
                CanMove = Collider.CanMove();
                if (CanMove)
                {
                    Drawer.Move(this, moveDir, speed);
                }                
            }            
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
