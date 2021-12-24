using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class Bullet : MovingObject
    {
        private float speed = 10f;
        private int damage = 2;

        public Bullet()
        {            
            GameObjectType = ObjectType.Bullet;
        }

        public override void Start()
        {
            Collider.IsTrigger = true;
        }

        public override void Update()
        {
            Move(Transform.MoveDirection, speed, Transform.Size / 2);
        }

        public override void ColliderEnter(Collider collider)
        {
            var gameObject = collider.GameObject;
            var health = (Health)gameObject.GetType().GetProperties()
                .Where(o => o.PropertyType == typeof(Health))
                .FirstOrDefault().GetValue(gameObject);
            if (health != null)
            {
                health.TakeDamage(damage);
                Core.Destroy(this);
            }
                
        }
    }
}
