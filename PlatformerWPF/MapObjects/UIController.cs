using BattleCitySharp.Controller;

namespace BattleCitySharp
{
    public class UIController : GameObject
    {
        private int enemyCount = EnemySpawner.generateCount;

        public UIController()
        {
            GameObjectType = ObjectType.Manager;            
        }

        public override void Start()
        {
            UIDrawer.CreateEnemiesInfo(enemyCount);
            UIDrawer.CreatePlayerInfo();
        }

        public static void SetHealth(int hp)
        {
            UIDrawer.UpdatePlayerInfo(hp);
        }
        public static void DeleteEnemy()
        {
            UIDrawer.DeleteObject();
        }
    }
}