using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class EnemySpawner : GameObject
    {
        public static int generateCount { get; } = 5;
        private Grid.Cell spawnCell;

        private int generated;
        private int spawnCooldown = 5;
        private float currentCooldown;

        public EnemySpawner()
        {
            GameObjectType = ObjectType.Manager;            
        }

        public override void Start()
        {
            generated = 0;
            currentCooldown = 0;
            GameManager.SetStartCountOfEnemies(generateCount);
            var random = new Random();
            int x, y;
            do
            {
                x = random.Next(13);
                y = random.Next(13);
            } 
            while (Grid.Instance[x, y].Type != ObjectType.Empty);
            spawnCell = Grid.Instance[x, y];
        }

        public override void Update()
        {
            if (generated == generateCount)
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
