using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCitySharp
{
    public class Enemy : Tank
    {
        public Health Health { get; }        
        
        private float slowSpeed;

        private float cooldown = 1f;
        private float currentCooldown;        

        private Vector2[] directions = new Vector2[4]
        {
            new Vector2(0, 0.1f),
            new Vector2(0.1f, 0),
            new Vector2(0, -0.1f),
            new Vector2(-0.1f, 0)
        };
        
        private int dirIndex;

        public Enemy()
        {
            slowSpeed = speed / 2;
            currentCooldown = 0;
            GameObjectType = ObjectType.Enemy;
            TeamId = 2;
            Health = new Health(1, this);            
        }

        public override void Start()
        {
            base.Start();
            var random = new Random();
            var dirIndex = random.Next(0, 3);
            moveDir = directions[dirIndex];
            cooldown = random.Next(1, 4);
            currentCooldown = cooldown;
        }

        public override void Update()
        {
            ProcessMoving();
            ProcessShooting();
        }

        protected override void ProcessShooting()
        {
            var random = new Random();            
            currentCooldown -= Time.DeltaTime;
            if (currentCooldown <= 0)
            {
                if (currentCooldown <= 0)
                {
                    Shoot();
                    cooldown = random.Next(1, 4);
                    currentCooldown = cooldown;
                }
            }
        }

        protected override void ProcessMoving()
        {
            if (!Collider.CanMove())
            {
                var random = new Random();
                var randDir = dirIndex;
                while (dirIndex == randDir)                
                    randDir = random.Next(0, 4);
                dirIndex = randDir;
                moveDir = directions[dirIndex];
            }

            base.ProcessMoving();
        }
    }
}
