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
        private int backMultiplier;

        public Player(Transform transform)
        {
            slowSpeed = speed / 2;
            tank = transform;
            rotateDir = transform.Direction;
        }

        //public override void Update()
        //{
        //    var vertical = Input.GetAxis("Vertical");
        //    backMultiplier = vertical < 0 ? -1 : 1;
        //    moveDir = tank.forward * vertical * Speed * Time.DeltaTime;
        //    Move(moveDir);
        //}

        private void Move(Direction moveDir)
        {
            throw new NotImplementedException();
        }

        //public override void LateUpdate()
        //{
        //    var horizontal = Input.GetAxis("Horizontal") * backMultiplier;
        //    tank.Direction = rotateDir;
        //}

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
