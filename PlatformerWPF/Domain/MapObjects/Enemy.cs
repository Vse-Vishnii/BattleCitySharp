using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class Enemy : MovingObject
    {
        private int bulletSize = 12;
        private float speed = 5;
        private float slowSpeed;

        private float cooldown = 1f;
        private float currentCooldown;

        private Dictionary<Direction, Func<Vector2>> shootPoint;

        public Enemy()
        {
            slowSpeed = speed / 2;
            currentCooldown = 0;
            GameObjectType = ObjectType.Enemy;
            // shootPoint = new Dictionary<Direction, Func<Vector2>>
            // {
            //     {Direction.Up, ()=> GetShootPoint(0,-1) },
            //     {Direction.Right,()=> GetShootPoint(1,0) },
            //     {Direction.Down,()=> GetShootPoint(0,1) },
            //     {Direction.Left,()=> GetShootPoint(-1,0) }
            // };
        }

        public override void Update()
        {
            ProcessMoving();
            //ProcessShooting();
        }

        // private void ProcessShooting()
        // {
        //     currentCooldown -= Time.DeltaTime;
        //     if (input.GetPressedButton(Key.Space))
        //     {
        //         if (currentCooldown <= 0)
        //         {
        //             Application.Current.Dispatcher.Invoke(() =>
        //             {
        //                 Core.Instantiate(new Bullet(), shootPoint[Transform.Direction](), Transform.Direction, bulletSize);
        //             });
        //             currentCooldown = cooldown;
        //         }
        //     }
        // }

        private void ProcessMoving()
        {
            var moveDir = new Vector2(0, 1);
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
    }
}
