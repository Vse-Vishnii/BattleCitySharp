using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public class Player : GameObject
    {
        private float speed;
        private float rotateSpeed;

        private Transform tank;
        private Direction moveDir;
        private Direction rotateDir;
        private float slowSpeed;

        private Input input;

        public Player(Transform transform)
        {
            slowSpeed = speed / 2;
            tank = transform;
            rotateDir = transform.Direction;
        }

        public override void Update()
        {
            var vertical = input.GetAxis("Vertical");
            var horizontal = input.GetAxis("Horizontal");
            if (vertical != 0)
            {
                moveDir = vertical > 0 ? Direction.Up : Direction.Down;
                speed *= vertical;
            }
            else if(horizontal != 0)
            {
                moveDir = horizontal > 0 ? Direction.Right : Direction.Left;
                speed *= horizontal;
            }
            Move(moveDir, speed);
        }

        private void Move(Direction moveDir, float speed)
        {
            throw new NotImplementedException();
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
