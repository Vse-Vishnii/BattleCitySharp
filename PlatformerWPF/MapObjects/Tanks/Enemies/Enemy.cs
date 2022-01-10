using System;
using System.Collections.Generic;
using System.Numerics;

namespace BattleCitySharp
{
    public class Enemy : Tank
    {
        public Health Health { get; private set; }

        private int typeNumber = 2;

        private Vector2[] directions = new Vector2[4]
        {
            new Vector2(0, 0.1f),
            new Vector2(0.1f, 0),
            new Vector2(0, -0.1f),
            new Vector2(-0.1f, 0)
        };

        private Dictionary<int, Action> typeDefiner;

        private int dirIndex;

        public Enemy()
        {
            currentCooldown = 0;
            GameObjectType = ObjectType.Enemy;
            TeamId = 2;
            typeDefiner = new Dictionary<int, Action>
            {
                {0,()=>DefineEnemy(1,(1,4),10) },
                {1,()=>DefineEnemy(3,(2,6),5)  }
            };
        }

        public override void Start()
        {
            base.Start();
            var random = new Random();
            var dirIndex = random.Next(0, 3);
            moveDir = directions[dirIndex];            
            currentCooldown = cooldown;
            var type = random.Next(typeNumber);
            EnemyDrawer.ChangeEnemyMaterial(this, type);
            typeDefiner[type]();
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
                    base.ProcessShooting();
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

        private void DefineEnemy(int hp, (int, int) shootSpeed, int moveSpeed)
        {
            var random = new Random();
            Health = new Health(hp, this);
            cooldown = random.Next(shootSpeed.Item1, shootSpeed.Item2);
            speed = moveSpeed;
        }
    }
}
