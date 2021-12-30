using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class EnemySpawner : GameObject
    {
        private int generateCount = 5;
        private Grid.Cell spawnCell;

        private int generated;
        private int spawnCooldown = 3;
        private float currentCooldown;

        public EnemySpawner()
        {
            GameObjectType = ObjectType.Manager;            
        }

        public override void Start()
        {
            generated = 0;
            currentCooldown = 0;
            var random = new Random();
            var x = random.Next(1, 10);
            var y = random.Next(1, 10);
            spawnCell = Grid.Instance[x, y];
        }

        public override void Update()
        {
            if (generated == 5)
                return;
            currentCooldown -= Time.DeltaTime;
            if (currentCooldown <= 0)
            {
                currentCooldown = spawnCooldown;
                generated++;
                Core.Instantiate(new Enemy(), spawnCell);
            }            
        }

        // private void CheckSpawnPosition(int x, int y)
        // {
        //     while(x == e.X && y == e.Y)
        // }
    }
}
