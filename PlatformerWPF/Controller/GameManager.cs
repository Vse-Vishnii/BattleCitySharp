using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BattleCitySharp
{
    public static class GameManager
    {
        private static Canvas field;
        private static Canvas ui;

        private static int enemyCount;

        public static void StartGame(Canvas _field,Canvas _ui)
        {
            field = _field;
            ui = _ui;
            Drawer.SetCanvas(field);
            UIDrawer.SetCanvas(ui);
            Runner.Start();
        }

        public static void ReloadGame()
        {
            foreach(var obj in ObjectContainer.Objects.ToList())
            {
                Core.Destroy(obj);
            }
            StartGame(field, ui);
        }

        public static void SetStartCountOfEnemies(int count)
        {
            enemyCount = count;
        }

        public static void ChangeEnemyCount()
        {
            enemyCount--;
            if (enemyCount <= 0)
            {
                ReloadGame();
            }
        }
    }
}
