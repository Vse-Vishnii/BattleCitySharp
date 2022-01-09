using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class Health
    {
        private readonly int maxHP;
        private readonly GameObject gameObject;
        public int HP { get; private set; }

        public Health(int max, GameObject gameObject)
        {
            maxHP = max;
            HP = maxHP;
            if(gameObject is Player) UIController.SetHealth(HP);
            this.gameObject = gameObject;
        }

        public void TakeDamage(int damage)
        {
            HP--;
            if(gameObject is Player) UIController.SetHealth(HP);
            if (HP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Core.Destroy(gameObject);
            if(gameObject is Enemy) UIController.DeleteEnemy();
        }
    }
}
