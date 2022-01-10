using System;

namespace BattleCitySharp.Map
{
    public class Generator : GameObject
    {
        private int generateCount = 50;
        private int bonusCount = 5;

        public Generator()
        {
            GameObjectType = ObjectType.Manager;
        }

        public override void Start()
        {
            var random = new Random();
            GenerateMap(random);
            GenerateBonus(random);
        }

        private void GenerateMap(Random random)
        {
            for (var i = 0; i < generateCount; i++)
            {
                var x = random.Next(0, 13);
                var y = random.Next(0, 13);
                Core.Instantiate(new Box(), Grid.Instance[x, y]);
            }
        }

        private void GenerateBonus(Random random)
        {
            var amount = random.Next(bonusCount);
            for (var i = 0; i < amount; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(3, 10);
                    y = random.Next(3, 10);                    
                }while (Grid.Instance[x, y].Type != ObjectType.Empty);
                Core.Instantiate(new AidKit(), Grid.Instance[x, y]);
            }
        }
    }
}
