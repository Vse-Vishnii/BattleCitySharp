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
        private float speed = 5;
        private float rotateSpeed;

        private Transform tank;
        private Vector2 moveDir;
        private Direction rotateDir;
        private float slowSpeed;

        private Input input;

        public Player(Input input)
        {
            this.input = input;
            slowSpeed = speed / 2;
            //tank = transform;            
        }

        public override void Start()
        {
            rotateDir = Transform.Direction;
        }

        public override void Update()
        {
            var vertical = input.GetAxis("Vertical");
            var horizontal = input.GetAxis("Horizontal");
            moveDir = new Vector2(horizontal, vertical);
            Move(moveDir, speed);
        }

        private void Move(Vector2 moveDir, float speed)
        {
            Drawer.Move(this, moveDir, speed);
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
